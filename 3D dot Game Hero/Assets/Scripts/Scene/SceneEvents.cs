using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEvents : MonoBehaviour {

    public static SceneEvents Instance;

    private void Awake() {
        Instance = this;
    }

    public delegate void PlayerDeathAction();
    public static event PlayerDeathAction onPlayerDeath;
    public void PlayerDeath() {
        if (onPlayerDeath != null) {
            onPlayerDeath();
        }
    }

}
