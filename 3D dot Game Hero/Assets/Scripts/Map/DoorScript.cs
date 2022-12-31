using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour {

    private enum DoorType { EnderDoor, CaveDoor, BossDoor };

#region Parameters

    [Header("References")]
    [SerializeField] private Animator doorAnimator;

    [Header("Objects")]
    [SerializeField] private PadlockScript padlockScript;

    [Header("Configuration")]

    [Header("States")]
    [SerializeField] private bool canOpen = false;
    [SerializeField] private bool playerNearby = false;
    [SerializeField] private DoorType doorType;

#endregion Parameters

#region Collisions Methods

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Tags.Player.ToString()) playerNearby = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == Tags.Player.ToString()) playerNearby = false;
    }

#endregion

#region Private Methods

    bool CheckKey() {
        Tags keyToAsk = Tags.Uknown;

        switch(doorType) {
            case DoorType.BossDoor:
                keyToAsk = Tags.BossKey;
                break;
            case DoorType.CaveDoor:
                keyToAsk = Tags.SkullKey;
                break;
            case DoorType.EnderDoor:
                keyToAsk = Tags.EnderKey;
                break;
        }

        return PlayerManager.Instance.IsItemAvailable(keyToAsk);
    }

#endregion

#region Public Methods

    public void OpenDoor() {
        doorAnimator.SetTrigger("Open");
    }

#endregion

#region Callbacks Functions

    public void UnlockDoor(InputAction.CallbackContext context) {
        canOpen = CheckKey();

        if (canOpen && playerNearby) {
            PlayerManager.Instance.MoveTo(transform.position, transform.forward, 3.0f);
            canOpen = false;
            padlockScript.UnlockPadlock();
        }
    }

#endregion

}
