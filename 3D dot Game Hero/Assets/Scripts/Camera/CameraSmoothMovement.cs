using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothMovement : MonoBehaviour {
    #region Parameters

    [Header("Config")]
    [SerializeField] private Vector3 moveSpeed;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool startMovement;

    #endregion

    #region Private Methods

    private void movementRoutine() {
        if (startMovement) {
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();

            if (Vector3.Distance(transform.position, targetPosition) < moveSpeed.x * Time.deltaTime) {
                transform.position = targetPosition;
                startMovement = false;
                return;
            }

            transform.Translate(new Vector3(direction.x * moveSpeed.x, direction.y * moveSpeed.y, direction.z * moveSpeed.y) * Time.deltaTime, Space.World);
        }
    }

    #endregion

    #region Public Methods

    public void MoveTo(Vector3 target) {
        targetPosition = target;
        startMovement = true;
    }

    #endregion

    #region MonoBehaviour Methods

    private void Update() {
        movementRoutine();
    }

    #endregion
}
