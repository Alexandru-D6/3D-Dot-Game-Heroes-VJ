using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// For general purpose while developing a feature. It could
// change depending on the necessity.
public class DebugControls : MonoBehaviour {

    public WeaponManager weaponManager;
    public WeaponScript weaponScript;

    [SerializeField] private string weaponToSpawn;

    [SerializeField] private bool buttonAvailability;
    [SerializeField] private float buttonDelay;

    #region IEnumerators

    IEnumerator delayedButon(float time) {
        buttonAvailability = false;
        yield return new WaitForSeconds(time);
        buttonAvailability = true;
    }

    #endregion

    public void Key_1_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            weaponManager.DB_selectWeapon(weaponToSpawn);
        }
    }
    public void Key_2_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            weaponManager.DB_unselectCurrentWeapon();
        }
    }
    public void Key_3_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            weaponManager.DB_deleteWeapon(weaponToSpawn);
        }
    }
    public void Key_4_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            weaponManager.DB_createWeapon(weaponToSpawn, int.MaxValue);
        }
    }
    public void Key_5_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
        }
    }
    public void Key_6_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
        }
    }
    public void Key_7_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            weaponScript = GameObject.FindGameObjectWithTag(weaponToSpawn).GetComponent<WeaponScript>();
        }
    }
    public void Key_8_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            weaponScript.SetLevelOfPower(weaponScript.GetLevelOfPower() - 1);
        }
    }
    public void Key_9_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            Debug.Log(weaponScript.GetLevelOfPower() + 1);
            weaponScript.SetLevelOfPower(weaponScript.GetLevelOfPower() + 1);
        }
    }

}
