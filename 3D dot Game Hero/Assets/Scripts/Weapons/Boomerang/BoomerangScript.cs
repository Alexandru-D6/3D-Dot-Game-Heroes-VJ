using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BoomerangScript : MonoBehaviour {

    #region Parameters

    [Header("Boomerang Stabilizer")]
    [SerializeField] private Vector3 defaultRotation;
    private Transform giroCoconutTransform;
    [SerializeField] private RotationConstraint rotationConstraint;

    #endregion

    #region Public Methods (Abstract probably)

    public void weaponCollided() {
    }

    public void Attack() {
        rotationConstraint.constraintActive = true;
    }

    #endregion

    #region Private Methods

    private void restoreDefaultRotation() {
        // Setting up y rotation (the one whose relative to the player rotation)
        Vector3 tmp = transform.localEulerAngles;
        tmp.y = defaultRotation.y;
        transform.localEulerAngles = tmp;
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
    }

    void Update() {
        //restoreDefaultRotation();
    }

    #endregion

}
