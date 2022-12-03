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
    [SerializeField] private float delayRestore;

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
                (deltaTime <= 0.0 && restoreAnim) || emergencyStop) {

            if (restoreAnim) {
                rotationConstraint.enabled = false;
                Vector3 tmp = defaultRotation;
                tmp.y = giroCoconutTransform.eulerAngles.y;
                transform.eulerAngles = tmp;
            }

            if (startAnim || emergencyStop) StartCoroutine(delayRestoreRoutine(delayRestore));
            else restoreAnim = false;

            startAnim = false;
            stopAnim = true;
            emergencyStop = false;
        }
    }

    public void swordCollided() {
        emergencyStop = true;
    }

    IEnumerator delayRestoreRoutine(float time) {
        yield return new WaitForSeconds(time);
        restoreAnim = true;
    }

    public void Attack() {
        rotationConstraint.enabled = true;
        startAnim = true;
    }

    private void Start() {
        giroCoconutTransform = GameObject.FindGameObjectWithTag("GiroCoconut").transform;
        ConstraintSource tmp = new ConstraintSource();
        tmp.sourceTransform = giroCoconutTransform;
        tmp.weight = 1;

        rotationConstraint.SetSource(0,tmp);
        rotationConstraint.enabled = false;
    }

    private void Update() {
        Vector3 tmp2 = defaultRotation;
        tmp2.y = giroCoconutTransform.eulerAngles.y;
        transform.eulerAngles = tmp2;

        controlAnim();

        bladeTransform.localScale = new Vector3(1.0f + scales.x * swordScaleCurveWidth.Evaluate(deltaTime),
                                                1.0f + scales.y * swordScaleCurveLenght.Evaluate(deltaTime),
                                                1.0f + scales.z * swordScaleCurveWidth.Evaluate(deltaTime));
    }

}
