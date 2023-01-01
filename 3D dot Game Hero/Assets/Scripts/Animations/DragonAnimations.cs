using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimations : AnimationManager {

    bool flying = false;
    [SerializeField] private GameObject mouthReference;
    [SerializeField] private GameObject flamethrowerPrefab;

    public virtual void toIdle() {
        enableRunning(false);
        animator.Play("Idle");
    }

    public override void enableRunning(bool value) {
        running = value;
        flying = false;
        animator.SetBool("Walk", running);
        animator.SetBool("Fly", flying);
    }

    public void enableFlying(bool value) {
        running = false;
        flying = value;
        animator.SetBool("Walk", running);
        animator.SetBool("Fly", flying);
    }

    public void Breath_Gs() {
        animator.Play("Breath_Gs");
        Instantiate(flamethrowerPrefab, mouthReference.transform);
    }

    public void Breath_Gw() {
        animator.Play("Breath_Gw");
        Instantiate(flamethrowerPrefab, mouthReference.transform);
    }

    public void Breath_Fs() {
        animator.Play("Breath_Fs");
        Instantiate(flamethrowerPrefab, mouthReference.transform);
    }

    public void Breath_Fw() {
        animator.Play("Breath_Fw");
        Instantiate(flamethrowerPrefab, mouthReference.transform);
    }

    public void AttackClawL() {
        animator.Play("Atk_Claw_L");
    }

    public void AttackClawR() {
        animator.Play("Atk_Claw_R");
    }

    public override void AttackStarted() { return; }

    public override void AttackFinished() { return; }

    public override void toHit() { return; }
}
