using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerAnimations : AnimationManager {

    [SerializeField] GameObject runningParticles;

    public void AttackStart() {
        animator.Play("Attack Start");
    }

    public void AttackReturn() {
        animator.Play("Attack Return");
    }

    public override void AttackStarted() { return; }

    public override void AttackFinished() { return; }

    public override void enableRunning(bool value)
    {
        base.enableRunning(value);
        runningParticles.SetActive(value);

    }
}
