using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestScript : MonoBehaviour {

#region Parameters

    [Header("Managers")]
    [SerializeField] private ChestAnimations chestAnimations;

    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private bool isPlayerNear;
    private bool canOpen;
    [SerializeField] private float openDelay;

#endregion

#region IEnumerators

    IEnumerator delayedOpen(float time) {
        yield return new WaitForSeconds(time);
        canOpen = true;
    }

#endregion

#region Public Methods

    public void playerEntered(bool value) {
        isPlayerNear = value;
    }

#endregion

#region MonoBehaviour Methods

    void Start() {
        canOpen = true;
    }

    void Update() {
        
    }

#endregion

    #region Callbacks Functions

    public void OpenChest(InputAction.CallbackContext context) {
        if (canOpen) {
            canOpen = false;
            Debug.Log("Chest Opened");
            StartCoroutine(delayedOpen(openDelay));
        }
    }

    #endregion

}
