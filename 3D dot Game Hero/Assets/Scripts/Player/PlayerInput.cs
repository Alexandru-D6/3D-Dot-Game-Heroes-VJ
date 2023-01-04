using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;
using UnityEditor;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour {

    #region Parameters

    enum rotationStates { Backward, RightBack, Right, RightFront, Forward, LeftFront, Left, LeftBack };

    [Header("Managers")]
    [SerializeField] private PlayerWeaponManager playerWeaponManager;
    [SerializeField] private ShieldScript shieldScript;
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private Rigidbody rb;

    [Space(10)]

    [Header("Controls")]
    private PlayerInputActions playerControls;
    private InputAction fire;
    private InputAction move;
    private InputAction numericButtons;
    private InputAction godMode;
    private InputAction dash;
    private InputAction shield;
    private bool canFire;
    private bool canNumericButton;
    private bool canDash;

    [Space(10)]

    [Header("Controls parameters")]
    [SerializeField] private float fireDelay;
    [SerializeField] private float numericButtonDelay;
    [SerializeField] private float dashDelay;
    [Space(2)]
    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private Vector2 moveSpeed;
    [Space(2)]
    [SerializeField] rotationStates currentRotation = rotationStates.Forward;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 dashVelocities;
    private bool inputEnabled = true;

    #endregion

    #region IEnumerators

    IEnumerator delayedFire(float time) {
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    IEnumerator delayedNumericButton(float time) {
        yield return new WaitForSeconds(time);
        canNumericButton = true;
    }

    IEnumerator delayedDashButton(float time) {
        yield return new WaitForSeconds(time);
        canDash = true;
    }

    #endregion

    #region Private Methods

    private void setRotation(Vector2 dir) {
        if (dir.x < 0.0f) {
            if (dir.y > 0.0f) currentRotation = rotationStates.RightFront;
            else if (dir.y < 0.0f) currentRotation = rotationStates.RightBack;
            else currentRotation = rotationStates.Right;
        } else if (dir.x > 0.0f) {
            if (dir.y > 0.0f) currentRotation = rotationStates.LeftFront;
            else if (dir.y < 0.0f) currentRotation = rotationStates.LeftBack;
            else currentRotation = rotationStates.Left;
        } else {
            if (dir.y > 0.0f) currentRotation = rotationStates.Forward;
            else if (dir.y < 0.0f) currentRotation = rotationStates.Backward;
        }
    }

    private void setTranslation() {
        Vector3 translation = new Vector3(  /*x*/    moveDirection.x * moveSpeed.x,
                                            /*y*/    0.0f,
                                            /*z*/    moveDirection.y * moveSpeed.y);

        transform.Translate(translation * Time.deltaTime, Space.World);
    }

    private void movementRoutine() {
        setTranslation();
        setRotation(moveDirection);

        SoundManager.Instance.PlayPlayerWalking(moveDirection != Vector2.zero);
        animationManager.enableRunning(moveDirection != Vector2.zero);
    }

    private void rotationRoutine() {
        float angleDest = -180.0f + ((int)currentRotation * 45.0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, angleDest, 0.0f), rotationSpeed);
    }

    #endregion

    #region Public Methods

    public void PI_AttackReturned() {
        canFire = true;
    }

    public void PI_resetFire() {
        canFire = true;
    }

    public Vector2 GetMovementSpeed() {
        return moveSpeed;
    }

    public void RotatePlayer(Vector3 target) {
        Vector3 dir = target - transform.position;
        dir.y = 0.0f;
        dir.Normalize();

        setRotation(dir);
        
        float angleDest = -180.0f + ((int)currentRotation * 45.0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, angleDest, 0.0f), 360.0f);

        animationManager.enableRunning(true);
    }

    public void disableInput(bool value) {
        inputEnabled = value;
    }

    #endregion

    #region MonoBehaviour Methods

    private void Awake() {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable() {
        // Assign and enable all controls
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.started += Fire;
        fire.canceled += Release;

        numericButtons = playerControls.Player.NumericButtons;
        numericButtons.Enable();
        numericButtons.started += NumericalButtons;

        godMode = playerControls.Player.GodMode;
        godMode.Enable();
        godMode.started += EnableGodMode;

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.started += DashButtons;

        shield = playerControls.Player.Shield;
        shield.Enable();
        shield.started += EnableShield;

        shield = playerControls.Player.Shield;
        shield.Enable();
        shield.canceled += DisableShield;
    }

    private void OnDisable() {
        move.Disable();
        fire.Disable();
        godMode.Disable();
        numericButtons.Disable();
        shield.Disable();
    }

    void Start(){
        canFire = true;
        canNumericButton = true;
        canDash = true;
    }

    void Update() {
        if (inputEnabled) {
            moveDirection = move.ReadValue<Vector2>();

            movementRoutine();
            rotationRoutine();
        }
    }

    #endregion

    #region Callbacks Functions

    public void Fire(InputAction.CallbackContext context) {
        if (canFire) {
            canFire = false;
            playerWeaponManager.UseCurrentWeapon();
        }
    }

    public void Release(InputAction.CallbackContext context) {
        playerWeaponManager.ReleaseCurrentWeapon();
    }

    public void NumericalButtons(InputAction.CallbackContext context) {
        if (canNumericButton) {
            canNumericButton = false;

            switch(context.control.path) {
                case "/Keyboard/1":
                    playerWeaponManager.SelectWeapon(Tags.Sword);
                    break;
                case "/Keyboard/2":
                    playerWeaponManager.SelectWeapon(Tags.Boomerang);
                    break;
                case "/Keyboard/3":
                    playerWeaponManager.SelectWeapon(Tags.Bow);
                    break;
                case "/Keyboard/4":
                    playerWeaponManager.SelectWeapon(Tags.Bomb);
                    break;
                case "/Keyboard/5":
                case "/Keyboard/6":
                case "/Keyboard/7":
                case "/Keyboard/8":
                case "/Keyboard/9":
                case "/Keyboard/0":
                    break;
            }

            StartCoroutine(delayedNumericButton(fireDelay));
        }
    }

    public void EnableGodMode(InputAction.CallbackContext context) {
        PlayerManager.Instance.SwitchGodMode();
    }

    public void DashButtons(InputAction.CallbackContext context) {
        if (canDash) {
            canDash = false;
            SoundManager.Instance.PlayDashSound();

            rb.velocity += new Vector3(moveDirection.x * dashVelocities.x,
                                        0.0f,
                                        moveDirection.y * dashVelocities.z);

            StartCoroutine(delayedDashButton(dashDelay));
        }
    }

    public void EnableShield(InputAction.CallbackContext context) {
        if (canFire) {
            canDash = false;
            canFire = false;

            shieldScript.EnableShield();
        }
    }

    public void DisableShield(InputAction.CallbackContext context) {
        canDash = true;
        canFire = true;
        shieldScript.DisableShield();
    }

    #endregion

}
