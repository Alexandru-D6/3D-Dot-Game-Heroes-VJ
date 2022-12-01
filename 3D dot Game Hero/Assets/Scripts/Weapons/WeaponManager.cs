using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    List<Pair<string,int>> AvailableWeapons = new List<Pair<string, int>>();

    [SerializeField] private GameObject spawnParent;
    [SerializeField] private GameObject[] weaponsPrefabs;

    private GameObject currentWeapon;

    public bool[] debugBool = { false, false, false, false };

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

    GameObject getWeapon(string weapon) {
        foreach(GameObject x in weaponsPrefabs) {
            if(x.tag.Equals(weapon)) return x; 
        }
        return null;
    }

    void selectWeapon(string weapon) {
        if (findUses(weapon) > 0) {
            GameObject obj = getWeapon(weapon);
            obj = Instantiate(obj, new UnityEngine.Vector3(), new UnityEngine.Quaternion());
            obj.SetActive(false);
            obj.transform.parent = spawnParent.transform;
            obj.SetActive(true);
            currentWeapon = obj;
            StartCoroutine(lazyActive(2.0f, currentWeapon));
        } else {
            deleteWeapon(weapon);
        }
    }

    IEnumerator lazyActive(float time, GameObject obj) {
        yield return new WaitForSeconds(time);
    }

    private void Start() {
        insertWeapon("Sword", int.MaxValue);
    }

    private void Update() {
        if (debugBool[0]) {
            debugBool[0] = false;
            selectWeapon("Sword");
        }
    }

}
