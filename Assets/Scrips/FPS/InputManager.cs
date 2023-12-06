using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : BYSingletonMono<InputManager>
{
    public static Vector3 deltaMouse;
    private Vector3 ogrinal_pos;
    public static Vector3 move_Dir;
    public static bool isRun;
    public UnityEvent OnChangeGun;
    public UnityEvent OnZoom;
    public UnityEvent OnJump;
    public UnityEvent <bool> OnFire;
    public UnityEvent OnReload;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        deltaMouse=Vector3.zero;
        if(Input.GetMouseButtonDown(0) )
        {
            ogrinal_pos = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        { 
            deltaMouse=Input.mousePosition -ogrinal_pos;
            ogrinal_pos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
        float x=Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move_Dir=new Vector3 (x,0,z);

        // ChangeGun
        if(Input.GetKeyDown(KeyCode.C)) 
        {
            OnChangeGun?.Invoke();
        }

        // Jump
        if (Input.GetMouseButtonDown(1))
        { 
           OnZoom?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }

        // Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        // Fire
        if (Input.GetMouseButtonDown(0))
        {
            OnFire?.Invoke(true);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            OnFire?.Invoke(false);
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReload?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnChangeGun.RemoveAllListeners();
    }
}
