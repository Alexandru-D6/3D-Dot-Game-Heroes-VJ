using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ZombieHealth : HealthScript {

    #region Parameters

    [Header("Managers")]
    [SerializeField] private ZombieMov zombieMov;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider zombieCollider;
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
                    return;
            }

            DecreaseHealth(GetDamage(TagsUtils.GetTag(other.tag)));
            Quaternion aux = Quaternion.AngleAxis(-90, Vector3.right);
            Instantiate(impactParticles, transform.position, aux, transform);
        }
    }

    #endregion

    #region Abstract Methods

    protected override void Die() {
        zombieMov.Hitted();
        zombieMov.enabled = false;
        animator.SetBool("Running", false);
        animator.SetBool("Attack", false);
        animator.Play("Idle");
        animator.SetTrigger("Dead");
        SoundManager.Instance.PlayZombieDeath();
    }

    protected override void GetHit() {
        animator.SetBool("Running", false);
        animator.SetBool("Attack", false);
        zombieMov.Hitted();
        zombieMov.enabled = false;
        animator.Play("Idle");
        animator.SetTrigger("getHit");
        SoundManager.Instance.PlayZombieHit();
    }

    public void EndHitAnimation()
    {
        if(currentHealth >0)zombieMov.enabled = true;
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
