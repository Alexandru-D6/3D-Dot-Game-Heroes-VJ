using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ArrowScript : MonoBehaviour {

    #region Parameters

    [Header("References")]
    private GameObject sceneObjects;

    [Header("Parameters")]
    [SerializeField] private Vector3 direction = Vector3.zero;

    #endregion

    public void DestroyArrow() {
        transform.parent = null;
        Destroy(gameObject);
    }

    public void ShootArrow(Vector3 direction) {
        transform.parent = sceneObjects.transform;
        direction = direction.normalized;
    }

    private void Start() {
        sceneObjects = GameObject.FindGameObjectWithTag(Tags.SceneObjects.ToString());
    }

    private void Update() {
        transform.Translate(direction * Time.deltaTime, Space.World);
    }
}
