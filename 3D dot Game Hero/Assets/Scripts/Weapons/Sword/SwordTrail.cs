using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrail : MonoBehaviour {
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform scalerObject;

    private void Update() {
        trailRenderer.widthMultiplier = scalerObject.localScale.y;
        trailRenderer.startWidth = 1.0f;
        trailRenderer.endWidth = 1.0f;
    }
}
