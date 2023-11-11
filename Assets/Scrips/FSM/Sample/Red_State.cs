using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Red_State : FSM_State
{
    [NonSerialized]
    public TrafficLight parent;
    // Start is called before the first frame update
    private int count = 0;
    private float time_count;
    public override void Enter()
    {
        count = 5;
        parent.count_lb.text = count.ToString();
        parent.image_light.color = Color.red;
    }
    public override void Update()
    {
        time_count += Time.deltaTime;
        // time_count = time_count + Time.deltaTime;
        if (time_count >= 1)
        {
            time_count = 0;
            count--;
            parent.count_lb.text = count.ToString();
            if (count <= 0)
            {
                parent.GotoState(parent.green_State);
            }
        }
    }
}
