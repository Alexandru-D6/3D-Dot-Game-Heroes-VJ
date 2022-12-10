using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

#region Parameters

    [Header("Animator")]
    [SerializeField] protected Animator animator;

    [Header("Debug info, don't modify")]
    [SerializeField] protected bool running; 

#endregion

#region Virtual Methods

    public virtual void AttackStarted() {
        animator.Play("Attack Start");
    }

    public virtual void AttackFinished() {
        animator.Play("Attack Return");
    }

    public virtual void enableRunning(bool value) {
        running = value;
        animator.SetBool("Running", value);
    }

    public virtual void toIdle() {
        enableRunning(false);
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
