using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : AnimationManager {

    public void AttackStart() {
        animator.Play("Attack Start");
    }

    public void AttackReturn() {
        animator.Play("Attack Return");
    }

    public override void AttackStarted() { return; }

    public override void AttackFinished() { return; }
}
