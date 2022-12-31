using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthScript {

    #region Parameters

    [Header("Managers")]
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ParticleSystem blood;

    #endregion

    #region Collision Methods

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Weapon) {

            Collider[] colliders = Physics.OverlapSphere(other.transform.position, Vector3.Distance(other.bounds.max, other.bounds.min) * 0.75f);

            foreach (var collider in colliders) {
                if (collider != other && collider.gameObject.layer == (int)Layers.Weapon) return;
            }

            // Exclude weapon List
            switch(TagsUtils.GetTag(other.tag)) {
                case Tags.Hand:
                case Tags.Shield:
                case Tags.Sword:
                case Tags.Boomerang:
                    return;
            }

            if (other.tag == Tags.Arrow.ToString() && other.GetComponent<ArrowScript>().GetOriginalLayer() == Layers.Player) return;

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
        Instantiate(blood, new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), transform.rotation);
    }

    #endregion

    #region MonoBehaviour Methods

    void Update() {
        if (currentHealth <= 0 && !playerManager.isDead()) {
            Die();
        }
    }

    #endregion

}
