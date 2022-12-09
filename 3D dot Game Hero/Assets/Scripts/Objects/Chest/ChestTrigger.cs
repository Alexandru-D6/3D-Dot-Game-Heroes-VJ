using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour {

    #region Parameters

    [SerializeField] private ChestScript chestScript;

    #endregion

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            chestScript.PlayerEntered(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            chestScript.PlayerEntered(false);
        }
    }

}
