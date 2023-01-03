using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePart : MonoBehaviour {

    [SerializeField] private ObstacleParts namePart;
    [SerializeField] private ObstacleManager wholeObstacle;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != null && other.transform.parent.gameObject.layer == (int)Layers.Player || other.gameObject.layer == (int)Layers.Player || other.tag == Tags.MovableObstacle.ToString()) {
            wholeObstacle.PushedFrom(namePart);
            wholeObstacle.isPushing = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.transform.parent != null && other.transform.parent.gameObject.layer == (int)Layers.Player || other.gameObject.layer == (int)Layers.Player || other.tag == Tags.MovableObstacle.ToString()) {
            wholeObstacle.PushedFrom(namePart);
            wholeObstacle.isPushing = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.parent != null && other.transform.parent.gameObject.layer == (int)Layers.Player || other.gameObject.layer == (int)Layers.Player || other.tag == Tags.MovableObstacle.ToString()) {
            wholeObstacle.isPushing = false;
        }
    }
}
