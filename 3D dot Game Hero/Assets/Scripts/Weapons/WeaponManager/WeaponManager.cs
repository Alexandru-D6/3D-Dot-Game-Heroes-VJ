using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponManager : MonoBehaviour {

#region Parameters

    [Header("Managers")]
    [SerializeField] protected AnimationManager animationManager;

    [Space(10)]

    [Header("Inventory Management")]
    [SerializeField] protected List<GameObject> weaponsPrefabs;
    [SerializeField] protected List<Tags> weaponsAvailable;
    [SerializeField] protected List<WeaponScript> inventory;

    [Space(10)]

    [Header("References")]
    [SerializeField] protected GameObject hand;
    [SerializeField] protected WeaponScript currentWeapon = null;

#endregion

#region Inventory Management

    protected virtual bool IsWeaponAvailable(Tags weapon) {
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

    protected virtual bool RevealSpawnedWeapon(Tags weapon) {
        if (inventory.Count == 0) return false;

        // Returning true, the selection of another weapon while boomerang is flying while be invalid
        if (currentWeapon.transform.parent != hand.transform) return true;

        // Reset animation and button cooldown
        animationManager.toIdle();

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

    protected virtual GameObject GetObjectPrefab(Tags weapon) {
        foreach(GameObject x in weaponsPrefabs) {
            if(x.tag.Equals(weapon.ToString())) return x; 
        }
        return null;
    }

    protected virtual void InstantiateWeapon(Tags weapon) {
        GameObject prefab = GetObjectPrefab(weapon);
        GameObject obj = Instantiate(prefab, new UnityEngine.Vector3(), new UnityEngine.Quaternion());

        obj.SetActive(false);
        obj.transform.parent = hand.transform;

        UnselectCurrentWeapon();
        currentWeapon = obj.GetComponent<WeaponScript>();
        currentWeapon.SetWeaponManager(this);

        inventory.Add(currentWeapon);
        obj.SetActive(true);
    }

    protected virtual void UnselectCurrentWeapon() {
        if (currentWeapon != null) {
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }

#endregion

#region Public Methods

    public virtual void DestroyWeapon(Tags weapon) {
        for (int i = 0; i < inventory.Count; ++i) {
            if (inventory[i].GetName().Equals(weapon.ToString())) {
                UnselectCurrentWeapon();

                inventory[i].SetActive(false);
                inventory[i].transform.parent = null;

                Destroy(inventory[i].gameObject);
                inventory.RemoveAt(i);
                return;
            }
        }
    }

    public virtual void AddToAvailables(Tags weapon) {
        // Search if it's already exists
        foreach(Tags x in weaponsAvailable) {
            if (x.Equals(weapon)) return;
        }

        weaponsAvailable.Add(weapon);
    }

    public virtual void SelectWeapon(Tags weapon) {
        if (IsWeaponAvailable(weapon)) {
            if (RevealSpawnedWeapon(weapon)) return;

            // If it haven't spawned yet, do it.
            InstantiateWeapon(weapon);
        } else {
            // TODO: It may return an error or do something?
        }
    }

    public virtual void ReleaseCurrentWeapon() {
        if (currentWeapon != null) {
            currentWeapon.GetComponent<WeaponScript>().Release();
        }
    }

    public virtual void AbortAttack()
    {
        currentWeapon.Abort();
    }

    #endregion

    #region Abstract Methods

    public abstract void UseCurrentWeapon();

    public abstract void AttackFinished();

    #endregion

    #region MonoBehaviour Methods

    #endregion

}
