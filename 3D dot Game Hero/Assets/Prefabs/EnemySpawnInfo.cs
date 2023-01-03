using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemySpawnInfo", order = 1)]
public class EnemySpawnInfo : ScriptableObject {

    [System.Serializable]
    public struct SpawnInfo {
        public GameObject prefab;
        public Vector3 position;
    }
    
    [SerializeField] public List<SpawnInfo> Enemies;
}
