using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_State 
{
    /// <summary>
    /// call to enter state
    /// </summary>
    public virtual void Enter()
    {

    }
    /// <summary>
    /// call to enter state
    /// </summary>
    /// <param name="data"></param>
    public virtual void Enter(object data)
    {
       
    }

    public virtual void FixedUpdate()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void LateUpdate()
    {

    }
    public virtual void Exit()
    {

    }
    public virtual void OnAnimEnter()
    {

    }
    public virtual void OnAnimMiddle()
    {

    }
    public virtual void OnAnimExit()
    {

    }
}
