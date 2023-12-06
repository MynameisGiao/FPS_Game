using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRender : MonoBehaviour
{
    public Vector3 posNormal;
    public Vector3 posAim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnNormal()
    {
        transform.localPosition = posNormal;
    }
    public void OnZoom()
    {
        transform.localPosition = posAim;
    }
}
