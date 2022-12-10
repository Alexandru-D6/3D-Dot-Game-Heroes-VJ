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
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private AnimationManager animationManager;

    [Space(10)]

    [Header("Controls")]
    private PlayerInputActions playerControls;
    private InputAction fire;
    private InputAction move;
    private InputAction numericButtons;
    private bool canFire;
    private bool canNumericButton;

    [Space(10)]

    [Header("Controls parameters")]
    [SerializeField] private float fireDelay;
    [SerializeField] private float numericButtonDelay;
    [Space(2)]
    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private Vector2 moveSpeed;
    [Space(2)]
    [SerializeField] rotationStates currentRotation = rotationStates.Forward;
    [SerializeField] private float rotationSpeed;

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

    #endregion

    #region Private Methods

    private void setRotation() {
        if (moveDirection.x < 0.0f) {
            if (moveDirection.y > 0.0f) currentRotation = rotationStates.RightFront;
            else if (moveDirection.y < 0.0f) currentRotation = rotationStates.RightBack;
            else currentRotation = rotationStates.Right;
        } else if (moveDirection.x > 0.0f) {
            if (moveDirection.y > 0.0f) currentRotation = rotationStates.LeftFront;
            else if (moveDirection.y < 0.0f) currentRotation = rotationStates.LeftBack;
            else currentRotation = rotationStates.Left;
        } else {
            if (moveDirection.y > 0.0f) currentRotation = rotationStates.Forward;
            else if (moveDirection.y < 0.0f) currentRotation = rotationStates.Backward;
        }
    }

    private void setTranslation() {
        Vector3 translation = new Vector3(  /*x*/    ((moveDirection.y != 0.0f) ? Mathf.Sign(moveDirection.y) : 0.0f) * moveSpeed.x,
                                            /*y*/    0.0f,
                                            /*z*/    -1.0f * ((moveDirection.x != 0.0f) ? Mathf.Sign(moveDirection.x) : 0.0f) * moveSpeed.y);

        transform.Translate(translation * Time.deltaTime, Space.World);
    }

    private void movementRoutine() {
        setTranslation();
        setRotation();

        animationManager.enableRunning(moveDirection != Vector2.zero);
    }

    private void rotationRoutine() {
        float angleDest = -90.0f + ((int)currentRotation * 45.0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, angleDest, 0.0f), rotationSpeed);
    }

    #endregion

    #region Public Methods

    public void PI_AttackReturned() {
        canFire = true;
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
        fire.performed += Fire;

        numericButtons = playerControls.Player.NumericButtons;
        numericButtons.Enable();
        numericButtons.performed += NumericalButtons;
    }

    private void OnDisable() {
        move.Disable();
        fire.Disable();
        numericButtons.Disable();
    }

    void Start(){
        canFire = true;
        canNumericButton = true;
    }

    void Update() {
        moveDirection = move.ReadValue<Vector2>();

        movementRoutine();
        rotationRoutine();
    }

    #endregion

    #region Callbacks Functions

    public void Fire(InputAction.CallbackContext context) {
        if (canFire) {
            canFire = false;
            weaponManager.UseCurrentWeapon();
        }
    }

    public void NumericalButtons(InputAction.CallbackContext context) {
        if (canNumericButton) {
            canNumericButton = false;

            switch(context.control.path) {
                case "/Keyboard/1":
                    weaponManager.SelectWeapon(Tags.Sword);
                    break;
                case "/Keyboard/2":
                    weaponManager.SelectWeapon(Tags.Boomerang);
                    break;
                case "/Keyboard/3":
                case "/Keyboard/4":
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

    #endregion

}
