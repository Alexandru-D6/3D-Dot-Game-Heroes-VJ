using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnchor : MonoBehaviour {
    [SerializeField] private string externalAnchorName;
    [SerializeField] private Transform externalAnchor;
    [SerializeField] private Transform internalAnchor;

    private void Start() {
        externalAnchor = transform.parent.transform.Find(externalAnchorName).transform;
    }

    void Update() {
        transform.Translate(externalAnchor.position - internalAnchor.position, Space.World);
    }
}
