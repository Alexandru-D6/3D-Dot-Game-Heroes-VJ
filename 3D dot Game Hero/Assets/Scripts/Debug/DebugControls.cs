using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// For general purpose while developing a feature. It could
// change depending on the necessity.
public class DebugControls : MonoBehaviour {

    public WeaponManager weaponManager;
    public WeaponScript weaponScript;
    public GameObject sword;

    [SerializeField] private bool buttonAvailability;
    [SerializeField] private float buttonDelay;
    List<Tags> availablesKeys = new List<Tags> { Tags.EnderKey, Tags.SkullKey };

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

            EnemiesRoomManager[] components = GameObject.FindObjectsOfType<EnemiesRoomManager>();

            foreach (EnemiesRoomManager x in components) {
                if (x.gameObject.GetComponent<RoomManager>().IsPlayerInsideTheRoom()) x.DestroyAllEnemies();
            }
        }
    }
    public void Key_2_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));

            RoomManager[] components = GameObject.FindObjectsOfType<RoomManager>();

            foreach (RoomManager x in components) {
                if (x.IsPlayerInsideTheRoom()) x.roomSolved();
            }
        }
    }
    public void Key_3_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));

            int random = Random.Range(0,availablesKeys.Count);
            PlayerManager.Instance.ReceiveItem(availablesKeys[random]);
            availablesKeys.RemoveAt(random);
        }
    }
    public void Key_4_Pressed(InputAction.CallbackContext context) {
        if (buttonAvailability) {
            StartCoroutine(delayedButon(buttonDelay));

            PlayerManager.Instance.ReceiveItem(Tags.BossKey);
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
            var tmp = GameObject.FindGameObjectsWithTag(Tags.Sword.ToString());

            foreach (var c in tmp) if (c.name.Contains("Sword(Clone)")) sword = c;
            weaponScript = sword.GetComponent<WeaponScript>();
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
            weaponScript.SetLevelOfPower(weaponScript.GetLevelOfPower() + 1);
        }
    }

}
