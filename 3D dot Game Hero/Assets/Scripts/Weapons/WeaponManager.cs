using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    List<Pair<string,int>> AvailableWeapons = new List<Pair<string, int>>();

    [SerializeField] private GameObject spawnParent;
    [SerializeField] private GameObject[] weaponsPrefabs;

    private GameObject currentWeapon = null;
    private List<GameObject> inventory = new List<GameObject>(0);

    void deleteWeapon(string weapon) {
        for (int i = 0; i < AvailableWeapons.Count; ++i) {
            if(AvailableWeapons[i].First.Equals(weapon)) {
                AvailableWeapons.RemoveAt(i);
            }
        }
    }

    void insertWeapon(string weapon, int uses) {
        AvailableWeapons.Add(new Pair<string,int>(weapon, uses));
    }

    int findUses(string weapon) {
        foreach(var x in AvailableWeapons) {
            if (x.First.Equals(weapon)) {
                return x.Second;
            }
        }
        return 0;
    }

    void decrementWeaponUses(string weapon) {
        foreach(var x in AvailableWeapons) {
            if(x.First.Equals(weapon)) {
                x.Second -= 1;
            }
        }
    }

    GameObject getWeaponForSpawn(string weapon) {
        foreach(GameObject x in weaponsPrefabs) {
            if(x.tag.Equals(weapon)) return x; 
        }
        return null;
    }

    bool revealSpawnedWeapon(string weapon) {
        if (inventory.Count == 0) return false;

        foreach (var x in inventory) {
            if (x.tag.Equals(weapon)) {
                if (currentWeapon != null) unselectCurrentWeapon();

                currentWeapon = x;
                currentWeapon.SetActive(x);
                return true;
            }
        }
        return false;
    }

    void selectWeapon(string weapon) {
        if (findUses(weapon) > 0) {
            if (revealSpawnedWeapon(weapon)) return;
            GameObject obj = getWeaponForSpawn(weapon);
            obj = Instantiate(obj, new UnityEngine.Vector3(), new UnityEngine.Quaternion());
            obj.SetActive(false);
            obj.transform.parent = spawnParent.transform;
            obj.SetActive(true);
            currentWeapon = obj;
            inventory.Add(obj);
            StartCoroutine(lazyActive(2.0f, currentWeapon));
        } else {
            destroyWeapon(weapon);
        }
    }

    void unselectCurrentWeapon() {
        if (currentWeapon != null) {
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }

    void destroyWeapon(string weapon) {
        for (int i = 0; i < inventory.Count; ++i) {
            if (inventory[i].tag.Equals(weapon)) {
                deleteWeapon(weapon);
                inventory[i].transform.parent = null;
                inventory[i].SetActive(false);
                Destroy(inventory[i]);
                inventory.RemoveAt(i);
                return;
            }
        }
    }

    public void useCurrentWeapon() {
        if (currentWeapon != null) {
            decrementWeaponUses(currentWeapon.tag);
            currentWeapon.GetComponent<SwordScript>().Attack();
        }
    }

    IEnumerator lazyActive(float time, GameObject obj) {
        yield return new WaitForSeconds(time);
    }

    #region DEBUG

    public void DB_createWeapon(string weapon, int uses) {
        insertWeapon(weapon, uses);
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
