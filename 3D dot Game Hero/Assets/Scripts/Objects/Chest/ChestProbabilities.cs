using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestProbabilities : MonoBehaviour {

    #region Structs

    [System.Serializable]
    struct ChestProbability {
        public Tags tag;
        public int probability;
    }

    #endregion

    #region Parameters

    [Header("Reference")]
    [SerializeField] private ChestSingleton chestSingleton;

    [Header("Parameters")]
    [SerializeField] private List<ChestProbability> probabilities;
    // TODO: maybe we can use this total Probability in the algorithm that choose the random number
    [SerializeField] private int totalProbability;
    private int currentTry;
    [SerializeField] private int maxTry;

    #endregion

    #region Public Methods

    public Tags RollAnItem() {
        Tags aux = GetRandomItem();
        currentTry = 0;
        return aux;
    }

    #endregion

    #region Private Methods

    public Tags GetRandomItem() {
        if (currentTry < maxTry) {
            int res = Random.Range(0,101);
            Tags tag = FindTag(res);

            if (tag == Tags.Uknown) {
                currentTry++;
                return GetRandomItem();
            }else {
                return tag;
            }
        }else return (Random.Range(0,2) == 0) ? Tags.Coin : Tags.Bomb;
    }

    public Tags FindTag(int value) {
        int aux = 0;
        foreach(var x in probabilities) {
            if (value < aux + x.probability && value >= aux) {
                if (ChestSingleton.GetInstance().IsAvailable(x.tag)) {
                    ChestSingleton.GetInstance().UseTag(x.tag);
                    return x.tag;
                }else {
                    return Tags.Uknown;
                }
            } else aux += x.probability;
        }
        return Tags.Uknown;
    }

    #endregion

    #region MonoBehaviour Methods

    void Start() {
    }

    #endregion

}
