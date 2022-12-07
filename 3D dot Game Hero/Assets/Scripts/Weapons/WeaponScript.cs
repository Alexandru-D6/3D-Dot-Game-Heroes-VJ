using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class WeaponScript : MonoBehaviour {

#region Parameters

    [Header("Weapon Stabilizer")]
    [SerializeField] protected Vector3 defaultRotation;
    protected Transform giroCoconutTransform;
    [SerializeField] protected RotationConstraint rotationConstraint;

    [Header("Weapon Parameters")]
    [SerializeField] protected int usesLeft;
    [SerializeField] protected int levelOfPower;

#endregion

#region Abstract Methods

    public abstract void Attack();
    public abstract void Collided();

#endregion

#region Virtual Methods

    public virtual void SetLevelOfPower(int level) {
        levelOfPower = 0;
    }

    public virtual int GetLevelOfPower() {
        return levelOfPower;
    }

#endregion

#region Public Methods

    public void SetActive(bool value) {
        gameObject.SetActive(value);
    }

    public void decrementUses() {
        usesLeft--;
    }

#endregion

#region MonoBehaviour Methods

    public virtual void Start() {
        usesLeft = 3;
        levelOfPower = 0;

        #region Setting up GiroCoconut
        giroCoconutTransform = GameObject.FindGameObjectWithTag("GiroCoconut").transform;

        ConstraintSource tmp = new ConstraintSource();
        tmp.sourceTransform = giroCoconutTransform;
        tmp.weight = 1;

        rotationConstraint.SetSource(0,tmp);
        rotationConstraint.constraintActive = false;
        #endregion
    }

#endregion

}
