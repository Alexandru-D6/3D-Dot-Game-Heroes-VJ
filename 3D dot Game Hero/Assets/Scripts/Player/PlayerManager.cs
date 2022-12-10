using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponManager))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(InventorySystem))]
[RequireComponent(typeof(PlayerAnimations))]
public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    #endregion

    #region Parameters

    [Header("References")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private PlayerAnimations playerAnimations;

    #endregion

    #region Enumerators

    private enum OutputInventory { Weapon, Items, Consumables, Uknown };

    #endregion

    #region Public Methods

    public void ReceiveItem(Tags tag) {
        switch(GetInventory(tag)) {
            case OutputInventory.Weapon:
                weaponManager.AddToAvailables(tag);
                break;
            case OutputInventory.Items:
                inventorySystem.AddToAvailables(tag);
                break;
            case OutputInventory.Consumables:
                // Comunicate with health system to increase or do what it's need to do
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
