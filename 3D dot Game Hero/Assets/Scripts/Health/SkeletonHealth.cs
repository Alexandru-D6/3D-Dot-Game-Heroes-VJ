using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SkeletonHealth : HealthScript {

    #region Parameters

    [Header("Managers")]
    [SerializeField] private SkeletonMov skeletonMov;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider skeletonCollider;
    [SerializeField] private GameObject disappearParticles;

    private bool isDead = false;

    #endregion

    #region Collision Methods

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Weapon && !isDead) {
            // Exclude weapon List
            switch(TagsUtils.GetTag(other.tag)) {
                case Tags.ZombieArm:
                case Tags.Bow:
                    return;
            }

            if (other.tag == Tags.Arrow.ToString() && other.GetComponent<ArrowScript>().GetOriginalLayer() == Layers.Enemies) return;

            DecreaseHealth(GetDamage(TagsUtils.GetTag(other.tag)));
        }
    }

    #endregion

    #region Abstract Methods

    protected override void Die() {
        skeletonMov.enabled = false;
        animator.SetBool("Running", false);
        animator.SetBool("Aim", false);
        animator.Play("Idle");
        animator.SetTrigger("Dead");
    }

    protected override void GetHit() {
        animator.SetBool("Running", false);
        animator.SetBool("Aim", false);
        skeletonMov.Hitted();
        skeletonMov.enabled = false;
        animator.Play("Idle");
        animator.SetTrigger("Hit");
    }

    public void EndHitAnimation()
    {
        skeletonMov.enabled = true;

    }

   public void readyToDestroy()
    {
        StartCoroutine (destroyObject());
        Instantiate(disappearParticles, transform.position, transform.rotation);
    }

   IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);

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
