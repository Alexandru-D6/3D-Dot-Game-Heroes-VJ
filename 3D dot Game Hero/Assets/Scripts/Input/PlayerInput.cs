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
        transform.Translate(new Vector3(moveDirection.y * moveSpeed.x, 0.0f, -1.0f * moveDirection.x * moveSpeed.y) * Time.deltaTime, Space.World);
        setRotation();

        playerAnimator.SetBool("Running", moveDirection != Vector2.zero);
    }

    /**
     * Both angle1 and angle2 should go with a range of [-180,180)
     * @params angle1 
     **/
    /// <summary> Return the direction to the shortest rotation into destination </summary>
    /// <param name="origin"> Origin of the rotation</param>
    /// <param name="destination"> Destination of the rotation</param>
    /// <remarks> Both angle should have a range of [-180,180)</remarks>
    private float getShortestSign(float origin, float destination) {

        return 0.0f;
    }

    private void rotationRoutine() {
        float angle = -180.0f + ((int)currentRotation * 45.0f);
        getShortestSign(angle, angle);
        transform.rotation = Quaternion.Euler(new Vector3(0.0f,angle,0.0f));
        /*if (currentRotation == 0) angle = (transform.rotation.y > 180.0f ? 360.0f : 0.0f);

        float _partialRotation = angle - transform.rotation.y;
        float sign = _partialRotation >= 0.0f ? 1.0f : -1.0f;

        Vector3 _rotation = new Vector3(0.0f,Mathf.Abs(_partialRotation) > rotationSpeed ? sign * rotationSpeed * Time.deltaTime : _partialRotation * Time.deltaTime,0.0f);
        
        transform.Rotate(_rotation, Space.World);*/
    }

    void Start()
    {
        
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
