using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ChestProbabilities))]
[RequireComponent(typeof(ChestAnimations))]
public class ChestScript : MonoBehaviour {

#region Parameters

    [Header("Reference")]
    [SerializeField] private ChestAnimations chestAnimations;
    [SerializeField] private ChestProbabilities chestProbabilities;

    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private bool isPlayerNear;
    private bool canOpen;
    [SerializeField] private float openDelay;
    [SerializeField] private float closeChestDelay;
    [SerializeField] private float destroyDelay;

#endregion

#region IEnumerators

    IEnumerator DelayedOpenButton(float time) {
        yield return new WaitForSeconds(time);
        canOpen = true;
    }

    IEnumerator DelayedCloseChest(float time) {
        yield return new WaitForSeconds(time);
        chestAnimations.close();
        SoundManager.Instance.PlayChestClose();
        StartCoroutine(DelayedDestroy(destroyDelay));
    }

    IEnumerator DelayedDestroy(float time) {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

#endregion

#region Public Methods

    public void PlayerEntered(bool value) {
        isPlayerNear = value;
    }

    public void RestoreChest(bool value) {
        gameObject.SetActive(value);

        if (value) {
            canOpen = true;
            chestAnimations.toIdle();
        }
    }

#endregion

#region MonoBehaviour Methods

    void Start() {
        SceneEvents.onPlayerDeath += OnPlayerDeath;
    }

    void Update() {
        
    }

    private void OnDestroy() {
        SceneEvents.onPlayerDeath -= OnPlayerDeath;
    }

#endregion

    #region Callbacks Functions

    public void OpenChest(InputAction.CallbackContext context) {
        if (canOpen && isPlayerNear) {
            canOpen = false;
            chestAnimations.open();
            SoundManager.Instance.PlayChestOpen();
            Debug.Log(chestProbabilities.RollAnItem());
            StartCoroutine(DelayedOpenButton(openDelay));
            StartCoroutine(DelayedCloseChest(closeChestDelay));
        }
    }

    public void OnPlayerDeath() {
        RestoreChest(false);
    }

    #endregion

}
