using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : HealthScript {

    #region Parameters

    [Header("Managers")]
    [SerializeField] private ZombieMov zombieMov;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider zombieCollider;

    private bool isDead = false;

    #endregion

    #region Collision Methods

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Weapon) {

            // Exclude weapon List
            switch(TagsUtils.GetTag(other.tag)) {
                case Tags.ZombieArm:
                    return;
            }

            DecreaseHealth(GetDamage(TagsUtils.GetTag(other.tag)));
        }
    }

    #endregion

    #region Abstract Methods

    protected override void Die() {
        zombieMov.enabled = false;
        animator.SetBool("Running", false);
        animator.SetBool("Attack", false);
        animator.Play("Idle");
        animator.SetTrigger("Dead");
    }

    protected override void GetHit() {
        animator.SetBool("Running", false);
        animator.SetBool("Attack", false);
        zombieMov.Hitted();
        zombieMov.enabled = false;
        animator.Play("Idle");
        animator.SetTrigger("getHit");
    }

    public void EndHitAnimation()
    {
        zombieMov.enabled = true;
    }

    #endregion

    #region MonoBehaviour Methods

    void Update() {
        if (currentHealth <= 0 && !isDead) {
            isDead = true;
            Die();
        }
    }

    #endregion

}
