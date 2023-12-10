using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float sensity = 2; // độ nhạy
    public float speed = 5;
    public Transform character_trans;
    public Transform root_cam;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta_mouse = InputManager.deltaMouse;
        horizontal=Mathf.Lerp(horizontal, horizontal+delta_mouse.x, Time.deltaTime*speed);
        vertical=Mathf.Lerp(vertical, vertical-delta_mouse.y, Time.deltaTime*speed);
        Quaternion q_horizontal=Quaternion.Euler(0,horizontal,0);
        character_trans.localRotation = q_horizontal;
        Quaternion q_vertical = Quaternion.Euler(vertical,0, 0);
        root_cam.localRotation = q_vertical;
    }

    public void AddRecoilGun(float damp)
    {
        vertical -= damp;
    }
}
