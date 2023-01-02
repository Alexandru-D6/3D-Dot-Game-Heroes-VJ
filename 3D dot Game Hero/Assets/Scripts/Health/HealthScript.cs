using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthScript : MonoBehaviour {

    [System.Serializable]
    public struct DamageStat {
        public Tags weapon;
        public int value;

        public DamageStat(Tags weapon, int value) {
            this.weapon = weapon;
            this.value = value;
        }
    };

    #region Parameters

    [Header("Information")]
    [Range(0,300)]
    [SerializeField] protected int currentHealth;
    [Range(0,300)]
    [SerializeField] protected int maxHealth;
    [Space(10)]
    [SerializeField] List<DamageStat> damageStats = new List<DamageStat>{ 
        new DamageStat(Tags.Arrow, 10),
        new DamageStat(Tags.Bomb, 10),
        new DamageStat(Tags.Boomerang, 10),
        new DamageStat(Tags.Hand, 10),
        new DamageStat(Tags.Shield, 10),
        new DamageStat(Tags.Sword, 10),
        new DamageStat(Tags.ZombieArm, 10),
        new DamageStat(Tags.EndermanArm, 10),
        new DamageStat(Tags.ExplosionCreeper, 10),
        new DamageStat(Tags.ExplotionBomb, 100),
        new DamageStat(Tags.DragonLeftFoot, 10),
        new DamageStat(Tags.DragonRightFoot, 10),
        new DamageStat(Tags.FlamethrowerParticles, 30),
    };

    [Header("Others")]
    [SerializeField] private bool isInmortal = false;

    #endregion

    #region IEnumerators

    IEnumerator delayedInmortalityDisableRoutine(float time) {
        yield return new WaitForSeconds(time);

        isInmortal = false;
    }

    #endregion

    #region Abstract Methods

    protected abstract void GetHit();

    protected abstract void Die();

    #endregion

    #region Public Methods

    public int GetHealth() {
        return currentHealth;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public void IncreaseHealth(int value) {
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public int GetDamage(Tags tag) {
        int damage = 0;

        foreach (DamageStat damageStat in damageStats) {
            if (damageStat.weapon == tag) damage = damageStat.value;
        }

        return damage;
    }

    public void DecreaseHealth(int value) {
        if (value > 0 && !isInmortal) {
            currentHealth -= value;
            if (currentHealth > 0) GetHit();

            GiveInmortality(1.0f);
        }
    }

    public void GiveInmortality(float time, bool infinite = false) {
        isInmortal = true;

        if (!infinite) StartCoroutine(delayedInmortalityDisableRoutine(time));
    }

    #endregion

    #region MonoBehaviour

    private void Start() {
        currentHealth = maxHealth;
    }

    #endregion

}
