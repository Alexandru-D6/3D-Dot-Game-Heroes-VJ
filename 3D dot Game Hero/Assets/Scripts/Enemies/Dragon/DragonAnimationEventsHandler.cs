using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationEventsHandler : MonoBehaviour {

    [SerializeField] private DragonLogic dragonLogic;
    private bool disableFlamethrower = false;

    public void DisableFlamethrower(bool value) {
        disableFlamethrower = value;
    }

    void OnFlamethrowerFinished() {
        if (!disableFlamethrower) {
            dragonLogic.OnFlamethrowerFinished();
            SoundManager.Instance.StopDragonFlamethrower();
        }
    }

    void OnFlamethrowerStarted() {
        if (!disableFlamethrower) {
            dragonLogic.OnFlamethrowerStarted();
            SoundManager.Instance.PlayDragonFlamethrower();
        }
    }

    void OnFootKickFinished() {
        dragonLogic.OnFootKickFinished();
    }
}
