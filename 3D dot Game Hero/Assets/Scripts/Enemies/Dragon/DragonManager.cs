using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonManager : MonoBehaviour {

    #region Parameters

    [Header("References")]
    [SerializeField] private DragonAnimations animator;
    [SerializeField] private DragonLogic dragonLogic;

    [Header("States")]
    private bool dead = false;

    [Header("Values")]
    [SerializeField] private float deathDelay = 2.0f;

    #endregion

    #region Public Methods

    public void GetHit() {
        animator.toHit();
    }

    public void Die() {
        animator.toDeath();
        dragonLogic.enabled = false;
        dead = true;
    }

    public bool isDead() {
        return dead;
    }

    public DragonAnimations GetAnimator() {
        return animator;
    }

    #endregion

    #region MonoBehaviour Methods

    void Start() {
    }

    void Update() {
    }

    #endregion

}
