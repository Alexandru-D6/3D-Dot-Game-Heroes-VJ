using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    #region Singleton

    public static SceneManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    #endregion

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
    [SerializeField] private Camera _camera;
    [SerializeField] private Location cameraLocation;
    [SerializeField] private Vector2 offsetRooms;
    private GameObject currentRoom = null;

    [Header("Prefabs to Spawn")]
    [SerializeField] private List<PrefabSpawn> prefabs;

    [Header("Light")]
    [SerializeField] private List<Location> lightsLocations;

    #endregion

    #region Public Methods

    public void moveCamera(Vector2 direction, GameObject triggerRoom) {
        if (currentRoom == null) {
            currentRoom = triggerRoom;
            return;
        }

        if (currentRoom != null && triggerRoom == currentRoom) return;

        if (currentRoom != null && triggerRoom != currentRoom) {
            currentRoom = triggerRoom;
        }

        Vector2 tmp = offsetRooms * direction;

        Vector3 movement = new Vector3(tmp.x, 0.0f, tmp.y);
        movement += _camera.transform.position;

        _camera.transform.position = movement;
    }

    #endregion

    #region MonoBehaviour Methods

    void Start(){
        _camera = Camera.main;
        _camera.transform.position = cameraLocation.position;
        _camera.transform.eulerAngles = cameraLocation.rotation;

        foreach(PrefabSpawn x in prefabs) {
            Instantiate(x.prefab, x.location.position, Quaternion.Euler(x.location.rotation));
        }
    }

    void Update()
    {
        
    }

    #endregion

}
