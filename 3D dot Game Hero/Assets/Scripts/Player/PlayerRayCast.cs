using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour {

    [SerializeField] private Vector3 offsetRaycast;
    [SerializeField] private float raycastLenght;

    void Update() {
        Vector3 currentPos = transform.position + offsetRaycast;

        if (Physics.Raycast(currentPos, transform.forward, out RaycastHit hitInfo, raycastLenght)) {
            if (hitInfo.transform.tag.Equals(Tags.Enderman.ToString())) {
                hitInfo.transform.GetComponent<EndermanMov>().alertEnderman();
            }
        }
    }
}
