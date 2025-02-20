using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonManager : MonoBehaviour {

    #region Parameters

    [Header("References")]
    [SerializeField] private DragonAnimations animator;
    [SerializeField] private DragonLogic dragonLogic;
    [SerializeField] private DragonHealth dragonHealth;

    [Header("States")]
    private bool dead = false;

    [Header("Values")]
    [SerializeField] private float deathDelay = 10.0f;

    #endregion

    IEnumerator delayedToCreditsRoutine(float time) {
        SoundManager.Instance.PlayMinecraftLevelUp();

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("Credits");
    }

    #region Public Methods

    public void GetHit() {
        animator.toHit();
    }

    public void Die() {
        animator.toDeath();
        dragonLogic.enabled = false;
        dead = true;

        StartCoroutine(delayedToCreditsRoutine(deathDelay));
    }

    public bool isDead() {
        return dead;
    }

    public DragonAnimations GetAnimator() {
        return animator;
    }

    #endregion

    private void RestoreDragon() {
        dragonHealth.RestoreHealth();
    }

    private void Start() {
        RestoreDragon();
        SceneEvents.onPlayerDeath += OnPlayerDeath;
    }

    private void Update() {
    }

    private void OnDestroy() {
        SceneEvents.onPlayerDeath -= OnPlayerDeath;
    }

    public void OnPlayerDeath() {
        RestoreDragon();
    }

}
