using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ArrowScript : MonoBehaviour {

    #region Parameters

    [Header("References")]
    private GameObject sceneObjects;

    [Header("Parameters")]
    private Vector3 direction = Vector3.zero;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Layers originalLayer;

    #endregion

    public void DestroyArrow() {
        transform.parent = null;
        Destroy(gameObject);
    }

    public void ShootArrow(Vector3 dir) {
        transform.parent = sceneObjects.transform;
        direction = dir.normalized;
    }

    public void SetLayer(Layers layer) {
        originalLayer = layer;
    }

    private void Start() {
        sceneObjects = GameObject.FindGameObjectWithTag(Tags.SceneObjects.ToString());
    }

    private void Update() {
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }
}
