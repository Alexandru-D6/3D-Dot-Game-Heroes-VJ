using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTextureManager : MonoBehaviour {

    #region Parameters

    [Header("Material")]
    [SerializeField] private Material material;
    [SerializeField] private Mesh mesh;

    private Material backupMaterial;
    private Mesh backupMesh;

    private MeshRenderer[] renderers;
    private MeshFilter[] meshes;

    #endregion

    #region MonoBehaviour Methods

    private void Start() {
        renderers = transform.GetComponentsInChildren<MeshRenderer>();
        meshes = transform.GetComponentsInChildren<MeshFilter>();
    }

    private void Update() {
        if (backupMaterial != material) {
            backupMaterial = material;

            foreach(var x in renderers) {
                x.material = backupMaterial;
            }
        }

        if (backupMesh != mesh) {
            backupMesh = mesh;
            foreach(var x in meshes) {
                x.mesh = backupMesh;
            }
        }
    }

    #endregion

}
