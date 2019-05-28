﻿using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public LayerMask PlatformMask;
	//detect where the player last standing, and move it with floating terrain
	private GameObject _lastStandingOn;

	public GameObject StandingOn{ get; private set;}
	private BoxCollider2D _boxCollider;

	private const float SkinWidth = 0.2f;
	//used to detects other object
	private const int TotalHorizontalRays = 8; 
	private const int TotalVerticalRays = 4;

	private float _verticalDistanceBetweenRays,	_horizontalDistanceBetweenRays;
	private float _jumpIn;

	//used for movingup and down slopes
	private static readonly float SlopeLimitTangant = Mathf.Tan (75f * Mathf.Deg2Rad);
	
	//Serialize
	public ControllerParameters DefaultParameters;
	private ControllerParameters _overrideParameters;
	public ControllerParameters Parameters{ get {return _overrideParameters ?? DefaultParameters;}}
	public ControllerState State { get; private set;}

	private Transform _transform;

	private Vector2 _velocity;
	public Vector2 Velocity{ get{ return _velocity; }}
	public Vector3 PlatformVelocity { get; private set;}
	private Vector3 _localscale;
	private Vector3 _raycastTopLeft, _raycastBottomRight, _raycastBottomLeft;
	private Vector3 _activeGlobalPlatformPoint, _activeLocalPlatformPoint;

	public bool HandleCollisions { get; set;}
	public bool CanJump{
		get{
			if(Parameters.JumpRestrictions == ControllerParameters.JumpBehavior.CanJumpAnywhere){
				return _jumpIn <= 0;
			}
			if(Parameters.JumpRestrictions == ControllerParameters.JumpBehavior.CanJumpOnGround){
				return State.isGrounded;
			}
			return false;
		}
	}

	public void Awake(){
		
		State = new ControllerState ();
		
		HandleCollisions = true;
		
		_transform = transform;
		_localscale = transform.localScale;
		_boxCollider = GetComponent<BoxCollider2D> ();
		
		var colliderWidth = _boxCollider.size.x * Mathf.Abs (transform.localScale.x) - (2 * SkinWidth);
		_horizontalDistanceBetweenRays = colliderWidth / (TotalVerticalRays - 1);
		
		var colliderHeight = _boxCollider.size.y * Mathf.Abs (transform.localScale.y) - (2 * SkinWidth);
		_verticalDistanceBetweenRays = colliderHeight / (TotalHorizontalRays - 1);
		
		//Debug.Log (""+colliderWidth);
	}

	public void AddForce(Vector2 force){
		_velocity += force;
	}
	
	public void SetForce(Vector2 force){
		_velocity = force;
	}
	
	public void SetHorizontalForce(float x){
		_velocity.x = x;
	}
	
	public void SetVerticalForce(float y){
		_velocity.y = y;
	}

	public void Jump(){
		AddForce (new Vector2 (0, Parameters.JumpMagnitude));
		_jumpIn = Parameters.JumpFrequency;
	}
	
	public void LateUpdate(){ //automatically called by system
		_jumpIn -= Time.deltaTime; 
		_velocity.y += Parameters.Gravity * Time.deltaTime;
		Move (Velocity * Time.deltaTime);
	}

	private void Move(Vector2 deltaMovement){
		var wasGrounded = State.isCollidingBelow;
		State.Reset ();
		
		if (HandleCollisions) {
			HandlePlatforms();
			CalculateRayOrigins();
			
			if(deltaMovement.y < 0 && wasGrounded){
				HandleVerticalSlope(ref deltaMovement);
			}
			
			if(Mathf.Abs(deltaMovement.x) > .001f){
				MoveHorizontally(ref deltaMovement);
			}

			MoveVertically(ref deltaMovement);

			CorrectHorizontalPlacement(ref deltaMovement, true);
			CorrectHorizontalPlacement(ref deltaMovement, false);
		}
		
		_transform.Translate (deltaMovement, Space.World);
		
		if (Time.deltaTime > 0) {
			_velocity = deltaMovement / Time.deltaTime;		
		}
		
		_velocity.x = Mathf.Min (_velocity.x, Parameters.MaxVelocity.x);
		_velocity.y = Mathf.Min (_velocity.y, Parameters.MaxVelocity.y);
		
		//handle moving up the slope
		if (State.isMovingUpSlope) {
			_velocity.y = 0;	
		}
		
		//Handle Platforms?
		if (StandingOn != null) {
			_activeGlobalPlatformPoint = transform.position;
			_activeLocalPlatformPoint = StandingOn.transform.InverseTransformPoint(transform.position);
			
			if(_lastStandingOn != StandingOn){
				if(_lastStandingOn != null){
					_lastStandingOn.SendMessage("ControllerExit2D", this, SendMessageOptions.DontRequireReceiver);
				}
				
				StandingOn.SendMessage("ControllerEnter2D", this, SendMessageOptions.DontRequireReceiver );
				_lastStandingOn = StandingOn;
			}else if (StandingOn != null){
				_lastStandingOn.SendMessage("ControllerStay2D", this, SendMessageOptions.DontRequireReceiver);
				_lastStandingOn = null;
			}
		}else if(_lastStandingOn != null) {
			_lastStandingOn.SendMessage("ControllerExit2D", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void HandlePlatforms(){
		//Handling Platforms
		if (StandingOn != null)
		{
			var newGlobalPlatformPoint = StandingOn.transform.TransformPoint(_activeLocalPlatformPoint);
			var moveDistance = newGlobalPlatformPoint - _activeGlobalPlatformPoint;
			
			if (moveDistance != Vector3.zero)
				transform.Translate(moveDistance, Space.World);
			
			PlatformVelocity = (newGlobalPlatformPoint - _activeGlobalPlatformPoint) / Time.deltaTime;
		}
		else
			PlatformVelocity = Vector3.zero;
		
		StandingOn = null;
	}

	private void CorrectHorizontalPlacement(ref Vector2 deltaMovement, bool isRight){
		var halfWidth = (_boxCollider.size.x * _localscale.x) / 2f;
		var rayOrigin = isRight ? _raycastBottomRight : _raycastBottomLeft;
		
		if (isRight) {
			rayOrigin.x -= (halfWidth - SkinWidth);		
		} else {
			rayOrigin.x += (halfWidth - SkinWidth);		
		}
		
		var rayDirection = isRight ? Vector2.right : -Vector2.right;
		var offset = 0f;
		
		for (var i = 1; i < TotalHorizontalRays - 1; i++) {
			var rayVector = new Vector2 (deltaMovement.x + rayOrigin.x, deltaMovement.y + rayOrigin.y + (i * _verticalDistanceBetweenRays));
			//Debug.DrawRay(rayVector, rayDirection * halfWidth, isRight ? Color.cyan : Color.magenta);
			
			var raycastHit = Physics2D.Raycast(rayVector, rayDirection, halfWidth, PlatformMask);
			if(!raycastHit) {
				continue;
			}
			
			offset = isRight ? ((raycastHit.point.x - _transform.position.x) - halfWidth) : (halfWidth - (_transform.position.x - raycastHit.point.x));
		}
		deltaMovement.x += offset;
	}

	private void CalculateRayOrigins(){
		var size = new Vector2 (_boxCollider.size.x * Mathf.Abs(_localscale.x), _boxCollider.size.y * Mathf.Abs(_localscale.y)) /2;
		
		var center = new Vector2 (_boxCollider.offset.x * _localscale.x, _boxCollider.offset.y * _localscale.y);
		
		//shouldnt it be vector2
		_raycastTopLeft = _transform.position + new Vector3 (center.x - size.x + SkinWidth, center.y + size.y - SkinWidth);
		_raycastBottomRight = _transform.position + new Vector3 (center.x + size.x - SkinWidth, center.y - size.y + SkinWidth);
		_raycastBottomLeft = _transform.position + new Vector3 (center.x - size.x + SkinWidth, center.y - size.y + SkinWidth);
	}

	private void MoveHorizontally(ref Vector2 deltaMovement){
		var isGoingRight = deltaMovement.x > 0;
		var rayDistance = Mathf.Abs (deltaMovement.x) + SkinWidth;
		var rayDirection = isGoingRight ? Vector2.right : - Vector2.right;
		var rayOrigin = isGoingRight ? _raycastBottomRight : _raycastBottomLeft;
		
		for(var i = 0; i< TotalHorizontalRays; i++){
			//typo here + replace with *
			var rayVector = new Vector2(rayOrigin.x, rayOrigin.y + (i * _verticalDistanceBetweenRays));
			Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);
			
			var raycastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, PlatformMask);
			if(!raycastHit){
				continue;
			}
			if(i == 0 && HandleHorizontalSlope(
				ref deltaMovement, Vector2.Angle(raycastHit.normal, Vector2.up),
				isGoingRight)){
				break;
			}
			
			deltaMovement.x = raycastHit.point.x - rayVector.x;
			rayDistance = Mathf.Abs(deltaMovement.x);
			
			if(isGoingRight){
				deltaMovement.x -= SkinWidth;
				State.isCollidingRight = true;
			}else {
				deltaMovement.x += SkinWidth;
				State.isCollidingLeft = true;
			}
			
			if(rayDistance < SkinWidth + .0001f){
				break;
			}
		}
	}

	private void MoveVertically(ref Vector2 deltaMovement){
		var isGoingUp = deltaMovement.y > 0; 
		var rayDistance = Mathf.Abs (deltaMovement.y) + SkinWidth;
		var rayDirection = isGoingUp ? Vector2.up : - Vector2.up;
		var rayOrigin = isGoingUp ? _raycastTopLeft : _raycastBottomLeft;
		
		rayOrigin.x += deltaMovement.x;
		
		var standingOnDistance = float.MaxValue;
		for(var i = 0; i < TotalVerticalRays; i++){
			//typo here + replace with *
			var rayVector = new Vector2(rayOrigin.x + (i * _horizontalDistanceBetweenRays), rayOrigin.y);
			Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);
			
			var raycasHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, PlatformMask);
			if(!raycasHit){
				continue;
			}
			
			if(isGoingUp){
				var verticalDistanceToHit = _transform.position.y - raycasHit.point.y;
				if(verticalDistanceToHit < standingOnDistance){
					
					standingOnDistance = verticalDistanceToHit;
					StandingOn = raycasHit.collider.gameObject;
					
				}
			}
			
			deltaMovement.y = raycasHit.point.y - rayVector.y;
			rayDistance = Mathf.Abs(deltaMovement.y);
			
			if(isGoingUp){
				deltaMovement.y -= SkinWidth;
				State.isCollidingAbove = true;
			}else{
				deltaMovement.y += SkinWidth;
				State.isCollidingBelow = true;
			}
			
			if(!isGoingUp && deltaMovement.y > .0001f){
				State.isMovingUpSlope = true;
			}
			
			if(rayDistance < SkinWidth + .0001f){
				break;
			}
		}
	}

	private void HandleVerticalSlope(ref Vector2 deltaMovement)
	{
		var center = (_raycastBottomLeft.x + _raycastBottomRight.x) / 2;
		var direction = -Vector2.up;
		
		var slopeDistance = SlopeLimitTangant * (_raycastBottomRight.x - center);
		var slopeRayVector = new Vector2(center, _raycastBottomLeft.y);
		
		Debug.DrawRay(slopeRayVector, direction * slopeDistance, Color.yellow);
		
		var raycastHit = Physics2D.Raycast(slopeRayVector, direction, slopeDistance, PlatformMask);
		if (!raycastHit)
			return;
		
		// ReSharper disable CompareOfFloatsByEqualityOperator
		
		var isMovingDownSlope = Mathf.Sign(raycastHit.normal.x) == Mathf.Sign(deltaMovement.x);
		if (!isMovingDownSlope)
			return;
		
		var angle = Vector2.Angle(raycastHit.normal, Vector2.up);
		if (Mathf.Abs(angle) < .0001f)
			return;
		
		State.isMovingDownSlope = true;
		State.SlopeAngle = angle;
		deltaMovement.y = raycastHit.point.y - slopeRayVector.y;
	}
	
	private bool HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
	{
		if (Mathf.RoundToInt(angle) == 90)
			return false;
		
		if (angle > Parameters.SlopeLimit)
		{
			deltaMovement.x = 0;
			return true;
		}
		
		if (deltaMovement.y > .07f)
			return true;
		
		deltaMovement.x += isGoingRight ? -SkinWidth : SkinWidth;
		deltaMovement.y = Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad) * deltaMovement.x);
		State.isMovingUpSlope = true;
		State.isCollidingBelow = true;
		return true;
	}

	public void onTriggerEnter2D(Collider2D other){
		
	}
	public void onTriggerExit2D(Collider2D other){}

}
