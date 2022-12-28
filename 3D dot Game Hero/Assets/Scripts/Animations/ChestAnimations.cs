using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimations : AnimationManager {
    [SerializeField] GameObject chestParticles;
    public void open() {
        running = true;
        animator.SetBool("Opened", true);
        chestParticles.GetComponent<ParticlesChest>().chestIsOpened();
    }

    public void close() {
        running = false;
        animator.SetBool("Opened", false);
    }

    public override void toIdle() {
        running = false;
        animator.SetBool("Opened", false);
    }

    public override void AttackStarted() { return; }

    public override void AttackFinished() { return; }

    public override void enableRunning(bool value) { return; }

    public override void toDeath() { return; }

    public override void toHit() { return; }
}
