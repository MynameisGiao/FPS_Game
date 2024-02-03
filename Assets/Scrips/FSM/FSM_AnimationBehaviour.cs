using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AnimationBehaviour : StateMachineBehaviour
{
    public FSM_System _system;
    public float time_middle;
    private float time_Count;
    private bool isCall;
    // Start is called before the first frame update
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if(_system==null)
        {
            _system = animator.GetComponent<FSM_System>();
        }
        if (_system == null)
        {
            _system = animator.GetComponentInParent<FSM_System>();
        }
        time_Count = 0;
        isCall = false;
        _system.OnAnimEnter();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        time_Count += Time.deltaTime;
        if(time_Count>=time_middle)
        {
            if(!isCall)
            {
                _system.OnAnimMiddle();
                isCall = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        _system.OnAnimExit();
    }
}
