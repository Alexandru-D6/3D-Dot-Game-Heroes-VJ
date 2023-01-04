using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/VaseSpawnInfo", order = 1)]
public class VaseSpawnInfo : ScriptableObject
{

    [System.Serializable]
    public struct Coords
    {
        
        public Vector3 position;
    }

    [SerializeField] public List<Coords> Vases;
}