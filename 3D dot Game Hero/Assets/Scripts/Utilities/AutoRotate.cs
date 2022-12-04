using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] private bool axisX;
    [SerializeField] private bool axisY;
    [SerializeField] private bool axisZ;

    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private float speedZ;

    // Update is called once per frame
    void Update() {
        if (axisX) transform.Rotate(new Vector3(speedX * Time.deltaTime, 0.0f, 0.0f), Space.Self);
        if (axisY) transform.Rotate(new Vector3(0.0f, speedY * Time.deltaTime, 0.0f), Space.Self);
        if (axisZ) transform.Rotate(new Vector3(0.0f, 0.0f, speedZ * Time.deltaTime), Space.Self);
    }
}
