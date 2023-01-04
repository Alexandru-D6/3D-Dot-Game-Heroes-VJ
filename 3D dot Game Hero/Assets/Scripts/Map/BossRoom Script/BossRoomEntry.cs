using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEntry : MonoBehaviour {

    [SerializeField] Camera playerCamera;
    [SerializeField] Vector3 CameraPosition;

    [SerializeField] List<GridScript> gridsInsideTheRoom;
    [SerializeField] private BossRoomManager bossRoomManager;

    IEnumerator delayedGridDoorsRoutine(float time, bool state) {
        yield return new WaitForSeconds(time);

        if (state) UnlockGrids();
        else LockGrids();
    }

    private void UnlockGrids() {
        foreach(var grid in gridsInsideTheRoom) {
            if (grid != null && grid.gameObject.activeInHierarchy) grid.OpenGrid();
        }
    }

    private void LockGrids() {
        foreach(var grid in gridsInsideTheRoom) {
            if (grid != null && grid.gameObject.activeInHierarchy) grid.CloseGrid();
        }
    }

    private void OnTriggerEnter(Collider other) {
        playerCamera.GetComponent<CameraSmoothMovement>().MoveTo(CameraPosition);
        StartCoroutine(delayedGridDoorsRoutine(1.0f, false));
        bossRoomManager.PlayerEntered();
    }

    private void Start() {
        playerCamera = Camera.main;
        UnlockGrids();
    }

}
