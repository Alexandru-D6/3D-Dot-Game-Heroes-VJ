using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationEventsHandler : MonoBehaviour {

    [SerializeField] private PathFollower pathFollower;
    void OnFlamethrowerFinished() {
        pathFollower.OnFlamethrowerFinished();
    }
}
