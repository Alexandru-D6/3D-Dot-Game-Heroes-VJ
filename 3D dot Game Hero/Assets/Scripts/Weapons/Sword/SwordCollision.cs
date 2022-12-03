using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    [SerializeField] private SwordScript swordScript;

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Wall")) {
            swordScript.swordCollided();
        }
    }
}
