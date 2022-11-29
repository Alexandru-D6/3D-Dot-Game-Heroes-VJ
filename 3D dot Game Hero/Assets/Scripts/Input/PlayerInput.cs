using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;

public class PlayerInput : MonoBehaviour {

    enum rotationStates { Backward, RightBack, Right, RightFront, Forward, LeftFront, Left, LeftBack};

    private PlayerInputActions playerControls;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private Vector2 fireDirection = Vector2.zero;
    [SerializeField] private Vector2 moveSpeed;
    private InputAction move;
    private InputAction fire;

    [SerializeField] rotationStates currentRotation = rotationStates.Forward;
    [SerializeField] private float rotationSpeed;

    private void Awake() {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable() {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable() {
        move.Disable();
        fire.Disable();
    }

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

    private void movementRoutine() {
        transform.Translate(new Vector3(    ((moveDirection.y != 0.0f) ? Mathf.Sign(moveDirection.y) : 0.0f) * moveSpeed.x, 
                                            0.0f, 
                                            -1.0f * ((moveDirection.x != 0.0f) ? Mathf.Sign(moveDirection.x) : 0.0f) * moveSpeed.y) 
                            * Time.deltaTime, Space.World);

        setRotation();
        playerAnimator.SetBool("Running", moveDirection != Vector2.zero);
    }

    private void rotationRoutine() {
        float angleDest = -180.0f + ((int)currentRotation * 45.0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, angleDest, 0.0f), rotationSpeed);
    }

    void Start(){
    }

    void Update() {
        moveDirection = move.ReadValue<Vector2>();
        fireDirection = fire.ReadValue<Vector2>();

        movementRoutine();
        rotationRoutine();
    }

    private void Fire(InputAction.CallbackContext context) {
        Debug.Log("Fire");
        //fireDirection = fire.ReadValue<Vector2>();
    }
}
