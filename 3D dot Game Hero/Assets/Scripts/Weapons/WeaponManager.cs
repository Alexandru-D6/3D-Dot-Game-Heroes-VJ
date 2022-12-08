using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// First version of the inventory system, it might need a huge refactor
// once there's more weapons.
public class WeaponManager : MonoBehaviour {

#region Parameters

    [Header("Managers")]
    [SerializeField] private AnimationManager animationManager;

    [Space(10)]

    [Header("Inventory Management")]
    [SerializeField] private List<GameObject> weaponsPrefabs;
    [SerializeField] private List<string> weaponsAvailable;
    [SerializeField] private List<WeaponScript> inventory;

    [Space(10)]

    [Header("References")]
    [SerializeField] private GameObject playerHand;
    [SerializeField] private WeaponScript currentWeapon = null;

#endregion

#region Inventory Management

    void AddToAvailables(string weapon) {
        // Search if it's already exists
        foreach(string x in weaponsAvailable) {
            if (x.Equals(weapon)) return;
        }

        weaponsAvailable.Add(weapon);
    }

    bool IsWeaponAvailable(string weapon) {
        // Search if it's spawned
        foreach(WeaponScript x in inventory) {
            if (x.GetName().Equals(weapon)) return true;
        }

        // Search if it's available to spawn
        foreach(string x in weaponsAvailable) {
            if (x.Equals(weapon)) return true;
        }

        return false;
    }

    bool RevealSpawnedWeapon(string weapon) {
        if (inventory.Count == 0) return false;

        if (currentWeapon != null && currentWeapon.GetName().Equals(weapon)) {
            SelectWeapon("Hand");
            return true;
        }

        foreach (var x in inventory) {
            if (x.GetName().Equals(weapon)) {
                UnselectCurrentWeapon();

                currentWeapon = x;
                currentWeapon.SetActive(true);
                return true;
            }
        }
        return false;
    }

    GameObject GetObjectPrefab(string weapon) {
        foreach(GameObject x in weaponsPrefabs) {
            if(x.tag.Equals(weapon)) return x; 
        }
        return null;
    }

    void InstantiateWeapon(string weapon) {
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

    public void DestroyWeapon(string weapon) {
        for (int i = 0; i < inventory.Count; ++i) {
            if (inventory[i].GetName().Equals(weapon)) {
                inventory[i].SetActive(false);
                inventory[i].transform.parent = null;

                Destroy(inventory[i].gameObject);
                inventory.RemoveAt(i);
                return;
            }
        }
    }

    public void SelectWeapon(string weapon) {
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
            animationManager.AttackStarted();
            currentWeapon.GetComponent<WeaponScript>().Attack();
        }
    }

    public void AttackFinished() {
        animationManager.AttackFinished();
    }

#endregion

#region MonoBehaviour Methods

    void Start() {
        SelectWeapon("Hand");
    }

#endregion

#region DEBUG (Delete afterwards the system works fine)

    public void DB_createWeapon(string weapon, int uses) {
        AddToAvailables(weapon);
    }

    public void DB_selectWeapon(string weapon) {
        SelectWeapon(weapon);
    }

    public void DB_deleteWeapon(string weapon) {
        DestroyWeapon(weapon);
    }

    public void DB_unselectCurrentWeapon() {
        UnselectCurrentWeapon();
    }

#endregion

}
