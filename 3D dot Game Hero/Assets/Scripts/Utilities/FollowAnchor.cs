using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnchor : MonoBehaviour {

    #region Parameters

    [SerializeField] private string externalAnchorName;
    [SerializeField] private Transform externalAnchor;
    [SerializeField] private Transform internalAnchor;

    #endregion

    #region MonoBehaviour Methods

    private void Start() {
        if(transform.parent != null) externalAnchor = transform.parent.transform.Find(externalAnchorName).transform;
    }

    void Update() {
        if (internalAnchor != null && externalAnchor != null) transform.Translate(externalAnchor.position - internalAnchor.position, Space.World);
    }

    #endregion

}
