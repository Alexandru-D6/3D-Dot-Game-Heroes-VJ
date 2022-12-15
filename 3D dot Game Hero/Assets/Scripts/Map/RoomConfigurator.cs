using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RoomConfigurator : MonoBehaviour {

    #region Parameters

    [Header("Wall Textures")]
    [SerializeField] private TexturesObject wallTextures;
    [SerializeField] private Tags wallTag;
    private List<MeshRenderer> wallRenderers = new List<MeshRenderer>();
    private List<MeshFilter> wallMeshes = new List<MeshFilter>();

    [Header("Ground Textures")]
    [SerializeField] private TexturesObject groundTextures;
    [SerializeField] private Tags groundTag;
    private List<MeshRenderer> groundRenderers = new List<MeshRenderer>();
    private List<MeshFilter> groundMeshes = new List<MeshFilter>();

    [Header("Doors GameObjects")]
    [SerializeField] private List<GameObject> doorsGameObjects;

    [Header("Doors Trigger GameObjects")]
    [SerializeField] private List<GameObject> doorsTriggerGameObjects;

    [Header("Pilars GameObjects")]
    [SerializeField] private List<GameObject> pilarsGameObjects;

    #endregion

    #region Public Methods

    public void UpdateDoor(int index) {
        doorsGameObjects[index % doorsGameObjects.Count].SetActive(!doorsGameObjects[index % doorsGameObjects.Count].activeInHierarchy);
        doorsTriggerGameObjects[index % doorsTriggerGameObjects.Count].SetActive(!doorsGameObjects[index % doorsGameObjects.Count].activeInHierarchy);
    }

    public void UpdatePilars(int index) {
        pilarsGameObjects[index % pilarsGameObjects.Count].SetActive(!pilarsGameObjects[index % pilarsGameObjects.Count].activeInHierarchy);
    }

    public void ChangeWalls() {
        foreach(var x in wallRenderers) {
            x.material = wallTextures.material;
        }

        foreach(var x in wallMeshes) {
            x.mesh = wallTextures.mesh;
        }
    }

    public void ChangeGrounds() {
        foreach(var x in groundRenderers) {
            if (!x.tag.Equals(groundTag.ToString())) return;

            x.material = groundTextures.material;
        }

        foreach(var x in groundMeshes) {
            if (!x.tag.Equals(groundTag.ToString())) return;

            x.mesh = groundTextures.mesh;
        }
    }

    public void GetWalls() {
        wallRenderers.Clear();
        wallMeshes.Clear();

        wallRenderers.AddRange(transform.GetComponentsInChildren<MeshRenderer>());
        wallMeshes.AddRange(transform.GetComponentsInChildren<MeshFilter>());

        int deletedItems = 0;
        int size = wallRenderers.Count;
        for (int i = 0; i < size; ++i) {
            if (!wallRenderers[i - deletedItems].tag.Equals(wallTag.ToString())) {
                wallRenderers.RemoveAt(i - deletedItems);
                deletedItems++;
            }
        }

        deletedItems = 0;
        size = wallMeshes.Count;
        for (int i = 0; i < size; ++i) {
            if (!wallMeshes[i - deletedItems].tag.Equals(wallTag.ToString())) {
                wallMeshes.RemoveAt(i - deletedItems);
                deletedItems++;
            }
        }
    }

    public void GetGrounds() {
        groundRenderers.Clear();
        groundMeshes.Clear();

        groundRenderers.AddRange(transform.GetComponentsInChildren<MeshRenderer>());
        groundMeshes.AddRange(transform.GetComponentsInChildren<MeshFilter>());

        int deletedItems = 0;
        int size = groundRenderers.Count;
        for (int i = 0; i < size; ++i) {
            if (!groundRenderers[i - deletedItems].tag.Equals(groundTag.ToString())) {
                groundRenderers.RemoveAt(i - deletedItems);
                deletedItems++;
            }
        }

        deletedItems = 0;
        size = groundMeshes.Count;
        for (int i = 0; i < size; ++i) {
            if (!groundMeshes[i - deletedItems].tag.Equals(groundTag.ToString())) {
                groundMeshes.RemoveAt(i - deletedItems);
                deletedItems++;
            }
        }
    }

    #endregion
}
