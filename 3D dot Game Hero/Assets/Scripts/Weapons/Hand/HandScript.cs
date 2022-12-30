using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : WeaponScript {

#region Parameters

    [Header("Hand Parameters")]
    [SerializeField] private float delayAttack;

#endregion

#region IEnumerators

    IEnumerator delayAttackFinished(float time) {
        yield return new WaitForSeconds(time);
        weaponManager.AttackFinished();
    }

#endregion

#region Abstract Methods

    public override void Attack() {
        StartCoroutine(delayAttackFinished(delayAttack));
    }

    public override void Release() {
        // DO NOTHING
    }

    public override void Abort() {
        // DO NOTHING
    }

    public override void Collided() {
    }

    public override void Start() {
        base.Start();

        usesLeft = int.MaxValue;
    }

#endregion

}
