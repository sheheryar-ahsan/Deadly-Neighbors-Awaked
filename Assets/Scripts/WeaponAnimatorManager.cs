using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    private Animator weaponAnimator;

    [Header("Weapon FX")]
    public GameObject weaponMuzzleFlashFX; // the muzzle flash FX that is instantiated when the weapon is fired
    public GameObject weaponBulletCaseFX; // the bullet case FX that is ejected from the weapon, when the weapon is fired

    [Header("Weapon FX Transform")]
    public Transform weaponMuzzleFlashTransform; // the location muzzle flash fx will instantiated
    public Transform weaponBulletCaseTransform; // the location the bullet case will instantiated

    private void Awake()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
    }

    public void ShootWeapon(PlayerCamera playerCamera)
    {
        // animate pistol
        weaponAnimator.Play("Shoot");
        // Instantiate muzzle flash FX
        GameObject muzzleFlash = Instantiate(weaponMuzzleFlashFX, weaponMuzzleFlashTransform);
        muzzleFlash.transform.parent = null;
        // Instantiate empty bullet case
        GameObject bulletCase  = Instantiate(weaponBulletCaseFX, weaponBulletCaseTransform);
        bulletCase.transform.parent = null;
        // shooting something
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.cameraObject.transform.position, playerCamera.cameraObject.transform.forward, out hit))
        {
            Debug.Log(hit.transform.gameObject.name);
        }
    }
}
