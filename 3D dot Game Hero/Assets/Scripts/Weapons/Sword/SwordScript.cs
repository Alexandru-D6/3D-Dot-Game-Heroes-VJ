using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [SerializeField] private AnimationCurve swordScaleCurveLenght;
    [SerializeField] private AnimationCurve swordScaleCurveWidth;
    [SerializeField] private Transform bladeTransform;

    private float startTime;
    private float lastTime;
    [SerializeField] private float deltaTime;

    private bool stopAnim = true;
    [SerializeField] private bool startAnim = false;
    [SerializeField] private bool restoreAnim = false;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float rangeLimiter;

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
                (deltaTime <= 0.0 && restoreAnim)) {
            startAnim = false;
            restoreAnim = false;
            stopAnim = true;
        }
    }

    private void Update() {
        controlAnim();

        bladeTransform.localScale = new Vector3(1.0f + 2.0f * swordScaleCurveWidth.Evaluate(deltaTime),
                                                1.0f + 4.0f * swordScaleCurveLenght.Evaluate(deltaTime),
                                                1.0f + 4.0f * swordScaleCurveWidth.Evaluate(deltaTime));
    }

}
