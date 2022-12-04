using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class SwordScript : MonoBehaviour {

    [SerializeField] private Vector3 defaultRotation;
    private Transform giroCoconutTransform;
    [SerializeField] private RotationConstraint rotationConstraint;

    [SerializeField] private Vector3 scales;
    [SerializeField] private AnimationCurve swordScaleCurveLenght;
    [SerializeField] private AnimationCurve swordScaleCurveWidth;
    [SerializeField] private Transform bladeTransform;

    private float startTime;
    private float lastTime;
    [SerializeField] private float deltaTime;

    private bool stopAnim = true;
    private bool emergencyStop = false;
    [SerializeField] private bool startAnim = false;
    [SerializeField] private bool restoreAnim = false;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float rangeLimiter;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float animationDuration;

    private void controlAnim() {
        if (startAnim && stopAnim && !restoreAnim) {
            startTime = Time.time;
            stopAnim = false;
        }

        if (restoreAnim && stopAnim && !startAnim) {
            startTime = Time.time;
            lastTime = startTime + deltaTime;
            stopAnim = false;
        }

        if (!stopAnim) {
            if (restoreAnim) startTime = Time.time;
            else lastTime = Time.time;
        }

        deltaTime = lastTime - startTime;

        if ((deltaTime >= swordScaleCurveLenght.keys[swordScaleCurveLenght.length - 1].time * rangeLimiter && startAnim) ||
                (deltaTime <= 0.0 && restoreAnim) || (emergencyStop && startAnim)) {

            if (restoreAnim) {
                rotationConstraint.constraintActive = false;
                restoreDefaultRotation();
            }

            if (startAnim || emergencyStop) StartCoroutine(delayRestoreRoutine(animationDuration - (2.0f * deltaTime)));
            else restoreAnim = false;

            startAnim = false;
            stopAnim = true;
        }

        emergencyStop = false;
    }

    public void swordCollided() {
        emergencyStop = true;
    }

    IEnumerator delayRestoreRoutine(float time) {
        yield return new WaitForSeconds(time);
        restoreAnim = true;
    }

    IEnumerator delayConstraintRoutine(float time) {
        yield return new WaitForSeconds(time);
        rotationConstraint.constraintActive = false;
        restoreDefaultRotation();
    }

    public void Attack() {
        rotationConstraint.constraintActive = true;
        startAnim = true;
    }

    private void restoreDefaultRotation() {
        Vector3 tmp = transform.localEulerAngles;
        tmp.z = defaultRotation.z;
        transform.localEulerAngles = tmp;

        tmp = transform.eulerAngles;
        tmp.y = giroCoconutTransform.eulerAngles.y;
        transform.eulerAngles = tmp;
    }

    private void Start() {
        giroCoconutTransform = GameObject.FindGameObjectWithTag("GiroCoconut").transform;
        ConstraintSource tmp = new ConstraintSource();
        tmp.sourceTransform = giroCoconutTransform;
        tmp.weight = 1;

        rotationConstraint.SetSource(0,tmp);
        rotationConstraint.constraintActive = false;

        restoreDefaultRotation();
    }

    private void Update() {
        controlAnim();

        bladeTransform.localScale = new Vector3(1.0f + scales.x * swordScaleCurveWidth.Evaluate(deltaTime),
                                                1.0f + scales.y * swordScaleCurveLenght.Evaluate(deltaTime),
                                                1.0f + scales.z * swordScaleCurveWidth.Evaluate(deltaTime));
    }

}
