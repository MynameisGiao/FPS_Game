using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    public string namePool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSpawn()
    {
        
        StopCoroutine("WaitDeactive");
        StartCoroutine("WaitDeactive");
    }
    IEnumerator WaitDeactive()
    {
        yield return new WaitForSeconds(2);
        BYPoolManager.instance.GetPool(namePool).Despawn(transform);
    }
    private void OnDespawn()
    {

    }
}
