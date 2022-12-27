using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeaponManager : WeaponManager {

#region Abstract Methods

    public override void UseCurrentWeapon() {
        if (currentWeapon != null) {
            //TODO: add here the animation of attack, in case of using animationManager change the cast
            ((PlayerAnimations)animationManager).AttackStart();
            currentWeapon.GetComponent<WeaponScript>().Attack();
        }
    }

    public override void AttackFinished() {
        //TODO: add here the animation of attack return, in case of using animationManager change the cast
        ((PlayerAnimations)animationManager).AttackReturn();
    }

#endregion

#region MonoBehaviour Methods

    void Start() {
        SelectWeapon(Tags.Bow);
    }

#endregion
}
