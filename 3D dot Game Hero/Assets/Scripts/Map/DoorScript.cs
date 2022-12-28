using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    [SerializeField] private bool doorOpened = false;
    [SerializeField] private Animator animator;

    private void Update() {
        animator.SetBool("Open", doorOpened);
    }

}
