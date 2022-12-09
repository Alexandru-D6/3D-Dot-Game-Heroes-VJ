using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {
    
    #region Parameters

    [Header("Inventory Management")]
    [SerializeField] private List<Tags> itemsAvailable;

#endregion

#region Public Methods

    public void DestroyItem(Tags item) {
        for (int i = 0; i < itemsAvailable.Count; ++i) {
            if (itemsAvailable[i] == item) {
                itemsAvailable.RemoveAt(i);
                return;
            }
        }
    }

    public bool IsItemAvailable(Tags item) {
        foreach(Tags x in itemsAvailable) {
            if (x.Equals(item)) return true;
        }

        return false;
    }

    public void AddToAvailables(Tags item) {
        // Search if it's already exists
        foreach(Tags x in itemsAvailable) {
            if (x == item) return;
        }

        itemsAvailable.Add(item);
    }

#endregion

#region MonoBehaviour Methods

    void Start() {
    }

#endregion
}
