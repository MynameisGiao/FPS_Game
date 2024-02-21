using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource footstep;
    public AudioSource runningFootstep;

    void Update()
    {
        if (InputManager.move_Dir != Vector3.zero && !InputManager.isRun)
        {
            footstep.enabled = true;
            runningFootstep.enabled = false ;
            
        }
        else if (InputManager.move_Dir != Vector3.zero && InputManager.isRun)
        {
            footstep.enabled = false ;
            runningFootstep.enabled = true;

        }
        else
        {
            footstep.enabled = false;
            runningFootstep.enabled = false;
        }
    }

}

