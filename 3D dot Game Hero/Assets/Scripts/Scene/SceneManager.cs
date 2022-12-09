using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    #region Structs

    [System.Serializable]
    struct Location {
        [SerializeField] public Vector3 position;
        [SerializeField] public Vector3 rotation;
    }

    [System.Serializable]
    struct PrefabSpawn {
        [SerializeField] public GameObject prefab;
        [SerializeField] public Location location;
    }

    #endregion

    #region Parameters

    [Header("Camera Options")]
    [SerializeField] private Location cameraLocation;

    [Header("Prefabs to Spawn")]
    [SerializeField] private List<PrefabSpawn> prefabs;

    [Header("Light")]
    [SerializeField] private List<Location> lightsLocations;

    #endregion

    #region MonoBehaviour Methods

    void Start(){
        Camera.main.transform.position = cameraLocation.position;
        Camera.main.transform.eulerAngles = cameraLocation.rotation;

        foreach(PrefabSpawn x in prefabs) {
            Instantiate(x.prefab, x.location.position, Quaternion.Euler(x.location.rotation));
        }
    }

    void Update()
    {
        
    }

    #endregion

}
