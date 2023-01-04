using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaclePart : MonoBehaviour {

    [SerializeField] private ObstacleParts namePart;
    [SerializeField] private ObstacleManager wholeObstacle;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != null && other.transform.parent.gameObject.layer == (int)Layers.Player || other.gameObject.layer == (int)Layers.Player ) {
            wholeObstacle.PushedFrom(namePart);
            wholeObstacle.isPushing = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.transform.parent != null && other.transform.parent.gameObject.layer == (int)Layers.Player || other.gameObject.layer == (int)Layers.Player ) {
            wholeObstacle.PushedFrom(namePart);
            wholeObstacle.isPushing = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.parent != null && other.transform.parent.gameObject.layer == (int)Layers.Player || other.gameObject.layer == (int)Layers.Player) {
            wholeObstacle.isPushing = false;
        }
    }
}
