using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class SwordScript : MonoBehaviour {

    #region Parameters

    [Header("Sword Stabilizer")]
    [SerializeField] private Vector3 defaultRotation;
    private Transform giroCoconutTransform;
    [SerializeField] private RotationConstraint rotationConstraint;

    [Header("Sword Animation")]
    [SerializeField] private Vector3 scales;
    [SerializeField] private AnimationCurve swordScaleCurveLenght;
    [SerializeField] private AnimationCurve swordScaleCurveWidth;
    [SerializeField] private Transform bladeTransform;
    [SerializeField] public int levelOfPower { get; private set; }
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
    }

    IEnumerator delayConstraintRoutine(float time) {
        yield return new WaitForSeconds(time);
        rotationConstraint.constraintActive = false;
        restoreDefaultRotation();
    }

    #endregion

    #region Public Methods (Abstract probably)

    public void swordCollided() {
        emergencyStop = true;
    }

    public void Attack() {
        rotationConstraint.constraintActive = true;
        startAnim = true;
    }

    public void setLevelOfPower(int level) {
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
                rotationConstraint.constraintActive = false;
                restoreDefaultRotation();
                trailBlade.SetActive(false);
            }

            // If the sword expanded to the maximum or collided, lock that position for the remaining time to sync with
            // the arm animation.
            if (startAnim || emergencyStop) StartCoroutine(delayRestoreRoutine(animationDuration - (2.0f * deltaTime)));
            else restoreAnim = false;

            startAnim = false;
            stopAnim = true;
        }

        emergencyStop = false;
    }

    private void restoreDefaultRotation() {
        // Setting up z rotation (the one whose relative to the hand)
        Vector3 tmp = transform.localEulerAngles;
        tmp.z = defaultRotation.z;
        transform.localEulerAngles = tmp;

        // Setting up y rotation (the one whose relative to the player rotation)
        tmp = transform.eulerAngles;
        tmp.y = giroCoconutTransform.eulerAngles.y;
        transform.eulerAngles = tmp;
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

    private void Start() {
        #region Setting up GiroCoconut
        giroCoconutTransform = GameObject.FindGameObjectWithTag("GiroCoconut").transform;

        ConstraintSource tmp = new ConstraintSource();
        tmp.sourceTransform = giroCoconutTransform;
        tmp.weight = 1;

        rotationConstraint.SetSource(0,tmp);
        rotationConstraint.constraintActive = false;
        #endregion

        restoreDefaultRotation();
    }

    private void Update() {
        controlAnim();

        // Making the actual scale to the sword
        if (startAnim || restoreAnim) {
            bladeTransform.localScale = new Vector3(1.0f + scales.x * swordScaleCurveWidth.Evaluate(deltaTime),
                                                    1.0f + scales.y * swordScaleCurveLenght.Evaluate(deltaTime),
                                                    1.0f + scales.z * swordScaleCurveWidth.Evaluate(deltaTime));
        }
    }

    #endregion

}
