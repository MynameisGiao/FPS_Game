using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePool : MonoBehaviour
{
    public Transform prefab_impact;
    // Start is called before the first frame update
    void Start()
    {
        BYPool imact_pool = new BYPool("Impact_Cube", 15, prefab_impact);
        BYPoolManager.instance.AddPool(imact_pool);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Transform cube_pool = BYPoolManager.instance.GetPool("sample").Spawn();
        
            cube_pool.position = transform.position;
            cube_pool.GetComponent<SamplePoolCube>().Setup(transform.position, transform.forward * 80);
        }
    }
}
