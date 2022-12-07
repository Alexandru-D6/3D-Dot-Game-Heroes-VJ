using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreRigidbodyInfo : MonoBehaviour {

    #region Parameters

    [SerializeField] bool velocity;
    [SerializeField] bool angularVelocity;
    [SerializeField] bool inertiaTensor;
    [SerializeField] bool inertiaTensorRotation;
    [SerializeField] bool centerMass;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 initialLocalPos;

    #endregion

    #region MonoBehaviour Methods

    private void Start() {
        initialLocalPos = transform.localPosition;
    }

    private void Update() {
        Vector3 zero = Vector3.zero;

        if (velocity) rb.velocity = zero;
        if (angularVelocity) rb.angularVelocity = zero;
        if (inertiaTensor) rb.inertiaTensor = zero;
        if (inertiaTensorRotation) rb.inertiaTensorRotation = Quaternion.identity;

        if (centerMass) {
            transform.localPosition = initialLocalPos;
            rb.ResetCenterOfMass();
        }
    }

    #endregion

}
