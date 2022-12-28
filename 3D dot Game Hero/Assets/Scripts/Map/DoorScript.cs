using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

#region Parameters

    [Header("References")]
    [SerializeField] private Animator animator;

    [Header("Objects")]
    [SerializeField] private GameObject padlock;

    [Header("Configuration")]
    // Maybe remove it?
    [SerializeField] private Vector3 padlockPosition;
    [SerializeField] private Vector3 padlockRotation;

    [Header("States")]
    [SerializeField] private bool doorOpened = false;
    [SerializeField] private bool playerNearby = false;

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

#region Private Methods



#endregion

#region MonoBehaviour Methods

    private void Update() {
        animator.SetBool("Open", doorOpened);
        padlock.SetActive(!doorOpened);
    }

#endregion MonoBehaviour Methods

}
