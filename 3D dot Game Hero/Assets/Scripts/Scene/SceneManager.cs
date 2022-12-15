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

    [Header("Prefabs to Spawn")]
    [SerializeField] private List<PrefabSpawn> prefabs;

    [Header("Light")]
    [SerializeField] private List<Location> lightsLocations;

    [Header("Room Change")]
    [SerializeField] private Vector2 offsetRooms;
    [SerializeField] private Vector2 offsetDoor;
    private GameObject currentRoom = null;

    #endregion

    #region Public Methods

    public void ChangeRoom(Vector2 direction, GameObject triggerRoom) {
        if (currentRoom != null && triggerRoom != currentRoom) {
            currentRoom = triggerRoom;
            return;
        }

        currentRoom = triggerRoom;

        Transform player = PlayerManager.Instance.transform;
        PlayerManager.Instance.PassDoor(player.position + new Vector3(direction.x * offsetDoor.x, 0.0f, direction.y * offsetDoor.y));

        Vector2 tmp = offsetRooms * direction;
        Vector3 movement = new Vector3(tmp.x, 0.0f, tmp.y);
        movement += _camera.transform.position;
        _camera.GetComponent<CameraSmoothMovement>().MoveTo(movement);
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
