using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRayCast : MonoBehaviour {

    [SerializeField] private Vector3 offsetRaycast;
   

    public bool isSeeingTheObjective(Vector3 postarget) {
        Vector3 currentPos = transform.position + offsetRaycast;
        float raycastLenght = Vector3.Distance(postarget, transform.position);
        if (Physics.Raycast(currentPos, transform.forward, out RaycastHit hitInfo, raycastLenght)) {
            return hitInfo.transform.tag.Equals(Tags.Wall.ToString());  
        }
        return false;
    }
}
