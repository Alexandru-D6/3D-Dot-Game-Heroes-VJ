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

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 initialLocalPos;

    #endregion

    #region MonoBehaviour Methods

    private void Start() {
        initialLocalPos = transform.localPosition;
    }

    private void Update() {
        Vector3 zero = Vector3.zero;

        if (velocity) _rigidbody.velocity = zero;
        if (angularVelocity) _rigidbody.angularVelocity = zero;
        if (inertiaTensor) _rigidbody.inertiaTensor = zero;
        if (inertiaTensorRotation) _rigidbody.inertiaTensorRotation = Quaternion.identity;

        if (centerMass) {
            transform.localPosition = initialLocalPos;
            _rigidbody.ResetCenterOfMass();
        }
    }

    #endregion

}
