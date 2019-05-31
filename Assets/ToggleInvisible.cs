using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInvisible : MonoBehaviour
{
   public void toggleInvisibility()
    {
        Renderer read = gameObject.GetComponent<Renderer>();

        if (read.enabled) {
            read.enabled = false;
        }
        else
        {
            read.enabled = true;
        }

    }
}
