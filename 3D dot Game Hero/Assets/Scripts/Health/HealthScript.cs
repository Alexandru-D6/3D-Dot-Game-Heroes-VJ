using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthScript : MonoBehaviour {

    #region Parameters

    [Header("Information")]
    [Range(0,300)]
    [SerializeField] protected int currentHealth;
    [Range(0,300)]
    [SerializeField] protected int maxHealth;

    #endregion

    #region Static Methods

    public static int GetDamage(Tags tag) {
        int damage = 0;
        switch(tag) {
            case Tags.Arrow:
                damage = 10;
                break;
            case Tags.Bomb:
                damage = 10;
                break;
            case Tags.Boomerang:
                damage = 10;
                break;
            case Tags.Hand:
                damage = 10;
                break;
            case Tags.Shield:
                damage = 10;
                break;
            case Tags.Sword:
                damage = 50;
                break;
        }

        return damage;
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
    }

    public void DecreaseHealth(int value) {
        currentHealth -= value;
        GetHit();
    }

    #endregion

    #region MonoBehaviour

    private void Start() {
        currentHealth = maxHealth;
    }

    #endregion

}
