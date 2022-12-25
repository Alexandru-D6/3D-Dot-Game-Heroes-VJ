using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class WeaponScript : MonoBehaviour {

#region Parameters

    [Header("Managers")]
    [SerializeField] protected WeaponManager weaponManager;

    [Header("Weapon Stabilizer")]
    [SerializeField] protected Vector3 defaultRotation;
    protected Transform giroCoconutTransform;
    [SerializeField] protected RotationConstraint rotationConstraint;

    [Header("Weapon Parameters")]
    [SerializeField] protected string weaponName;
    [SerializeField] protected int usesLeft;
    [SerializeField] protected int levelOfPower;

#endregion

#region Abstract Methods

    public abstract void Attack();
    public abstract void Release();
    public abstract void Collided();

#endregion

#region Virtual Methods

    public virtual void SetLevelOfPower(int level) {
        levelOfPower = 0;
    }

    public virtual int GetLevelOfPower() {
        return levelOfPower;
    }

    public virtual void AttackFinished() {
        weaponManager.AttackFinished();
    }

    public virtual void RestartState() {
        return;
    }

#endregion

#region Public Methods

    public void SetActive(bool value) {
        gameObject.SetActive(value);
    }

    public void DecrementUses() {
        usesLeft--;
    }

    public void IncrementUses() {
        usesLeft++;
    }

    public int GetLeftUses() {
        return usesLeft;
    }

    public string GetName() {
        return weaponName;
    }

    public void SetWeaponManager(WeaponManager wp) {
        weaponManager = wp;
    }

#endregion

#region MonoBehaviour Methods

    public virtual void Start() {
        weaponName = gameObject.tag;
        usesLeft = 3;
        levelOfPower = 0;

        #region Setting up GiroCoconut
        giroCoconutTransform = GameObject.FindGameObjectWithTag("GiroCoconut").transform;

        ConstraintSource tmp = new ConstraintSource();
        tmp.sourceTransform = giroCoconutTransform;
        tmp.weight = 1;

        if (rotationConstraint.sourceCount != 0) rotationConstraint.SetSource(0,tmp);
        else rotationConstraint.AddSource(tmp);

        #endregion
    }

#endregion

}
