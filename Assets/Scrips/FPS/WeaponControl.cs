using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WeaponControl : MonoBehaviour
{
    public List<WeaponBehavior> weapons = new List<WeaponBehavior>();
    public WeaponBehavior current_WP;
    private int index_gun = -1;
    public List<int> gun_ids;
    public Transform anchor_gun;    
    public UnityEvent<WeaponBehavior> OnGunChangeEvent;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return  new WaitUntil(()=>SceneManager.GetActiveScene().buildIndex>1);

        yield return new WaitForSeconds(3);
        foreach (int id in gun_ids)
        {
            ConfigGunRecord cf = ConfigManager.instance.configGun.GetRecordBykeySearch(id);
            GameObject wp_object = Instantiate(Resources.Load("Gun/" + cf.Prefab, typeof(GameObject))) as GameObject;
            wp_object.transform.SetParent(anchor_gun, false);
            WeaponBehavior wp = wp_object.GetComponent<WeaponBehavior>();
            weapons.Add(wp);
            wp_object.SetActive(false);
            wp.Setup(new WeaponData { cf = cf });
        }

        InputManager.instance.OnChangeGun.AddListener(OnChangeGun);
        InputManager.instance.OnZoom.AddListener(OnZoom);
        InputManager.instance.OnFire.AddListener(OnFire);
        InputManager.instance.OnReload.AddListener(OnReload);
        OnChangeGun();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        OnGunChangeEvent.RemoveAllListeners();
    }
    private void OnFire(bool isFire)
    {
        current_WP?.OnFire(isFire);
    }
    private void OnZoom()
    {
        current_WP?.OnZoom();
    }
    private void OnReload()
    {
        current_WP?.OnReload();
    }
    private void OnChangeGun()
    {
        index_gun++;
        if (index_gun >= weapons.Count)
            index_gun = 0;
        if (current_WP != null)
        {
            current_WP.OnHideGun(() =>
            {
                current_WP = weapons[index_gun];
                OnGunChangeEvent?.Invoke(current_WP);

                current_WP.OnReadyGun();
            });
        }
        else
        {
            current_WP = weapons[index_gun];
            OnGunChangeEvent?.Invoke(current_WP);

            current_WP.OnReadyGun();
        }


    }
}
