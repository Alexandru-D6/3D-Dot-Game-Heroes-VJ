using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadlockScript : MonoBehaviour {
    
    [SerializeField] private Animator padlockAnimator;
    [SerializeField] private DoorScript doorScript;
    private bool canUnlock = true;

    [SerializeField] private GameObject padlock;
    [SerializeField] private GameObject key;
    [SerializeField] private Camera padlockCamera;
    [SerializeField] private Camera mainCamera;

    // TODO: Instead of jsut enable the camera make somekind of transition
    public void UnlockPadlock() {
        if (canUnlock) {
            canUnlock = false;

            mainCamera.enabled = false;
            padlockCamera.enabled = true;

            padlockAnimator.SetTrigger("Unlock");
        }
    }

    public void HidePadlock() {
        mainCamera.enabled = true;
        padlockCamera.enabled = false;

        padlock.SetActive(false);
        key.SetActive(false);

        doorScript.OpenDoor();
    }

    public void RestorePadlock() {
        padlock.SetActive(true);
        key.SetActive(false);

        padlockAnimator.Play("Idle");
        canUnlock = true;
    }

    private void Start() {
        mainCamera = Camera.main;

        RestorePadlock();
    }
}
