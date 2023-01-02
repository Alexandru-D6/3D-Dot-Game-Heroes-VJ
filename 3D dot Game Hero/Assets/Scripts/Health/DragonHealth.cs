using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHealth : HealthScript {
    #region Parameters

    [Header("Managers")]
    [SerializeField] private DragonManager dragonManager;

    #endregion

    #region Collision Methods

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Weapon) {

            Tags tag = TagsUtils.GetTag(other.tag);

            DecreaseHealth(GetDamage(tag));
        }
    }

    #endregion

    #region Abstract Methods

    protected override void Die() {
        dragonManager.Die();
    }

    protected override void GetHit() {
        dragonManager.GetHit();
    }

    #endregion

    #region MonoBehaviour Methods

    void Update() {
        if (currentHealth <= 0 && !dragonManager.isDead()) {
            Die();
        }
    }

    #endregion
}
