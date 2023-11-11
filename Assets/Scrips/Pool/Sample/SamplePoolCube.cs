using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePoolCube : MonoBehaviour
{
    public Rigidbody rig_;
    public GameObject model;
    private void Awake()
    {
        model.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            BYPoolManager.instance.GetPool("sample").Despawn(transform);
            Transform impact = BYPoolManager.instance.GetPool("Impact_Cube").Spawn();
            impact.position = transform.position;
        }
    }
    public void Setup(Vector3 pos, Vector3 force)
    {
        rig_.position = pos;
        rig_.velocity = Vector3.zero;
        rig_.AddForce(force);
    }
    public void OnSpawn()
    {
        Invoke("DelayShowModel", 0.1f);
        StopCoroutine("LifeTime");
        StartCoroutine("LifeTime");
        rig_.isKinematic = false;
    }
    void DelayShowModel()
    {
        model.SetActive(true);

    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(2);
        BYPoolManager.instance.GetPool("sample").Despawn(transform);
    }
    public void OnDespawn()
    {
        model.SetActive(false);
        rig_.isKinematic = true;
        rig_.velocity = Vector3.zero;
    }
}
