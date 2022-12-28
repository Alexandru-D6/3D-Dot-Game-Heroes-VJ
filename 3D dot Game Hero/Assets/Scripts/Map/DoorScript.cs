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
    [SerializeField] private bool canOpen = true;
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



#endregion

#region Public Methods

    public void OpenDoor() {
        doorAnimator.SetTrigger("Open");
    }

#endregion

#region MonoBehaviour Methods

    private void Update() {
    }

#endregion MonoBehaviour Methods

#region Callbacks Functions

    public void UnlockDoor(InputAction.CallbackContext context) {
        if (canOpen && playerNearby) {
            PlayerManager.Instance.MoveTo(transform.position, transform.forward, 3.0f);
            canOpen = false;
            padlockScript.UnlockPadlock();
        }
    }

#endregion

}
