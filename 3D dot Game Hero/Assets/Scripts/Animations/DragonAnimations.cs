using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DragonAnimations : AnimationManager {

    bool flying = false;
    [SerializeField] private ParticleSystem flamethrowerParticle;

    public virtual void toIdle() {
        enableRunning(false);
        animator.Play("Idle");
    }

    public void forceRunning() {
        running = true;
        flying = false;
        animator.Play("Walk");
    }

    public override void enableRunning(bool value) {
        running = value;
        flying = false;
        animator.SetBool("Walk", running);
        animator.SetBool("Fly", flying);
    }

    public void forceFlying() {
        running = false;
        flying = true;
        animator.Play("Fly");
    }

    public void enableFlying(bool value) {
        running = false;
        flying = value;
        animator.SetBool("Walk", running);
        animator.SetBool("Fly", flying);
    }

    public void Breath_Gs() {
        animator.Play("Breath_Gs");
    }

    public void Breath_Gw() {
        animator.Play("Breath_Gw");
    }

    public void Breath_Fs() {
        animator.Play("Breath_Fs");
    }

    public void Breath_Fw() {
        animator.Play("Breath_Fw");
    }

    public void AttackClawL() {
        animator.Play("Atk_Claw_L");
    }

    public void AttackClawR() {
        animator.Play("Atk_Claw_R");
    }

    public void SetFlamethrower(bool value) {
        if (value) flamethrowerParticle.Play();
        else flamethrowerParticle.Stop();
    }

    public override void AttackStarted() { return; }

    public override void AttackFinished() { return; }

    public override void toHit() { return; }
}
