using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_System : MonoBehaviour
{
    public FSM_State cur_State;
    private FSM_State previous_State;
    // Start is called before the first frame update
    public void GotoState(FSM_State newState)
    {
       
        if(cur_State!=null)
        {
            cur_State.Exit();
            previous_State = cur_State;
        }
        cur_State = newState;
        cur_State.Enter();
    }
    public void GotoState(FSM_State newState, object data)
    {
        cur_State?.Exit();
        if(cur_State!=null)
        {
            previous_State = cur_State;
        }
        cur_State = newState;
        cur_State.Enter(data);
    }

    public void GotoPreviousState()
    {
        if(previous_State!=null)
        {
            FSM_State temp_state=null;

            if (cur_State!=null)
            {
                cur_State.Exit();
                temp_state = cur_State;
            }
            cur_State = previous_State;
            cur_State.Enter();
            previous_State = temp_state;
        }
    }
    // Update is called once per frame

    protected virtual void FixedUpdate()
    {
        cur_State?.FixedUpdate();
    }

    protected virtual void Update()
    {
        cur_State?.Update();
    }
    protected virtual void LateUpdate()
    {
        cur_State?.LateUpdate();
    }

    public  void OnAnimEnter()
    {
        cur_State?.OnAnimEnter();
    }
    public  void OnAnimMiddle()
    {
        cur_State?.OnAnimMiddle();
    }
    public  void OnAnimExit()
    {
        cur_State?.OnAnimExit();
    }
}
