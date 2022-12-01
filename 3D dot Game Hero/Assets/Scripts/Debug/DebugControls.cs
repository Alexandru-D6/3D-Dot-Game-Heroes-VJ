using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugControls : MonoBehaviour {

    public WeaponManager weaponManager;
    public SwordScript SwordScript;
    public void Key_1_Pressed(InputAction.CallbackContext context) {
        weaponManager.DB_selectWeapon("Sword");
    }
    public void Key_2_Pressed(InputAction.CallbackContext context) {
        weaponManager.DB_unselectCurrentWeapon();
    }
    public void Key_3_Pressed(InputAction.CallbackContext context) {
        weaponManager.DB_deleteWeapon("Sword");
    }
    public void Key_4_Pressed(InputAction.CallbackContext context) {
        weaponManager.DB_createWeapon("Sword", int.MaxValue);
    }
    public void Key_5_Pressed(InputAction.CallbackContext context) {

    }
    public void Key_6_Pressed(InputAction.CallbackContext context) {

    }
    public void Key_7_Pressed(InputAction.CallbackContext context) {

    }
    public void Key_8_Pressed(InputAction.CallbackContext context) {

    }
    public void Key_9_Pressed(InputAction.CallbackContext context) {

    }
}
