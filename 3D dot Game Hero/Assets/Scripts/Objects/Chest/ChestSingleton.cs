using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSingleton {

    #region Singleton

    private static ChestSingleton Instance;

    public static ChestSingleton GetInstance() {
        if (Instance == null) {
            Instance = new ChestSingleton();
            Instance.Start();
        }

        return Instance;
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

    [Header("References")]
    private PlayerManager playerManager;

    [Header("Available Tags in Pool")]
    private Dictionary<Tags, int> availableObjects = new Dictionary<Tags, int>() {
        { Tags.Bomb,        10      },
        { Tags.Shield,      1       },
        { Tags.Sword,       1       },
        { Tags.Boomerang,   1       },
        { Tags.Bow,         1       },
        { Tags.EnderKey,    1       },
        { Tags.GoldenKey,   1       },
        { Tags.SkullKey,    1       },
        { Tags.BossKey,     1       },
        { Tags.Hamburguer,  20      },
        { Tags.Coin,        100     },
    };


    #endregion

    #region Public Methods

    public bool IsAvailable(Tags tag) {
        return availableObjects.ContainsKey(tag);
    }

    public bool UseTag(Tags tag) {
        if (availableObjects.ContainsKey(tag)) {
            // Update the state of drops
            availableObjects[tag]--;
            if (availableObjects[tag] == 0) availableObjects.Remove(tag);

            // Send that item to the player
            PlayerManager.Instance.ReceiveItem(tag);

            return true;
        }
        return false;
    }

    public void RestoreObject() {
        Instance = new ChestSingleton();
    }

    #endregion

    #region MonoBehaviour Methods

    public void Start() {
        playerManager = PlayerManager.Instance;
    }
    #endregion

}
