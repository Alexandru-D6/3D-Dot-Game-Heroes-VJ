using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {
    
    #region Parameters

    [Header("Inventory Management")]
    [SerializeField] private List<Pair<Tags,int>> itemsAvailable = new List<Pair<Tags, int>>();

#endregion

#region Public Methods

    public bool isStackableItem(Tags tag) {
        switch(tag) {
            case Tags.Coin:
                return true;
        }
        return false;
    }

    public void DestroyItem(Tags item) {
        for (int i = 0; i < itemsAvailable.Count; ++i) {
            if (itemsAvailable[i].First == item) {
                itemsAvailable.RemoveAt(i);
                return;
            }
        }
    }

    public bool IsItemAvailable(Tags item) {
        if (itemsAvailable.Count == 0) return false;

        foreach(var x in itemsAvailable) {
            if (x.First.Equals(item)) return true;
        }

        return false;
    }

    public void AddToAvailables(Tags item) {
        // Search if it's already exists
        if(item == Tags.Coin)UIPlayer.Instance.addCoin();
        else UIPlayer.Instance.UpdateInventoy(item, true);
        int i = 0;
        while (i < itemsAvailable.Count) {
            if (itemsAvailable[i].First == item) {
                if (!isStackableItem(item)) return;
                break;
            }
            ++i;
        }

        if (i != itemsAvailable.Count) {
            itemsAvailable[i].Second += 1;
        }
        else itemsAvailable.Add(new Pair<Tags, int>(item, 1));
    }

#endregion

#region MonoBehaviour Methods

    void Start() {
    }

#endregion
}
