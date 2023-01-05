using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsManager : MonoBehaviour {

    #region Singleton

    public static SceneObjectsManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(Instance);
            Instance = this;
        } else Instance = this;
    }

    #endregion

    private void DestroyEverything() {
        for (int i = transform.childCount - 1; i >= 0; --i) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void DestroyObjects() {
        for (int i = transform.childCount - 1; i >= 0; --i) {
            if (gameObject.layer != (int)Layers.Weapon) Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void Start() {
        DestroyEverything();
        SceneEvents.onPlayerDeath += OnPlayerDeath;
    }

    private void OnDestroy() {
        SceneEvents.onPlayerDeath -= OnPlayerDeath;
    }

    public void OnPlayerDeath() {
        DestroyEverything();
    }
}
