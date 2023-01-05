using Assets.Scripts.Enemies;
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
    [SerializeField] private GameObject impactParticles;
    [SerializeField] private Manager manager;

    private bool isDead = false;

    #endregion

    #region Collision Methods

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Weapon && !isDead) {
            // Exclude weapon List
            switch(TagsUtils.GetTag(other.tag)) {
                case Tags.ZombieArm:
                case Tags.Bow:
                case Tags.Bomb:
                    return;
            }

            if (other.tag == Tags.Arrow.ToString() && other.GetComponent<ArrowScript>().GetOriginalLayer() == Layers.Enemies) return;

            DecreaseHealth(GetDamage(TagsUtils.GetTag(other.tag)));
            Quaternion aux = Quaternion.AngleAxis(-90, Vector3.right);
            Instantiate(impactParticles, transform.position, aux, transform);
        }
    }

    #endregion

    #region Abstract Methods

    protected override void Die() {
        skeletonMov.Hitted();
        skeletonMov.enabled = false;
        animator.SetBool("Running", false);
        animator.SetBool("Shoot",false);
        animator.Play("Idle");
        animator.SetTrigger("Dead");
        SoundManager.Instance.PlaySkeletonDeath();
    }

    protected override void GetHit() {
        animator.SetBool("Running", false);
        animator.SetBool("Shoot",false);
        skeletonMov.Hitted();
        skeletonMov.enabled = false;
        animator.Play("Idle");
        animator.SetTrigger("Hit");
        SoundManager.Instance.PlaySkeletonHit();
    }

    public void EndHitAnimation()
    {
        if (currentHealth > 0) {
            skeletonMov.enabled = true;
            skeletonMov.isEnabled();
        }

    }

   public void readyToDestroy()
    {
        StartCoroutine (destroyObject());
        Instantiate(disappearParticles, transform.position, transform.rotation, transform);
    }

   IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<LootBag>().InstantiateLoot(transform.position);
        manager.isDead();
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
