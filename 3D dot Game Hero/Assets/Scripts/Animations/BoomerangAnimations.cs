using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangAnimations : AnimationManager {
    
    public void enableFlying(bool value) {
        running = value;
        animator.SetBool("Flying", running);
    }

    public override void toIdle() {
        animator.SetBool("Flying", false);
        animator.Play("Idle");
    }

    public override void AttackStarted() { return; }

    public override void AttackFinished() { return; }

    public override void enableRunning(bool value) { return; }

    public override void toDeath() { return; }

    public override void toHit() { return; }

}
