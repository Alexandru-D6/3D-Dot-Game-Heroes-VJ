using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

#region Parameters

    [Header("Managers")]
    [SerializeField] private PlayerAnimations playerAnimations;
    [SerializeField] private PlayerInput playerInput;

    [Space(10)]

    [Header("Inventory Management")]
    [SerializeField] private List<GameObject> weaponsPrefabs;
    [SerializeField] private List<Tags> weaponsAvailable;
    [SerializeField] private List<WeaponScript> inventory;

    [Space(10)]

    [Header("References")]
    [SerializeField] private GameObject playerHand;
    [SerializeField] private WeaponScript currentWeapon = null;

#endregion

#region Inventory Management

    bool IsWeaponAvailable(Tags weapon) {
        // Search if it's spawned
        foreach(WeaponScript x in inventory) {
            if (x.GetName().Equals(weapon.ToString())) return true;
        }

        // Search if it's available to spawn
        foreach(Tags x in weaponsAvailable) {
            if (x.Equals(weapon)) return true;
        }

        return false;
    }

    bool RevealSpawnedWeapon(Tags weapon) {
        if (inventory.Count == 0) return false;

        // Returning true, the selection of another weapon while boomerang is flying while be invalid
        if (currentWeapon.transform.parent != playerHand.transform) return true;

        // Reset animation and button cooldown
        playerAnimations.toIdle();
        playerInput.PI_resetFire();

        if (currentWeapon != null && currentWeapon.GetName().Equals(weapon.ToString())) {
            SelectWeapon(Tags.Hand);
            return true;
        }

        foreach (var x in inventory) {
            if (x.GetName().Equals(weapon.ToString())) {
                UnselectCurrentWeapon();

                currentWeapon = x;
                currentWeapon.SetActive(true);
                currentWeapon.RestartState();
                return true;
            }
        }
        return false;
    }

    GameObject GetObjectPrefab(Tags weapon) {
        foreach(GameObject x in weaponsPrefabs) {
            if(x.tag.Equals(weapon.ToString())) return x; 
        }
        return null;
    }

    void InstantiateWeapon(Tags weapon) {
        GameObject prefab = GetObjectPrefab(weapon);
        GameObject obj = Instantiate(prefab, new UnityEngine.Vector3(), new UnityEngine.Quaternion());

        obj.SetActive(false);
        obj.transform.parent = playerHand.transform;

        UnselectCurrentWeapon();
        currentWeapon = obj.GetComponent<WeaponScript>();
        currentWeapon.SetWeaponManager(this);

        inventory.Add(currentWeapon);
        obj.SetActive(true);
    }

    void UnselectCurrentWeapon() {
        if (currentWeapon != null) {
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }

#endregion

#region Public Methods

    public void DestroyWeapon(Tags weapon) {
        for (int i = 0; i < inventory.Count; ++i) {
            if (inventory[i].GetName().Equals(weapon.ToString())) {
                inventory[i].SetActive(false);
                inventory[i].transform.parent = null;

                Destroy(inventory[i].gameObject);
                inventory.RemoveAt(i);
                return;
            }
        }
    }

    public void AddToAvailables(Tags weapon) {
        // Search if it's already exists
        foreach(Tags x in weaponsAvailable) {
            if (x.Equals(weapon)) return;
        }

        weaponsAvailable.Add(weapon);
    }

    public void SelectWeapon(Tags weapon) {
        if (IsWeaponAvailable(weapon)) {
            if (RevealSpawnedWeapon(weapon)) return;

            // If it haven't spawned yet, do it.
            InstantiateWeapon(weapon);
        } else {
            // TODO: It may return an error or do something?
        }
    }

    public void UseCurrentWeapon() {
        if (currentWeapon != null) {
            playerAnimations.AttackStart();
            currentWeapon.GetComponent<WeaponScript>().Attack();
        }
    }

    public void AttackFinished() {
        playerAnimations.AttackReturn();
    }

#endregion

#region MonoBehaviour Methods

    void Start() {
        SelectWeapon(Tags.Hand);
    }

#endregion

}
