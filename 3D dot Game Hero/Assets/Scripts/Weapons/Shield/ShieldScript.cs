using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ShieldScript : MonoBehaviour {
    
    [SerializeField] private PlayerAnimations animator;
    [SerializeField] private RotationConstraint rotationConstraint;
    [SerializeField] private Collider _collider;
    private bool isHolding = false;

    private void OnTriggerEnter(Collider other) {
        Tags tag = TagsUtils.GetTag(other.tag);

        if (tag == Tags.ExplosionCreeper || tag == Tags.ExplotionBomb) {
            PlayerManager.Instance.GiveInmortality(0.2f);
        }
    }

    public void EnableShield() {
        animator.EnableShield(true);
        Axis lockedAxis = Axis.X | Axis.Y | Axis.Z;
        rotationConstraint.rotationAxis = lockedAxis;

        isHolding = true;
        _collider.enabled = true;
    }

    public void DisableShield() {
        animator.EnableShield(false);
        Axis lockedAxis = Axis.None;
        rotationConstraint.rotationAxis = lockedAxis;

        isHolding = false;
        _collider.enabled = false;
    }

    private void Update() {
        if (!isHolding) {
            transform.localEulerAngles = new Vector3(0.0f,0.0f,0.0f);
        }
    }

}
