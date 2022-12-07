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

    void addToAvailables(string weapon) {
        // Search if it's already exists
        foreach(string x in weaponsAvailable) {
            if (x.Equals(weapon)) return;
        }

        weaponsAvailable.Add(weapon);
    }

    bool isWeaponAvailable(string weapon) {
        // Search if it's spawned
        foreach(WeaponScript x in inventory) {
            if (x.getName().Equals(weapon)) return true;
        }

        // Search if it's available to spawn
        foreach(string x in weaponsAvailable) {
            if (x.Equals(weapon)) return true;
        }

        return false;
    }

    bool revealSpawnedWeapon(string weapon) {
        if (inventory.Count == 0) return false;

        foreach (var x in inventory) {
            if (x.getName().Equals(weapon)) {
                currentWeapon = x;
                currentWeapon.SetActive(true);
                return true;
            }
        }
        return false;
    }

    GameObject getObjectPrefab(string weapon) {
        foreach(GameObject x in weaponsPrefabs) {
            if(x.tag.Equals(weapon)) return x; 
        }
        return null;
    }

    void destroyWeapon(string weapon) {
        for (int i = 0; i < inventory.Count; ++i) {
            if (inventory[i].getName().Equals(weapon)) {
                inventory[i].SetActive(false);
                inventory[i].transform.parent = null;

                Destroy(inventory[i].gameObject);
                inventory.RemoveAt(i);
                return;
            }
        }
    }

    void unselectCurrentWeapon() {
        if (currentWeapon != null) {
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }

    void selectWeapon(string weapon) {
        if (isWeaponAvailable(weapon)) {
            // TODO: There is an edge case where the creation of this new weapon, not spawned yet, may fail
            //          if the prefab doesn't exist. It would be a great idea to select the previous weapon.
            unselectCurrentWeapon();

            if (revealSpawnedWeapon(weapon)) return;

            // If it haven't spawned yet, do it.
            GameObject prefab = getObjectPrefab(weapon);
            GameObject obj = Instantiate(prefab, new UnityEngine.Vector3(), new UnityEngine.Quaternion());

            obj.SetActive(false);
            obj.transform.parent = playerHand.transform;
            currentWeapon = obj.GetComponent<WeaponScript>();
            inventory.Add(currentWeapon);
            obj.SetActive(true);
        } else {
            // TODO: It may return an error or do something?
        }
    }

    public void useCurrentWeapon() {
        if (currentWeapon != null) {
            currentWeapon.GetComponent<WeaponScript>().Attack();
        }
    }

#endregion

#region DEBUG (Delete afterwards the system works fine)

    public void DB_createWeapon(string weapon, int uses) {
        addToAvailables(weapon);
    }

    public void DB_selectWeapon(string weapon) {
        selectWeapon(weapon);
    }

    public void DB_deleteWeapon(string weapon) {
        destroyWeapon(weapon);
    }

    public void DB_unselectCurrentWeapon() {
        unselectCurrentWeapon();
    }

#endregion

}
