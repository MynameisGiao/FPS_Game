using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public Animator animator;
    private Action callback_hideGun;
    public WeaponRender weaponRender;
    public bool isZoom;
    public float rof;
    public int clip_Size;
    public float time_reload_left;
    public float time_reload_outof;
    private bool isReloading;
    public int number_bullet;
    // public int total;
    private float time_fire;
    private bool isFire;
    public bool isAuto;

    // check when is auto -> false
    private bool isCheckAuto;

    // Start is called before the first frame update
    void Awake()
    {
        

    }
    public void Setup()
    {
        number_bullet = clip_Size;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 more_dir = InputManager.move_Dir;
        float speed = InputManager.isRun ? 2 : 1;
        if (more_dir.magnitude > 0.5f)
            animator.SetFloat("Speed", speed);
        else
        {
            animator.SetFloat("Speed", 0);
        }

        time_fire += Time.deltaTime;
        if (isFire && number_bullet>0 && !isReloading)
        {
            if(!isAuto)
            {
                if (isCheckAuto)
                    return;
            }
            if(time_fire>=rof)
            {
                isCheckAuto = true;
                time_fire = 0;
                number_bullet--;
                if(isZoom)
                {
                    animator.Play("Fire_Zoom", 0, 0);
                }
                else
                {
                    animator.Play("Fire", 0, 0);
                    
                }
                if (number_bullet <= 0)
                {
                    // reload
                    OnReload();
                }
            }
        }
    }
    public void OnFire(bool isFire)
    {
        this.isFire = isFire;
        if (isFire)
        {
            isCheckAuto = false;
        }
    }
    public void OnZoom()
    {
        isZoom = !isZoom;
        if(isZoom)
        {
            weaponRender.OnZoom();
            animator.Play("ZoomIn", 0, 0);
        }
        else
        {
            weaponRender.OnNormal();
            animator.Play("ZoomOut", 0, 0);
        }
    }

    public void OnReload()
    {
        if(!isReloading && number_bullet<clip_Size)
        {
            StopCoroutine("ReloadProgress");
            StartCoroutine("ReloadProgress");
        }
        
    }
    IEnumerator ReloadProgress()
    {
        isReloading = true;
        isFire = false;
        isZoom = false;
        weaponRender.OnNormal();
        float timeReload = number_bullet > 0 ? time_reload_left : time_reload_outof;
        if(number_bullet>0 )
        {
            animator.Play("Reload_ammo_left", 0, 0);
        }
        else
        {
            animator.Play("Reload_out_of_ammo", 0, 0);
        }
        yield return new WaitForSeconds(timeReload);
        
        isReloading = false;
        number_bullet = clip_Size;

        //// dùng cho trường hợp có tổng số lượng đạn
        //int number_need = clip_Size - number_bullet;
        //if(total >= number_need )
        //{
        //    number_bullet = clip_Size;
        //    total -= number_need;
        //}
        //else
        //{
        //    number_bullet += total;
        //    total = 0;
        //}
    }
    public void OnHideGun(Action callback)
    {
        animator.Play("Hide", 0, 0);
        Invoke("HideGun", 0.5f);
        callback_hideGun = callback;
    }
    private void HideGun()
    {
        gameObject.SetActive(false);
        callback_hideGun?.Invoke();
    }
    public void OnReadyGun()
    {
        isZoom = false;
        weaponRender.OnNormal();
        gameObject.SetActive(true);
        animator.Play("Show", 0, 0);

    }

}
