using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    public Joystick joystick;
    public RectTransform btnFire;
    public Image iconFire_Button;
    public Image iconGun;
    public Text ammo_lb;
    public WeaponControl weaponControl;
    public GameObject lockObject;


    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.SetJoystick(joystick);

        weaponControl.OnGunChangeEvent.AddListener(OnWeaponChange);


    }
    private void OnWeaponChange(WeaponBehavior cur_wp)
    {
        cur_wp.OnAmmoChange.AddListener(OnAmmoChange);
        string name_image = cur_wp.weaponData.cf.Prefab;
        iconGun.overrideSprite = SpriteLibControl.instance.GetSpriteByName(name_image);
        lockObject.SetActive(false);
    }
    private void OnAmmoChange(int number, int total)
    {
        ammo_lb.text = number.ToString() + "/" + total.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        InputManager.instance.SetMoveDir(joystick.Direction);
    }
    public void OnFire(bool isFire)
    {
        btnFire.localScale = Vector3.one * (isFire ? 0.8f : 1);
        InputManager.instance.OnFireInput(isFire);
    }
    public void OnReload()
    {
        InputManager.instance.OnReloadInput();
    }
    public void OnZoom()
    {
        InputManager.instance.OnZoomInput();
    }
    public void OnChangeGun()
    {
        InputManager.instance.OnChangeGunInput();
    }
    public void OnJump()
    {
        InputManager.instance.OnJumpInput();
    }
}
