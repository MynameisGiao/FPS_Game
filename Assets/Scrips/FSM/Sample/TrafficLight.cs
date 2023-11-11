using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLight : FSM_System
{
    public Image image_light;
    public Text count_lb;

    public Red_State red_state;
    public Green_State green_State;
    public Yellow_State yellow_State;
    // Start is called before the first frame update
    void Start()
    {
        red_state.parent = this;
        green_State.parent = this;
        yellow_State.parent = this;
        GotoState(green_State);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //

    }
}
