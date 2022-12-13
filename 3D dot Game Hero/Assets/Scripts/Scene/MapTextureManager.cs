using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTextureManager : MonoBehaviour {

    #region Parameters

    [Header("Material")]
    [SerializeField] private Material material;
    [SerializeField] private Mesh mesh;
    [SerializeField] private Tags targetTag;

    [SerializeField] private Material transparentMaterial;
    [SerializeField] private Camera _camera;

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
                if (!x.tag.Equals(targetTag.ToString())) return;

                Vector3 localForward = x.transform.forward;
                Vector3 cameraForward = _camera.transform.forward;

                if (Vector3.Dot(localForward, cameraForward) <= 0) x.material = backupMaterial;
                else x.material = transparentMaterial;
            }
        }

        if (backupMesh != mesh) {
            backupMesh = mesh;
            foreach(var x in meshes) {
                if (!x.tag.Equals(targetTag.ToString())) return;

                x.mesh = backupMesh;
            }
        }
    }

    #endregion

}
