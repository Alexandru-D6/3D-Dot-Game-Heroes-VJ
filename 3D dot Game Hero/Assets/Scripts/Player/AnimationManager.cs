using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

#region Parameters

    [Header("Animator")]
    [SerializeField] protected Animator animator;

    [Header("Debug info, don't modify")]
    [SerializeField] private bool running; 

#endregion

#region Virtual Methods

    public virtual void AttackStarted() {
        animator.Play("Attack Started");
    }

    public virtual void AttackFinished() {
        animator.Play("Attack Finished");
    }

    public virtual void enableRunning(bool value) {
        running = value;
        animator.SetBool("Running", value);
    }

    public virtual void toIdle() {
        animator.SetBool("Running", false);
        animator.Play("Idle");
    }

    public virtual void toDeath() {
        animator.SetTrigger("Death");
    }

    public virtual void toHit() {
        animator.SetTrigger("Hit");
    }

#endregion

}
