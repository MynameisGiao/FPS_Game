using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPS_Crosshair : MonoBehaviour
{
    public float minSize = 50;
    public float maxSize = 100;
    public float cur_size;
    public float speed=2;
    public RectTransform parent;
    public float max_size_screen = 1920;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateCrosshair(float accuracy)
    {
        cur_size = accuracy * max_size_screen / 1000f;
    }
    // Update is called once per frame
    void Update()
    {
      
        cur_size = Mathf.Lerp(cur_size, minSize, Time.deltaTime * speed);
        parent.sizeDelta = cur_size * Vector2.one;
    }
}
