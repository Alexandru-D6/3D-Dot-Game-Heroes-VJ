using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class FlashingAlphaScript : MonoBehaviour {

    [SerializeField] private Material matBlack;
    [SerializeField] private Material matTrans;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float timeRemaining = 0.5f;
    [SerializeField] private float amplitude = 0.2f;
    [SerializeField] private float steeps = 0.05f;

    public void SetFlashing(float time, float amp, float steep) {
        timeRemaining = time;
        amplitude = amp;
        steeps = steep;
    }

    // Update is called once per frame
    void Update() {
        if (timeRemaining >= 0) {
            _renderer.materials[1].CopyPropertiesFromMaterial(matTrans);
            float tmp = (timeRemaining % steeps) / steeps;
            Color col = matTrans.color;
            col.a = 1.0f - amplitude * tmp;
            matTrans.color = col;

            timeRemaining -= Time.deltaTime;
        }else {
            _renderer.materials[1].CopyPropertiesFromMaterial(matBlack);
        }
    }
}
