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
    [Space(10)]
    [SerializeField] List<DamageStat> healingStats = new List<DamageStat>{ 
        new DamageStat(Tags.Hamburguer, 10),
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

    public int GetHealing(Tags tag) {
        int heal = 0;

        foreach (DamageStat healingStat in healingStats) {
            if (healingStat.weapon == tag) heal = healingStat.value;
        }

        return heal;
    }

    public bool IncreaseHealth(int value) {
        if (currentHealth >= maxHealth) return false;

        currentHealth += value;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        SoundManager.Instance.PlayPlayerEating();
        return true;
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

    public bool IsInmortal() {
        return isInmortal;
    }

    public void RestoreHealth() {
        currentHealth = maxHealth;
    }

    #endregion

    #region MonoBehaviour

    private void Start() {
        currentHealth = maxHealth;
    }

    #endregion

}
