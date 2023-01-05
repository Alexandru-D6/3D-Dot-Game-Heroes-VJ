using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Processors;

public class BoomerangScript : WeaponScript {

#region Parameters

    [Header("Boomerang Reference")]
    [SerializeField] private GameObject sceneObjects;
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject player;
    [SerializeField] private FollowAnchor followAnchorScript;
    [SerializeField] private BoomerangAnimations boomerangAnimations;

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
            boomerangAnimations.enableFlying(false);
            isFlying = false;
            isReturning = false;

            transform.parent = playerHand.transform;
            followAnchorScript.enabled = true;

            AttackFinished();
        }
    }

#endregion

#region IEnumerators

#endregion

#region Abstract Methods

    public override void Attack() {
        // Change parent of boomerang, set new height and disable stabilizer
        transform.parent = sceneObjects.transform;
        SetPosition(transform, y: flyHeight);
        followAnchorScript.enabled = false;

        // Save necesary values
        originalPosition = transform.localPosition;
        boomerangDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * player.transform.localEulerAngles.y), 0.0f, Mathf.Cos(Mathf.Deg2Rad * player.transform.localEulerAngles.y));
        NormalizeVector(ref boomerangDirection);

        isFlying = true;
        boomerangAnimations.enableFlying(true);

        LockAxis(true, true, true);

        SoundManager.Instance.PlayPlayerBoomerang();
    }

    public override void Release() {
        // DO NOTHING
    }

    public override void Abort() {
        // DO NOTHING
    }

    public override void Collided() {
        if(isFlying) isReturning = true;
    }

    #endregion

    #region Virtual Methods

    public override void AttackFinished() {
        base.AttackFinished();

        LockAxis(true, false, false);
    }

    public override void RestartState() {
        LockAxis(true, false, false);

        boomerangAnimations.enableFlying(false);
        isFlying = false;
        isReturning = false;

        transform.parent = playerHand.transform;
        followAnchorScript.enabled = true;
    }

    #endregion

    #region Private Methods

    private void ControlBoomerang() {
        if (isFlying) {
            Translate(boomerangDirection);

            if (!isReturning) isReturning = Vector3.Distance(transform.localPosition, originalPosition) >= maxFlyDistance;

            if (isReturning) boomerangDirection = CalculateDirection(transform.localPosition, player.transform.localPosition);
        }
    }

    private void NormalizeVector(ref Vector3 vector) {
        for (int i = 0; i < 3; ++i) {
            if (Mathf.Abs(vector[i]) >= 0.5f) vector[i] = Mathf.Sign(vector[i]) * 1.0f;
            else vector[i] = 0.0f;
        }
    }

    private Vector3 CalculateDirection(Vector3 origin, Vector3 Destination) {
        Vector3 dir = (Destination - origin).normalized;
        dir.y = 0.0f;
        return dir;
    }

    private void LockAxis(bool x, bool y, bool z) {
        rotationConstraint.enabled = false;
        Axis lockedAxis = Axis.None | ((x == true) ? Axis.X : Axis.None) | ((y == true) ? Axis.Y : Axis.None) | ((z == true) ? Axis.Z : Axis.None);
        rotationConstraint.rotationAxis = lockedAxis;

        transform.localEulerAngles = defaultRotation;
        rotationConstraint.enabled = true;
    }

    private void Translate(Vector3 direction) {
        Vector3 tmp = transform.localPosition;

        tmp += direction * velocity * Time.deltaTime;

        transform.localPosition = tmp;
    }

    private void SetPosition(Transform transform, float x = float.NaN, float y = float.NaN, float z = float.NaN) {
        Vector3 tmp = transform.localPosition;

        if (!float.IsNaN(x)) tmp.x = x;
        if (!float.IsNaN(y)) tmp.y = y;
        if (!float.IsNaN(z)) tmp.z = z;

        transform.localPosition = tmp;
    }

#endregion

#region MonoBehaviour Methods

    public override void Start() {
        base.Start();

        usesLeft = int.MaxValue;

        playerHand = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        sceneObjects = GameObject.FindGameObjectWithTag("SceneObjects");

        LockAxis(true, false, false);
    }

    void Update() {
        ControlBoomerang();
    }

#endregion

}
