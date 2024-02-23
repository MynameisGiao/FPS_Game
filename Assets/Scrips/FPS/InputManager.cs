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
    public bool isKeyBoardMove;

    private Joystick joystick; // Thêm dòng này
   
    // Start is called before the first frame update
    void Start()
    {

    }
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    public void OnFireInput(bool isFire)
    {
        OnFire?.Invoke(isFire);
    }
    public void OnZoomInput()
    {
        OnZoom?.Invoke();
    }
    public void OnReloadInput()
    {
        OnReload?.Invoke();
    }
    public void OnChangeGunInput()
    {
        OnChangeGun?.Invoke();
    }

    public void OnJumpInput()
    {
        OnJump?.Invoke();
    }


    // check Joytick
    public void SetJoystick(Joystick newJoystick)
    {
        joystick = newJoystick;
    }
    // Update is called once per frame
    void Update()
    {
        // Move
        deltaMouse=Vector3.zero;

        if(!IsPointerOverUIObject() && !IsUsingJoystick())
        {
            if (Input.GetMouseButtonDown(0))
            {
                ogrinal_pos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                deltaMouse = Input.mousePosition - ogrinal_pos;
                ogrinal_pos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {

            }
        }
        

        if(isKeyBoardMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            move_Dir = new Vector3(x, 0, z);
        }
       

        // ChangeDeck
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
        if(isKeyBoardMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRun = true;
            }
            else
            {
                isRun = false;
            }
        }
       

        // Fire
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnFire?.Invoke(true);
        }
        else if (Input.GetKeyUp(KeyCode.E))
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

    public void SetMoveDir(Vector2 move)
    {
        if (!isKeyBoardMove)
        {
            float x = move.x;
            x = Mathf.Clamp(x, -1, 1);
            float z = move.y;
            z = Mathf.Clamp(z, -1, 1);
            if (move.magnitude>1)
            {
                isRun = true;
            }
            else
            {
                isRun = false;
            }
            
            move_Dir = new Vector3(x, 0, z);
        }

    }


    // check Joytick
    private bool IsUsingJoystick()
    {
        // Adjust the condition based on your Joystick implementation
        return joystick.Direction != Vector2.zero;
    }

}
