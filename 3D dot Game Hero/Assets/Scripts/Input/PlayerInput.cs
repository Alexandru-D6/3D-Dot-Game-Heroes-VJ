using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour {

    [SerializeField] private PlayerInputActions playerControls;

    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private Vector2 fireDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;

    private void Awake() {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable() {
        move = playerControls.Player.Move;
        move.Enable();
        move.performed += Movement;

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable() {
        move.Disable();
        fire.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Fire(InputAction.CallbackContext context) {
        Debug.Log("Fire");
        fireDirection = fire.ReadValue<Vector2>();
    }

    private void Movement(InputAction.CallbackContext context) {
        Debug.Log("Move");
        moveDirection = move.ReadValue<Vector2>();
    }
}
