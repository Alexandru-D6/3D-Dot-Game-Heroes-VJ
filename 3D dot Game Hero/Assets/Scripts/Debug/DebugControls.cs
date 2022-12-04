using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugControls : MonoBehaviour {

    public WeaponManager weaponManager;
    public SwordScript SwordScript;

    [SerializeField] private bool buttonAvailability;

    [SerializeField] private float buttonDelay;
    IEnumerator delayedButon(float time) {
        buttonAvailability = false;
        yield return new WaitForSeconds(time);
        buttonAvailability = true;
    }

    public void Key_1_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            weaponManager.DB_selectWeapon("Sword");
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
            weaponManager.DB_deleteWeapon("Sword");
        }
    }
    public void Key_4_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            weaponManager.DB_createWeapon("Sword", int.MaxValue);
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
            SwordScript = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordScript>();
        }
    }
    public void Key_8_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            SwordScript.setLevelOfPower(SwordScript.levelOfPower - 1);
        }
    }
    public void Key_9_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));
            Debug.Log(SwordScript.levelOfPower);
            SwordScript.setLevelOfPower(SwordScript.levelOfPower + 1);
        }
    }
}
