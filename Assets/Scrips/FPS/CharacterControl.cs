using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public CharacterController characterController_;
    private float vel_y;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.OnJump.AddListener(OnJump);
    }
    private void OnJump()
    {
        // set jump force
        if(characterController_.isGrounded)
            vel_y = 3.5f;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 move_dir = InputManager.move_Dir;
        float speed=InputManager.isRun ? 2:1;
        move_dir=transform.TransformDirection(move_dir);

        // x= x0 - v*a*t*t;
        if(vel_y > 0)
        {
            move_dir.y = vel_y;
        }
        else
        {
            if (!characterController_.isGrounded)
            {
                move_dir.y = vel_y;
                if(move_dir.y <-2)
                {
                    move_dir.y = -2;
                }
            }
        }

       
        move_dir.Normalize();
        // v=v0 + at;
        vel_y=vel_y - 9.8f*Time.deltaTime;

        characterController_.Move(move_dir * Time.deltaTime *3.0f* speed);
       
    }


    public virtual void OnDamage(DamageData damageData)
    {
        Debug.LogError("Damage");
       // Destroy(gameObject);
    }

}
