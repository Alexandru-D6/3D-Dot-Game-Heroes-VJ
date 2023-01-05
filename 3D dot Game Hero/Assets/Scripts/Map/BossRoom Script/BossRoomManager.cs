using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomManager : MonoBehaviour {
    
    [SerializeField] private Transform bossGameObject;
    [SerializeField] private Vector3 initBossPosition = new Vector3(32.2f,45.0f,82.4f);
    [SerializeField] private Vector3 targetPosition = new Vector3(32.2f,0.0f,82.4f);
    [SerializeField] private Vector3 animationSpeed = new Vector3(30.0f,30.0f,30.0f);
    [SerializeField] private DragonAnimations dragonAnimator;
    [SerializeField] private DragonAnimationEventsHandler dragonAnimationEventsHandler;

    private bool enableSpawn = false;
    private bool startMovement = false;

    IEnumerator delayedStartBattleRoutine(float time) {
        yield return new WaitForSeconds(time);
        
        enableSpawn = false;
        PlayerManager.Instance.LockPlayer(true);
        dragonAnimationEventsHandler.DisableFlamethrower(false);
        bossGameObject.GetComponent<DragonLogic>().enabled = true;

        dragonAnimator.toIdle();
    }

    IEnumerator delayedRoarRoutine(float time) {
        yield return new WaitForSeconds(time);
        
        SoundManager.Instance.PlayDragonRoar();
        dragonAnimator.Breath_Gs();
    }

    IEnumerator delayedDisableInputRoutine(float time) {
        yield return new WaitForSeconds(time);

        PlayerManager.Instance.LockPlayer(false);
    }

    public void PlayerEntered() {
        StartCoroutine(delayedDisableInputRoutine(1.0f));
        enableSpawn = true;
        startMovement = true;
        dragonAnimationEventsHandler.DisableFlamethrower(true);
        bossGameObject.GetComponent<DragonLogic>().enabled = false;

        dragonAnimator.toIdle();
        dragonAnimator.enableFlying(true);

        SoundManager.Instance.StopMinecraftMainTheme();
        SoundManager.Instance.PlayZeldaMainTheme();
    }

    public void SpawnAnimation() {
        if (startMovement) {
            Vector3 direction = targetPosition - bossGameObject.localPosition;
            direction.Normalize();

            if (Vector3.Distance(bossGameObject.localPosition, targetPosition) < animationSpeed.x * Time.deltaTime) {
                bossGameObject.localPosition = targetPosition;
                startMovement = false;
                BossRoar();
                return;
            }

            bossGameObject.Translate(new Vector3(direction.x * animationSpeed.x, direction.y * animationSpeed.y, direction.z * animationSpeed.y) * Time.deltaTime, Space.World);
        }
    }

    public void BossRoar() {
        dragonAnimator.enableFlying(false);

        StartCoroutine(delayedRoarRoutine(1.0f));
        StartCoroutine(delayedStartBattleRoutine(4.0f));
    }

    private void Start() {
        bossGameObject.localPosition = initBossPosition;
        bossGameObject.localEulerAngles = new Vector3(0.0f,180.0f,0.0f);
        dragonAnimator = bossGameObject.GetComponent<DragonAnimations>();
        dragonAnimationEventsHandler = bossGameObject.GetComponentInChildren<DragonAnimationEventsHandler>();
    }

    private void Update() {
        if (enableSpawn) SpawnAnimation();
    }
}
