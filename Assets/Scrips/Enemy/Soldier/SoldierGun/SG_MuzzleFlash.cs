using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_MuzzleFlash : MonoBehaviour
{
    public ParticleSystem particleSystem_;
    public void Awake()
    {

    }
    public void Fire()
    {
        particleSystem_.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
