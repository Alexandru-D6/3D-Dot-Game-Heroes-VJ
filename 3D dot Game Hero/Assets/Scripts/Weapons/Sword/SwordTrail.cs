using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrail : MonoBehaviour {

    #region Parameters

    [Header("Trails list (they should be order from the fardest to the closest)")]
    [SerializeField] private List<TrailRenderer> trailRendereres;

    [Header("Trails gradient")]
    [SerializeField] private Gradient originalTrail;
    [SerializeField] private Gradient whiteTrail;

    [Header("Blades GameObjects")]
    [SerializeField] private GameObject originalBlade;
    [SerializeField] private GameObject whiteBlade;

    [Header("Trails options")]
    [Range(0.0f, 0.25f)]
    [SerializeField] private float upperTime;
    [Range(0.0f, 0.25f)]
    [SerializeField] private float lowerTime;
    [SerializeField] private AnimationCurve trailWidth;

    #endregion

    #region MonoBehaviour Methods

    private void Update() {
        float steeps = ((upperTime - lowerTime) / (trailRendereres.Count));

        for (int i = 0; i < trailRendereres.Count; ++i) {
            trailRendereres[i].alignment = LineAlignment.TransformZ;
            trailRendereres[i].colorGradient = (originalBlade.activeInHierarchy) ? originalTrail : whiteTrail;
            trailRendereres[i].time = upperTime - (steeps * i);
            trailRendereres[i].widthCurve = trailWidth;
        }
    }

    #endregion

}
