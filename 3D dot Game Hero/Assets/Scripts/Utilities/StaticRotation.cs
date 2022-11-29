using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticRotation : MonoBehaviour {

    private bool lockRotation { get; set; }
    [SerializeField] public bool lock_;

    [SerializeField] private Vector3 initialRotation;
    [SerializeField] private Vector3 lockedRotation;

    [SerializeField] private Transform weaponAnchor;
    [SerializeField] private Transform shaftAnchor;

    private void Start() {
        weaponAnchor = transform.parent.Find("WeaponAnchor").transform;
        transform.eulerAngles = initialRotation;
        transform.localEulerAngles = initialRotation;
    }

    void Update() {
        lockRotation = lock_;

        // Global
        Vector3 temp = transform.eulerAngles;
        if (lockRotation) temp.z = lockedRotation.z;
        transform.eulerAngles = temp;

        transform.Translate(weaponAnchor.position - shaftAnchor.position, Space.World);
    }
}
