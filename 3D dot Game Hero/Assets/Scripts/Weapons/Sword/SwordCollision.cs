using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    [SerializeField] private SwordScript swordScript;

    [SerializeField] private BoxCollider bc;
    [SerializeField] private Vector3 increasedCenter;
    [SerializeField] private Vector3 increasedSize;
    [SerializeField] private Vector3 originalCenter;
    [SerializeField] private Vector3 originalSize;

    void setBoxColliderSize(Vector3 center, Vector3 size) {
        bc.center = center;
        bc.size = size;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Wall")) {
            swordScript.swordCollided();
            setBoxColliderSize(originalCenter, originalSize);

        }
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Wall")) {
            swordScript.swordCollided();
            setBoxColliderSize(originalCenter, originalSize);
        }
    }

    private void OnCollisionExit(Collision collision) {
        setBoxColliderSize(increasedCenter, increasedSize);
    }
}
