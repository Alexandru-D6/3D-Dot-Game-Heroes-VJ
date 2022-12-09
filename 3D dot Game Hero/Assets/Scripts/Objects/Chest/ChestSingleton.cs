using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSingleton : MonoBehaviour {

    #region Singleton

    public static ChestSingleton Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    #endregion

    #region Structs

    [System.Serializable]
    public struct PoolObject {
        public Tags tag;
        public int usesLeft;
    }

    #endregion

    #region Parameters

    [Header("Available Tags in Pool")]
    [SerializeField] private List<PoolObject> availableObjectsList;
    [SerializeField] private bool syncListState;
    private Dictionary<Tags, int> availableObjects;


    #endregion

    #region Public Methods

    public bool IsAvailable(Tags tag) {
        return availableObjects.ContainsKey(tag);
    }

    public bool UseTag(Tags tag) {
        if (availableObjects.ContainsKey(tag)) {
            availableObjects[tag]--;
            decrementFromList(tag);
            if (availableObjects[tag] == 0) availableObjects.Remove(tag);
            return true;
        }
        return false;
    }

    #endregion

    #region Private Methods

    private void SyncList() {
        availableObjects = new Dictionary<Tags, int>();

        foreach(var x in availableObjectsList) {
            availableObjects.Add(x.tag, x.usesLeft);
        }

        syncListState = false;
    }

    private void decrementFromList(Tags tag) {
        for(int i = 0; i < availableObjectsList.Count; ++i) {
            if (availableObjectsList[i].tag == tag) {
                PoolObject temp = new PoolObject();
                temp.tag = tag;
                temp.usesLeft = availableObjectsList[i].usesLeft - 1;
                availableObjectsList[i] = temp;
                return;
            }
        }
    }

    #endregion

    #region MonoBehaviour Methods

    private void Start() {
        syncListState = true;
        SyncList();
    }

    private void Update() {
        if (syncListState) SyncList();
    }

    #endregion

}
