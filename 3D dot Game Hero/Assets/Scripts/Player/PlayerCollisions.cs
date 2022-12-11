using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour {

    #region Parameters

    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;

    #endregion

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Weapon) {
            switch(TagsUtils.GetTag(other.tag)) {
                case Tags.Hand:
                case Tags.Shield:
                case Tags.Sword:
                case Tags.Boomerang:
                    return;
            }
            playerHealth.DecreaseHealth(HealthScript.GetDamage(TagsUtils.GetTag(other.tag)));
        }
    }
}
