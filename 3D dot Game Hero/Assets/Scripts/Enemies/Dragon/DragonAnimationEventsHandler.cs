using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationEventsHandler : MonoBehaviour {

    [SerializeField] private DragonLogic dragonLogic;

    void OnFlamethrowerFinished() {
        dragonLogic.OnFlamethrowerFinished();

    }

    void OnFlamethrowerStarted() {
        dragonLogic.OnFlamethrowerStarted();

    }

    void OnFootKickFinished() {
        dragonLogic.OnFootKickFinished();
    }
}
