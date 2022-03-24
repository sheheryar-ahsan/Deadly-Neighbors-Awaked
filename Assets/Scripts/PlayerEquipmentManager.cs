using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    private AnimatorManager animatorManager;
    private WeaponLoaderSlot weaponLoaderSlot;

    [Header("Current Equipment")]
    public WeaponItem weapon;
    public WeaponAnimatorManager weaponAnimator;
    private LeftHandIKTarget leftHandIK;
    private RightHandIKTarget rightHandIK;
    //public SubWeaponItem Subweapon; // knife, grenade etc

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        LoadWeaponLoaderSlots();
    }

    private void Start()
    {
        LoadCurrentweapon();
    }

    private void LoadWeaponLoaderSlots()
    {
        // back slot
        // hip slot
        weaponLoaderSlot = GetComponentInChildren<WeaponLoaderSlot>();
    }

    private void LoadCurrentweapon()
    {
        // load the weapon onto our players hand
        weaponLoaderSlot.LoadWeaponModel(weapon);
        // change our player movement/idle animations to the weapon movemnt/idle animations
        animatorManager.animator.runtimeAnimatorController = weapon.weaponAnimator;
        weaponAnimator = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponAnimatorManager>();
        rightHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        animatorManager.AssignHandIK(rightHandIK, leftHandIK);
    }
}
