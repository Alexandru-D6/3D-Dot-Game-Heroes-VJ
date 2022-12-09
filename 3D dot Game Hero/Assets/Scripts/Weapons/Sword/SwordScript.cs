using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class SwordScript : WeaponScript {

#region Parameters

    [Header("Sword Animation")]
    [SerializeField] private Vector3 scales;
    [SerializeField] private AnimationCurve swordScaleCurveLenght;
    [SerializeField] private AnimationCurve swordScaleCurveWidth;
    [SerializeField] private Transform bladeTransform;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float rangeLimiter;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float animationDuration;

    [Header("Sword Animation Backend")]
    [SerializeField] private float deltaTime;
    [SerializeField] private bool startAnim = false;
    [SerializeField] private bool restoreAnim = false;
    private float startTime;
    private float lastTime;
    private bool stopAnim = true;
    private bool emergencyStop = false;

    [Header("Sword Trail")]
    [SerializeField] private GameObject originalBlade;
    [SerializeField] private GameObject whiteBlade;
    [SerializeField] private GameObject trailBlade;

#endregion

#region IEnumerators

    IEnumerator delayRestoreRoutine(float time) {
        yield return new WaitForSeconds(time);
        restoreAnim = true;
        AttackFinished();
    }

#endregion

#region Abstract Methods

    public override void Collided() {
        emergencyStop = true;
    }

    public override void Attack() {
        LockAxis(true, false, false);
        startAnim = true;
    }

#endregion

#region Virtual Methods

    public override void SetLevelOfPower(int level) {
        if (level < 0 || level > 2) return;

        levelOfPower = level;

        swordLevelRoutine();
    }

#endregion

#region Private Methods

    private void controlAnim() {

        // Start the scale of the sword (transition from 0.0f -> 1.0f)
        if (startAnim && stopAnim && !restoreAnim) {
            trailBlade.SetActive(true);
            startTime = Time.time;
            stopAnim = false;
        }

        // Start the restore the sword (transition from 1.0f -> 0.0f)
        if (restoreAnim && stopAnim && !startAnim) {
            startTime = Time.time;
            lastTime = startTime + deltaTime;
            stopAnim = false;
        }

        // Updates the correct variable to create deltaTime since the start
        // of the animation.
        // --> startTime is static and the original time from start, so we update lastTime until the difference is >= swordScaleCurve time.
        // <-- lastTime is the time to achieve, so we update startTime until the diference is <= 0.0f.
        if (!stopAnim) {
            if (restoreAnim) startTime = Time.time;
            else lastTime = Time.time;
        }

        deltaTime = lastTime - startTime;

        if ((deltaTime >= swordScaleCurveLenght.keys[swordScaleCurveLenght.length - 1].time * rangeLimiter && startAnim) ||
                (deltaTime <= 0.0 && restoreAnim) || (emergencyStop && startAnim)) {

            // If the sword achieve the restore of the sword
            if (restoreAnim) {
                trailBlade.SetActive(false);
            }

            // If the sword expanded to the maximum or collided, lock that position for the remaining time to sync with
            // the arm animation.
            if (startAnim || emergencyStop) StartCoroutine(delayRestoreRoutine(animationDuration - (2.0f * deltaTime)));
            else {
                restoreAnim = false;
                LockAxis(false, true, true);
            }

            startAnim = false;
            stopAnim = true;
        }

        emergencyStop = false;
    }

    private void LockAxis(bool x, bool y, bool z) {
        rotationConstraint.enabled = false;
        transform.localEulerAngles = defaultRotation;
        rotationConstraint.enabled = true;

        Axis lockedAxis = Axis.None | ((x == true) ? Axis.X : Axis.None) | ((y == true) ? Axis.Y : Axis.None) | ((z == true) ? Axis.Z : Axis.None);
        rotationConstraint.rotationAxis = lockedAxis;
    }

    private void switchBlade(bool isOriginalSword) {
        originalBlade.SetActive(isOriginalSword);
        whiteBlade.SetActive(!isOriginalSword);
    }

    private void swordLevelRoutine() {
        switch(levelOfPower) {
            case 0:
                switchBlade(true);
                rangeLimiter = 0.8f;
                break;

            case 1:
                switchBlade(true);
                rangeLimiter = 1.0f;
                break;

            case 2:
                switchBlade(false);
                rangeLimiter = 1.0f;
                break;
        }
    }

#endregion

#region MonoBehaviour Methods

    public override void Start() {
        base.Start();

        usesLeft = int.MaxValue;

        swordLevelRoutine();

        LockAxis(false, true, true);
    }

    private void Update() {
        controlAnim();

        // Making the actual scale to the sword
        if (startAnim || restoreAnim) {
            bladeTransform.localScale = new Vector3(1.0f + scales.x * swordScaleCurveLenght.Evaluate(deltaTime),
                                                    1.0f + scales.y * swordScaleCurveLenght.Evaluate(deltaTime),
                                                    1.0f + scales.z * swordScaleCurveWidth.Evaluate(deltaTime));
        }
    }

#endregion

}
