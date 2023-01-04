using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CreeperHealth : HealthScript {

    #region Parameters

    [Header("Managers")]
    [SerializeField] private CreeperMov creeperMov;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider creeperCollider;
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
                case Tags.EndermanArm:
                case Tags.ZombieArm:
                case Tags.ExplosionCreeper:
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
        creeperMov.Hitted();
        creeperMov.enabled = false;
        animator.SetBool("Running", false);
        animator.SetBool("Explosion", false);
        animator.Play("Idle");
        animator.SetTrigger("Dead");
    }

    protected override void GetHit() {
       // creeperMov.Hitted();
       // creeperMov.enabled = false;
       // animator.Play("Idle");
       // animator.SetTrigger("Hit");
    }

    public void EndHitAnimation()
    {
       // if(currentHealth>0)creeperMov.enabled = true;
    }

   public void readyToDestroy()
    {
        StartCoroutine (destroyObject());
        Quaternion aux = transform.rotation;
        aux = Quaternion.AngleAxis(transform.eulerAngles.y + 90, Vector3.up);
        Instantiate(disappearParticles, transform.position, aux, transform);
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
