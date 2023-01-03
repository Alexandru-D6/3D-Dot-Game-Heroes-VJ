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

    private void OnParticleCollision(GameObject other) {
        if (other.gameObject.layer == (int)Layers.Weapon) {
            DecreaseHealth(GetDamage(TagsUtils.GetTag(other.tag)));
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Weapon) {

            Tags tag = TagsUtils.GetTag(other.tag);

            Collider[] colliders = Physics.OverlapSphere(other.transform.position, Vector3.Distance(other.bounds.max, other.bounds.min) * 0.55f);

            foreach (var collider in colliders) {
                if (colliders.Length <= 1 || collider != other && (collider.gameObject.layer == (int)Layers.Weapon || collider.gameObject.layer == (int)Layers.Shield)) return;
            }

            // Exclude weapon List
            switch(tag) {
                case Tags.Hand:
                case Tags.Shield:
                case Tags.Sword:
                case Tags.Boomerang:
                case Tags.Bomb:
                    return;
            }

            if (tag == Tags.Arrow && other.GetComponent<ArrowScript>().GetOriginalLayer() == Layers.Player) return;

            if (tag == Tags.DragonLeftFoot || tag == Tags.DragonRightFoot) playerManager.DragonHit(tag);

            DecreaseHealth(GetDamage(tag));
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
