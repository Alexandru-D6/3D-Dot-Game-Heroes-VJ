using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMangEnd : MonoBehaviour {

    [SerializeField] private Rigidbody rb;
    private RigidbodyConstraints rotationConstraint = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    public bool isBlocked = false;
    public bool isPushing = false;

    public void PushedFrom(ObstacleParts name) {
        if(!isBlocked)
        switch (name) {
            case ObstacleParts.Front:

                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | rotationConstraint;
                break;
            case ObstacleParts.Back:

                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | rotationConstraint;
                break;
            case ObstacleParts.Right:

                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | rotationConstraint;
                break;
            case ObstacleParts.Left:

                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | rotationConstraint;
                break;
        }
  
        
    }

    public void blockAll() {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Start() {
        blockAll();
    }

    private void Update() {
        if (!isPushing) blockAll();
    }

    public void Blocked()
    {
        isBlocked= true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}



