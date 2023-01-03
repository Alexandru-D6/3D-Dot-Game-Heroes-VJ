using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ShieldScript : MonoBehaviour {
    
    [SerializeField] private PlayerAnimations animator;
    [SerializeField] private RotationConstraint rotationConstraint;
    private bool isHolding = false;

    public void EnableShield() {
        animator.EnableShield(true);
        Axis lockedAxis = Axis.X | Axis.Y | Axis.Z;
        rotationConstraint.rotationAxis = lockedAxis;

        isHolding = true;
    }

    public void DisableShield() {
        animator.EnableShield(false);
        Axis lockedAxis = Axis.None;
        rotationConstraint.rotationAxis = lockedAxis;

        isHolding = false;
    }

    private void Update() {
        if (!isHolding) {
            transform.localEulerAngles = new Vector3(0.0f,0.0f,0.0f);
        }
    }

}
