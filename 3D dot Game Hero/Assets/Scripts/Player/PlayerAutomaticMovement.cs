using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutomaticMovement : MonoBehaviour {

    #region Parameters

    [Header("References")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Config")]
    [SerializeField] private Vector2 moveSpeed;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool startMovement;

    #endregion

    #region IEnumerators

    IEnumerator delayedPlayerDisable(float time) {
        yield return new WaitForSeconds(time);
        playerInput.enabled = false;
    }

    #endregion

    #region Private Methods

    private void movementRoutine() {
        if (startMovement) {
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0.0f;
            direction.Normalize();

            transform.Translate(new Vector3(direction.x * moveSpeed.x, 0.0f, direction.z * moveSpeed.y) * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, targetPosition) < 0.5f) {
                startMovement = false;
                playerInput.disableInput(true);
            }
        }
    }

    #endregion

    #region Public Methods

    public void MoveTo(Vector4 target) {
        targetPosition = target;
        startMovement = true;
        playerInput.disableInput(false);
    }

    #endregion

    #region MonoBehaviour Methods

    private void Start() {
        moveSpeed = playerInput.GetMovementSpeed();
    }

    private void Update() {
        movementRoutine();
    }

    #endregion
}
