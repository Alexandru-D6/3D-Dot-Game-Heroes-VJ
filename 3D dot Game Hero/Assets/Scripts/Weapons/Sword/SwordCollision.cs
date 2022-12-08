using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour {

    #region Parameters

    [Header("Sword script for callbacks")]
    [SerializeField] private SwordScript swordScript;

    [Header("BoxColliders info for dynamic size (better precision when collision)")]
    [SerializeField] private BoxCollider bc;
    [SerializeField] private Vector3 increasedCenter;
    [SerializeField] private Vector3 increasedSize;
    [SerializeField] private Vector3 originalCenter;
    [SerializeField] private Vector3 originalSize;

    #endregion

    #region Private Methods

    private void setBoxColliderSize(Vector3 center, Vector3 size) {
        bc.center = center;
        bc.size = size;
    }

    #endregion

    #region On Collision Methods

    // TODO: refactor must be needed to use Layers instead of Tag.
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Wall")) {
            swordScript.Collided();
            setBoxColliderSize(originalCenter, originalSize);

        }
    }

    // TODO: refactor must be needed to use Layers instead of Tag.
    private void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Wall")) {
            swordScript.Collided();
            setBoxColliderSize(originalCenter, originalSize);
        }
    }

    // TODO: refactor must be needed to use Layers instead of Tag to
    //          avoid false positives when colliding with mobs (?)  
    private void OnCollisionExit(Collision collision) {
        setBoxColliderSize(increasedCenter, increasedSize);
    }

    #endregion

}
