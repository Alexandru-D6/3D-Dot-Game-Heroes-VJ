using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthScript {

    #region Parameters

    [Header("Managers")]
    [SerializeField] private PlayerManager playerManager;

    #endregion

    #region Abstract Methods

    protected override void Die() {
        playerManager.Die();
    }

    protected override void GetHit() {
        playerManager.GetHit();
    }

    #endregion

    #region MonoBehaviour Methods

    void Update() {
        if (currentHealth <= 0) {
            Die();
        }
    }

    #endregion

}
