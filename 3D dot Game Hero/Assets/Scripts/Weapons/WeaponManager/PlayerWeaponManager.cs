using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : WeaponManager {

    #region Parameters

    [Header("Managers")]
    [SerializeField] private PlayerInput playerInput;

#endregion

#region Inventory Management

    protected override bool RevealSpawnedWeapon(Tags weapon) {

        playerInput.PI_resetFire();

        return base.RevealSpawnedWeapon(weapon);
    }

#endregion

#region Public Methods

    public override void UseCurrentWeapon() {
        if (currentWeapon != null) {
            ((PlayerAnimations)animationManager).AttackStart();
            currentWeapon.GetComponent<WeaponScript>().Attack();
            if (currentWeapon.tag == Tags.Bomb.ToString()) {
                UIPlayer.Instance.UsedaBomb();
            } 
        }
    }

    public override void AttackFinished() {
        ((PlayerAnimations)animationManager).AttackReturn();
    }

#endregion

#region MonoBehaviour Methods

    void Start() {
        SelectWeapon(Tags.Sword);
    }

#endregion
}
