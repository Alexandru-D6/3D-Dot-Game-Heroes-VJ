using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(PlayerWeaponManager))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(InventorySystem))]
[RequireComponent(typeof(PlayerAnimations))]
public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(Instance);
            Instance = this;
        } else Instance = this;
    }

    #endregion

    #region Parameters

    [Header("References")]
    [SerializeField] private PlayerWeaponManager playerWeaponManager;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private PlayerAnimations playerAnimations;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private PlayerAutomaticMovement playerAutomaticMovement;

    [Header("States")]
    private bool dead = false;

    [Header("Values")]
    [SerializeField] private float deathDelay = 2.0f;
    [SerializeField] private Vector3 dragonThrowback = new Vector3(10.0f, 5.0f, 0.0f);

    #endregion

    #region Enumerators

    private enum OutputInventory { Weapon, Items, Consumables, Uknown };

    #endregion

    #region Public Methods

    public void ReceiveItem(Tags tag) {
        switch(GetInventory(tag)) {
            case OutputInventory.Weapon:
                playerWeaponManager.AddToAvailables(tag);
                UIPlayer.Instance.UnlockedWeapon(tag);
                break;
            case OutputInventory.Items:
                inventorySystem.AddToAvailables(tag);
                break;
            case OutputInventory.Consumables:
                playerHealth.IncreaseHealth(playerHealth.GetHealing(tag));
                break;
        }
        
    }

    public bool IsItemAvailable(Tags tag) {
        if (OutputInventory.Items == GetInventory(tag)) {
            return inventorySystem.IsItemAvailable(tag);
        }

        return false;
    }

    public void GetHit() {
        playerAnimations.toHit();
    }

    IEnumerator DelayedReset(float time) {
        yield return new WaitForSeconds(time);

        SceneEvents.Instance.PlayerDeath();
    }

    public void Die() {
        playerAnimations.toIdle();
        playerAnimations.toDeath();
        playerCollider.enabled = false;
        playerRigidBody.isKinematic = true;
        playerInput.enabled = false;
        dead = true;

        StartCoroutine(DelayedReset(deathDelay));
    }

    public bool isDead() {
        return dead;
    }

    public void PassDoor(Vector3 target) {
        playerInput.RotatePlayer(target);
        playerAutomaticMovement.MoveTo(target);
    }

    public void MoveTo(Vector3 doorPosition, Vector3 doorForward, float distance) {
        playerAutomaticMovement.MoveTo(doorPosition + doorForward * distance);
    }

    public void DragonHit(Tags tag) {
        GameObject dragon = GameObject.FindGameObjectWithTag(Tags.Dragon.ToString());
        Vector3 dragonForward = dragon.transform.forward;

        switch (tag) {
            case Tags.DragonLeftFoot:
                playerRigidBody.velocity += new Vector3(dragonThrowback.x * dragonForward.z,dragonThrowback.y, -1.0f * dragonThrowback.z * dragonForward.x);
                break;
            case Tags.DragonRightFoot:
                playerRigidBody.velocity += new Vector3(-1.0f * dragonThrowback.x * dragonForward.z, dragonThrowback.y, dragonThrowback.z * dragonForward.x);
                break;
        }
    }

    #endregion

    #region Private Methods

    OutputInventory GetInventory(Tags tag) {
        switch(tag) {
            case Tags.Sword:
            case Tags.Bomb:
            case Tags.Boomerang:
            case Tags.Bow:
            case Tags.Shield:
            case Tags.Hand:
                return OutputInventory.Weapon;
            case Tags.BossKey:
            case Tags.EnderKey:
            case Tags.GoldenKey:
            case Tags.SkullKey:
            case Tags.Coin:
                return OutputInventory.Items;
            case Tags.Hamburguer:
                return OutputInventory.Consumables;
        }
        return OutputInventory.Uknown;
    }

    #endregion

    #region MonoBehaviour Methods

    void Start() {
    }

    void Update() {
    }

    #endregion

}
