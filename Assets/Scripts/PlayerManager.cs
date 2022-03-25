using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerCamera playerCamera;
    private InputManager inputManager;
    private PlayerLocomotionManager playerLocomotionManager;
    public PlayerEquipmentManager playerEquipmentManager;
    private Animator animator;
    private AnimatorManager animatorManager;

    [Header("Player Flags")]
    public bool disableRootMotion;
    public bool isPerformingAction;
    public bool isPerformingQuickTurn;
    public bool isAiming;

    private void Awake()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
        inputManager = GetComponent<InputManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        animator = GetComponent<Animator>();
        animatorManager = GetComponent<AnimatorManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        disableRootMotion = animator.GetBool("disableRootMotion");
        isAiming = animator.GetBool("isAiming");
        isPerformingAction = animator.GetBool("isPerformingAction");
        isPerformingQuickTurn = animator.GetBool("isPerformingQuickTurn");
    }

    private void FixedUpdate() // due to movement on a rigidbody, we have to use fix update
    {
        playerLocomotionManager.HandleAllLocomotion();
    }

    private void LateUpdate()
    {
        playerCamera.HandleAllCameraMovement();
    }

    public void UseCurrentWeapon()
    {
        if (isPerformingAction)
        {
            return;
        }
        //animatorManager.PlayAnimationWithOurRootMotion("Pistol_Shoot", true);
        playerEquipmentManager.weaponAnimator.ShootWeapon(playerCamera);
    }
}
