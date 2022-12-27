using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeaponManager : WeaponManager {

#region Abstract Methods

    public override void UseCurrentWeapon() {
        if (currentWeapon != null) {

            currentWeapon.GetComponent<WeaponScript>().Attack();
        }
    }

    public override void AttackFinished() {

    }

#endregion

#region MonoBehaviour Methods

    void Start() {
        SelectWeapon(Tags.Bow);
    }

#endregion
}
