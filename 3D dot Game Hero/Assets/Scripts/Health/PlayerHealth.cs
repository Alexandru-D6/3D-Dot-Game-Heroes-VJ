using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthScript {

    #region Parameters

    [Header("Managers")]
    [SerializeField] private PlayerManager playerManager;

    #endregion

    #region Collision Methods

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Weapon) {

            // Exclude weapon List
            switch(TagsUtils.GetTag(other.tag)) {
                case Tags.Hand:
                case Tags.Shield:
                case Tags.Sword:
                case Tags.Boomerang:
                    return;
            }

            DecreaseHealth(GetDamage(TagsUtils.GetTag(other.tag)));
        }
    }

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
