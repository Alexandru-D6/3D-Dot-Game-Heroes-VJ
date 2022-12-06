using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Processors;

public class BoomerangScript : MonoBehaviour {

    #region Parameters

    [Header("Boomerang Stabilizer")]
    [SerializeField] private Vector3 defaultRotation;
    private Transform giroCoconutTransform;
    [SerializeField] private RotationConstraint rotationConstraint;

    [Header("Boomerang Reference")]
    [SerializeField] private GameObject sceneObjects;
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject player;
    [SerializeField] private FollowAnchor followAnchorScript;
    [SerializeField] private Animator animator;

    [Header("Boomerang Parameters")]
    [SerializeField] private bool isFlying;
    [SerializeField] private bool isReturning;
    [SerializeField] private float maxFlyDistance;
    [SerializeField] private float flyHeight;
    [SerializeField] private float velocity;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 boomerangDirection;


    #endregion

    #region Collision Methods

    private void OnTriggerEnter(Collider other) {

        if (other.tag.Equals("Wall")) {
            isReturning = true;
        }else if (other.tag.Equals("Player") && isReturning) {
            animator.SetBool("Flying", false);
            transform.parent = playerHand.transform;
            rotationConstraint.constraintActive = true;
            followAnchorScript.enabled = true;
            isFlying = false;
            isReturning = false;
        }
    }

    #endregion

    #region IEnumerators

    IEnumerator debugRoutine(float time) {
        yield return new WaitForSeconds(time);
        transform.parent = playerHand.transform;
        rotationConstraint.constraintActive = true;
        followAnchorScript.enabled = true;
        isFlying = false;
        isReturning = false;
    }

    #endregion

    #region Public Methods (Abstract probably)

    public void weaponCollided() {
        if (isFlying) isReturning = true;
    }

    public void Attack() {
        // Change parent of boomerang, set new height and disable stabilizer
        rotationConstraint.constraintActive = false;
        transform.parent = sceneObjects.transform;
        setPosition(transform, y: flyHeight);
        followAnchorScript.enabled = false;

        // Save necesary values
        originalPosition = transform.localPosition;
        boomerangDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * player.transform.localEulerAngles.y), 0.0f, -1.0f * Mathf.Sin(Mathf.Deg2Rad * player.transform.localEulerAngles.y));
        normalizeVector(ref boomerangDirection);

        isFlying = true;
        animator.SetBool("Flying", true);
    }

    #endregion

    #region Private Methods

    private void controlBoomerang() {
        if (isFlying) {
            translate(boomerangDirection);

            if (!isReturning) isReturning = Vector3.Distance(transform.localPosition, originalPosition) >= maxFlyDistance;

            if (isReturning) boomerangDirection = calculateDirection(transform.localPosition, player.transform.localPosition);
        }
    }

    private void normalizeVector(ref Vector3 vector) {
        for (int i = 0; i < 3; ++i) {
            if (Mathf.Abs(vector[i]) >= 0.9) vector[i] = Mathf.Sign(vector[i]) * 1.0f;
            else vector[i] = 0.0f;
        }
    }

    private Vector3 calculateDirection(Vector3 origin, Vector3 Destination) {
        Vector3 dir = (Destination - origin).normalized;
        dir.y = 0.0f;
        return dir;
    }

    private void restoreDefaultRotation() {
        // Setting up y rotation (the one whose relative to the player rotation)
        Vector3 tmp = transform.localEulerAngles;
        tmp.y = defaultRotation.y;
        transform.localEulerAngles = tmp;
    }

    private void translate(Vector3 direction) {
        Vector3 tmp = transform.localPosition;

        tmp += direction * velocity * Time.deltaTime;

        transform.localPosition = tmp;
    }

    private void setPosition(Transform transform, float x = float.NaN, float y = float.NaN, float z = float.NaN) {
        Vector3 tmp = transform.localPosition;

        if (!float.IsNaN(x)) tmp.x = x;
        if (!float.IsNaN(y)) tmp.y = y;
        if (!float.IsNaN(z)) tmp.z = z;

        transform.localPosition = tmp;
    }

    #endregion

    #region MonoBehaviour Methods

    void Start() {
        #region Setting up GiroCoconut
        giroCoconutTransform = GameObject.FindGameObjectWithTag("GiroCoconut").transform;

        // TODO: Make this change whenever this weapon is selected, as this rotation is useless for the sword
        Vector3 tmp2 = giroCoconutTransform.transform.localEulerAngles;
        tmp2.z = 0.0f;
        giroCoconutTransform.transform.localEulerAngles = tmp2;

        ConstraintSource tmp = new ConstraintSource();
        tmp.sourceTransform = giroCoconutTransform;
        tmp.weight = 1;

        rotationConstraint.SetSource(0, tmp);
        rotationConstraint.constraintActive = false;
        #endregion

        restoreDefaultRotation();

        playerHand = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        sceneObjects = GameObject.FindGameObjectWithTag("SceneObjects");
    }

    void Update() {
        controlBoomerang();
    }

    #endregion

}
