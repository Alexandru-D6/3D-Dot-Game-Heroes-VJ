using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    [SerializeField] private Animator gridAnimator;
    public void OpenGrid() {
        gridAnimator.SetBool("Open", true);
    }

    public void CloseGrid() {
        gridAnimator.SetBool("Open", false);
    }
}
