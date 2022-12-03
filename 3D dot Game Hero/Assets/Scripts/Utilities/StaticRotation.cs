using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticRotation : MonoBehaviour {

    public bool lockRotation { get; set; }
    public bool debugrotation = false;

    [SerializeField] private Vector3 initialRotation;
    [SerializeField] private Vector3 lockedRotation;

    [SerializeField] private Transform weaponAnchor;
    [SerializeField] private Transform shaftAnchor;

    public float maxRotation;
    public float minRotation;
    public bool restartStats;

    private void Start() {
        weaponAnchor = transform.parent.Find("WeaponAnchor").transform;
        transform.eulerAngles = initialRotation;
        transform.localEulerAngles = initialRotation;
    }

    void Update() {
        if (restartStats) {
            restartStats = false;
            maxRotation = float.MinValue;
            minRotation = float.MaxValue;
        }

        if (transform.eulerAngles.z > maxRotation) maxRotation = transform.eulerAngles.z;
        if (transform.eulerAngles.z < minRotation) minRotation = transform.eulerAngles.z;

        transform.Translate(weaponAnchor.position - shaftAnchor.position, Space.World);

        lockRotation = debugrotation;
    }
}
