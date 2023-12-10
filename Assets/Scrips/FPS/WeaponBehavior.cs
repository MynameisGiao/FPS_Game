using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public ShellControl shellControl;
    public MuzzleFlash muzzleFlash;
    public AudioSource audioSource;

    public AudioClip[] sfx_fires;
    public AudioClip sfx_reload_left;
    public AudioClip sfx_reload_outof;
    public AudioClip sfx_ready;

    public float min_accuracy;
    public float max_accuracy;
    public float cur_accuracy;
    public float drop_accurancy;
    public float recover_accuracy;
    public float recoil = 0.2f;

    public LayerMask mask;
    public CrossHair crossHair;
    private CameraControl cameraControl;
    private Camera cam;
    // Start is called before the first frame update
    void Awake()
    {
        cameraControl= GameObject.FindObjectOfType<CameraControl>();
        cam = cameraControl.cam;

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
                shellControl.Fire();
                muzzleFlash.Fire();
                AudioClip sfx_fire=sfx_fires.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                audioSource.PlayOneShot(sfx_fire);

                cur_accuracy += drop_accurancy;
                cur_accuracy=Mathf.Clamp(cur_accuracy, min_accuracy, max_accuracy);
                CreateBullet();
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
        cur_accuracy =Mathf.Lerp(cur_accuracy, min_accuracy, Time.deltaTime * recover_accuracy);
        crossHair.cur_accuracy = cur_accuracy;
    }

    private void CreateBullet()
    {
        float x= UnityEngine.Random.Range(-cur_accuracy, cur_accuracy) * 0.001f;
        float y= UnityEngine.Random.Range(-cur_accuracy, cur_accuracy) * 0.001f;

        cameraControl.AddRecoilGun(recoil);
        Ray RayOrigin = cam.ViewportPointToRay(new Vector3(0.5f + x, 0.5f + y, 0));
        RaycastHit hitInfo;
        if(Physics.Raycast(RayOrigin, out hitInfo,100f, mask))
        {
            Transform impact = null;
            if(hitInfo.collider.CompareTag("SoftBody"))
            {
                impact = BYPoolManager.instance.GetPool("Impact SoftBody").Spawn();
            }
            else if (hitInfo.collider.CompareTag("Metal"))
            {
                impact = BYPoolManager.instance.GetPool("Impact Metal").Spawn();
            }
            else if (hitInfo.collider.CompareTag("Concrete"))
            {
                impact = BYPoolManager.instance.GetPool("Impact Concrete").Spawn();
            }
            else if (hitInfo.collider.CompareTag("Dirt"))
            {
                impact = BYPoolManager.instance.GetPool("Impact Dirt").Spawn();
            }
           
            if(impact!= null)
            {
                impact.position = hitInfo.point;
                impact.forward = hitInfo.normal;
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
       
        if (number_bullet>0 )
        {
            audioSource.PlayOneShot(sfx_reload_left);
            animator.Play("Reload_ammo_left", 0, 0);
        }
        else
        {
            audioSource.PlayOneShot(sfx_reload_outof);
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
        audioSource.PlayOneShot(sfx_ready);
    }

}
