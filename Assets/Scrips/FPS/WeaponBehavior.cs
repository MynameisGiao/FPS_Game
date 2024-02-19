using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class WeaponData
{
    public ConfigGunRecord cf;
}

public class WeaponBehavior : MonoBehaviour
{
    private EnemyControl enemyControl;
    public Animator animator;
    private Action callback_hideGun;
    public WeaponRender weaponRender;

    private bool is_Zoom;
    public bool isZoom
    {
        get
        {
            return is_Zoom;
        }
        set
        {
            is_Zoom = value;
            cameraControl.fov = is_Zoom ? fov_zoom : fov_normal;
            crossHair.OnZoom(is_Zoom);
            if (isSniper)
            {
                model.SetActive(!is_Zoom);
            }
        }
    }
    public float rof;
    public int clip_Size;
    public float time_reload_Left;
    public float time_reload_outof;
    private bool isReloading;
    public int number_bullet;
    public int total;
    private float time_fire;
    private bool isFire;
    public bool isAuto;

    // check when isAuto -> false
    private bool isCheckAuto;
    public ShellControl shellControl;
    public MuzzleFlash muzzleFlash;

    public AudioSource audioSource_;
    public AudioClip[] sfx_fires;
    public AudioClip sfx_reload_left;
    public AudioClip sfx_reload_outof;
    public AudioClip sfx_ready;

    // cho ShotGun
    public AudioClip sfx_reload_open;
    public AudioClip sfx_reload_insert;
    public AudioClip sfx_reload_close;


    public float min_accuracy;
    public float max_accuracy;
    public float cur_accuracy;
    public float drop_accuracy;
    public float recvoer_accuracy;
    public float recoil;
    public LayerMask mask;
    public CrossHair crossHair;
    private CameraControl cameraControl;
    private Camera cam;
    public WeaponData weaponData;
    public UnityEvent<int, int> OnAmmoChange;

    public bool isSniper;
    public bool isShotGun;

    public float fov_normal;
    public float fov_zoom;
    public GameObject model;

    public int bps = 1;

    // Start is called before the first frame update
    void Awake()
    {
        cameraControl = GameObject.FindObjectOfType<CameraControl>();
        cam = cameraControl.cam;
        isZoom = false;
    }
    public void Setup(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        clip_Size = weaponData.cf.ClipSize;
        total = weaponData.cf.Total;
        rof = weaponData.cf.ROF;
        recoil = weaponData.cf.Recoil;
        fov_normal = weaponData.cf.Fov_normal;
        fov_zoom = weaponData.cf.Fov_zoom;
        number_bullet = clip_Size;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 move_dir = InputManager.move_Dir;
        float speed = InputManager.isRun ? 2 : 1;
        if (move_dir.magnitude > 0.5f)
        {
            animator.SetFloat("Speed", speed);

        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        time_fire += Time.deltaTime;
        if (isFire && number_bullet > 0 && !isReloading)
        {
            if (!isAuto)
            {
                if (isCheckAuto)
                    return;
            }
            if (time_fire >= rof)
            {
                isCheckAuto = true;

                time_fire = 0;
                number_bullet--;
                OnAmmoChange?.Invoke(number_bullet, total);
                shellControl.Fire();
                muzzleFlash.Fire();
                AudioClip sfx_fire = sfx_fires.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                audioSource_.PlayOneShot(sfx_fire);

                cur_accuracy += drop_accuracy;
                cur_accuracy = Mathf.Clamp(cur_accuracy, min_accuracy, max_accuracy);
                for (int i = 0; i < bps; i++)
                {
                    CreateBullet();
                }

                cameraControl.AddRecoilGun(recoil);

                if (isZoom)
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
        cur_accuracy = Mathf.Lerp(cur_accuracy, min_accuracy, Time.deltaTime * recvoer_accuracy);
        crossHair.cur_accuracy = cur_accuracy;
    }
    private void CreateBullet()
    {
        float x = UnityEngine.Random.Range(-cur_accuracy, cur_accuracy) * 0.001f;

        float y = UnityEngine.Random.Range(-cur_accuracy, cur_accuracy) * 0.001f;
        Ray RayOrigin = cam.ViewportPointToRay(new Vector3(0.5f + x, 0.5f + y, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(RayOrigin, out hitInfo, 100f, mask))
        {
            
            Transform impact = null;
            if (hitInfo.collider.CompareTag("SoftBody"))
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
            else if (hitInfo.collider.CompareTag("Enemy"))
            {
                EnemyControl enemy = hitInfo.collider.GetComponent<EnemyControl>();
                if (enemy != null)
                {
                    enemyControl = enemy;
                    impact = BYPoolManager.instance.GetPool("Impact Blood").Spawn();
                    enemyControl.OnDamage(this.weaponData);
                   
                   
                }
               
            }

            if (impact != null)
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
            isCheckAuto = false;
    }
    public void OnZoom()
    {
        isZoom = !isZoom;
        if (isZoom)
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
        if (!isReloading && number_bullet < clip_Size && total > 0)
        {
            if (isShotGun)
            {
                StopCoroutine("ReloadProgressShotGun");
                StartCoroutine("ReloadProgressShotGun");
            }
            else
            {
                StopCoroutine("ReloadProgress");
                StartCoroutine("ReloadProgress");
            }

        }

    }

    IEnumerator ReloadProgressShotGun()
    {
        isReloading = true;
        isFire = false;
        isZoom = false;
        weaponRender.OnNormal();
        audioSource_.PlayOneShot(sfx_reload_open);
        animator.Play("Reload_Open", 0, 0);
        yield return new WaitForSeconds(0.93f);
        while (number_bullet < clip_Size && total > 0)
        {
            audioSource_.PlayOneShot(sfx_reload_insert);
            animator.Play("Reload_Insert", 0, 0);
            yield return new WaitForSeconds(0.7f);
            number_bullet++;
            total--;
            OnAmmoChange?.Invoke(number_bullet, total);
        }
        audioSource_.PlayOneShot(sfx_reload_close);
        animator.Play("Reload_Close", 0, 0);
        yield return new WaitForSeconds(0.8f);
        isReloading = false;
    }
    IEnumerator ReloadProgress()
    {
        isReloading = true;
        isFire = false;
        isZoom = false;
        weaponRender.OnNormal();
        float timeReload = number_bullet > 0 ? time_reload_Left : time_reload_outof;
        if (number_bullet > 0)
        {
            audioSource_.PlayOneShot(sfx_reload_left);

            animator.Play("Reload_ammo_left", 0, 0);

        }
        else
        {
            audioSource_.PlayOneShot(sfx_reload_outof);
            animator.Play("Reload_out_of_ammo", 0, 0);

        }
        yield return new WaitForSeconds(timeReload);

        isReloading = false;
        int number_need = clip_Size - number_bullet;
        if (total >= number_need)
        {
            number_bullet = clip_Size;
            total -= number_need;
        }
        else
        {
            number_bullet += total;
            total = 0;
        }
        OnAmmoChange?.Invoke(number_bullet, total);

    }
    public void OnHideGun(Action callback)
    {
        animator.Play("Hide", 0, 0);
        Invoke("HideGun", 0.5f);
        callback_hideGun = callback;
        OnAmmoChange.RemoveAllListeners();
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
        audioSource_.PlayOneShot(sfx_ready);
        OnAmmoChange?.Invoke(number_bullet, total);
    }

   
}
