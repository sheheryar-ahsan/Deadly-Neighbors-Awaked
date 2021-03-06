using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private AnimatorManager animatorManager;
    private Animator animator;
    private PlayerManager playerManager;
    private PlayerUIManager playerUIManager;

    [Header("Player Movement")]
    public float verticalMovementInput;
    public float horizontalMovementInput;
    private Vector2 movementInput; // raw input from player control

    [Header("Camera Rotation")]
    public float verticalCameraInput;
    public float horizontalCameraInput;
    private Vector2 cameraInput;

    [Header("Button Inputs")]
    public bool runInput;
    public bool quickTurnInput;
    public bool aimingInput;
    public bool shootInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // on pressing WSAD keys (player movement), taking values and assigning to movement input
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            // on mouse movement, taking values and assigning to camera input
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            // on pressing left shif key, the run input becomes true
            playerControls.PlayerMovement.Run.performed += i => runInput = true;
            // on let go left shif key, the run input becomes false
            playerControls.PlayerMovement.Run.canceled += i => runInput = false;
            // on pressing Q key
            playerControls.PlayerMovement.QuickTurn.performed += i => quickTurnInput = true;
            // on pressing right mouse button
            playerControls.PlayerActions.Aim.performed += i => aimingInput = true;
            // on let go right mouse button
            playerControls.PlayerActions.Aim.canceled += i => aimingInput = false;
            // on pressing left mouse button
            playerControls.PlayerActions.Shoot.performed += i => shootInput = true;
            // on let go left mouse button
            playerControls.PlayerActions.Shoot.canceled += i => shootInput = false;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
        HandleQuickTurnInput();
        HandleAimingInput();
        HandleShootingInput();
    }

    private void HandleMovementInput()
    {
        // assigning the horizontal & vertical movement input values got from player control (keys / joyStick)
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animatorManager.HandleAnimatorValue(horizontalMovementInput, verticalMovementInput, runInput);

        // temp
        if (verticalMovementInput != 0 || horizontalMovementInput != 0)
        {
            animatorManager.rightHandIK.weight = 0;
            animatorManager.leftHandIK.weight = 0;
        }
        else
        {
            animatorManager.rightHandIK.weight = 1;
            animatorManager.leftHandIK.weight = 1;
        }
    }

    private void HandleCameraInput()
    {
        horizontalCameraInput = cameraInput.x;
        verticalCameraInput = cameraInput.y;
    }

    private void HandleQuickTurnInput()
    {
        if (playerManager.isPerformingAction) // not to do quick turn if there is already a action playing
        {
            return;
        }
        if (quickTurnInput)
        {
            animator.SetBool("isPerformingQuickTurn", true);
            animatorManager.PlayAnimationWithOurRootMotion("Quick Turn", true);
        }
    }

    private void HandleAimingInput()
    {
        // for not letting aim during movement
        if (verticalMovementInput != 0 || horizontalMovementInput !=0)
        {
            aimingInput = false;
            animator.SetBool("isAiming", false);
            playerUIManager.crossHair.SetActive(false);
            return;
        }

        if (aimingInput)
        {
            animator.SetBool("isAiming", true);
            playerUIManager.crossHair.SetActive(true);
        }
        else
        {
            animator.SetBool("isAiming", false);
            playerUIManager.crossHair.SetActive(false);
        }

        animatorManager.UpdateAimConstraints();
    }

    private void HandleShootingInput()
    {
        if (shootInput && aimingInput)
        {
            shootInput = false;
            playerManager.UseCurrentWeapon();
            // shoot current weapon
        }
    }
}
