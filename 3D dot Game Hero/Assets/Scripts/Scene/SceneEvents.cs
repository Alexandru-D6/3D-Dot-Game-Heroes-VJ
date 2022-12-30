using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEvents : MonoBehaviour {

    public static SceneEvents Instance;

    private void Awake() {
        Instance = this;
    }

    public event Action onPlayerDeath;
    public void PlayerDeath() {
        if (onPlayerDeath != null) {
            onPlayerDeath();
        }
    }

}
