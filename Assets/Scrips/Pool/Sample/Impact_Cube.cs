using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact_Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSpawn()
    {
        StopCoroutine("LifeTime");
        StartCoroutine("LifeTime");
    }
  
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(2);
        BYPoolManager.instance.GetPool("Impact_Cube").Despawn(transform);
    }
}
