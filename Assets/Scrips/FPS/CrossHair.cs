using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public RectTransform parent;
    public float rate_size;
    public float cur_accuracy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parent.sizeDelta = Vector2.one * rate_size * cur_accuracy; 
    }
}
