using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplicateRotation : MonoBehaviour {

    #region Parameters

    [SerializeField] private Transform original;
    [SerializeField] private List<bool> lockedRotations;

    #endregion

    #region MonoBehaviour Methods

    void Update() {
        Vector3 tmp = transform.eulerAngles;

        for (int i = 0; i < lockedRotations.Count; i++) {
            if (lockedRotations[i]) tmp[i] = original.eulerAngles[i];
        }

        transform.eulerAngles = tmp;
    }

    #endregion

}
