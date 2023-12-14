using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public RectTransform parent;
    public float rate_size;
    public float cur_accuracy;
    public bool isSniper;
    public Image crossHairSniper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSniper)
        {

        }
        else
        {
            parent.sizeDelta = Vector2.one * rate_size * cur_accuracy;
        }
     
    }
    public void OnZoom(bool isZoom)
    {
        if (isSniper)
        {
            crossHairSniper.gameObject.SetActive(isZoom);
        }
        else
        {
            parent.gameObject.SetActive(!isZoom);
        }
    }
}
