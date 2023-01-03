using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    #region Parameters

    [SerializeField] List<GridScript> gridsInsideTheRoom;
    [SerializeField] private bool gridsOpen = true;
    [SerializeField] private bool isRoomCleared = false;
    [SerializeField] private bool isPlayerInsideTheRoom = true;
    [SerializeField] private bool isSpawnRoom = false;

    [SerializeField] private EnemySpawnInfo spawns;

    #endregion

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

    public void ChangePlayerPresence() {
        isPlayerInsideTheRoom = !isPlayerInsideTheRoom;
    }

    public void InitRoom() {
        gridsOpen = true;
        isRoomCleared = false;
        isPlayerInsideTheRoom = isSpawnRoom;

        UnlockGrids();
    }

    public void Start() {
        InitRoom();
        SceneEvents.onPlayerDeath += OnPlayerDeath;

        foreach(var x in spawns.Enemies) {
            Instantiate(x.prefab, x.position, Quaternion.identity, transform);
        }
    }

    void Update() {
        if (!gridsOpen && isRoomCleared && isPlayerInsideTheRoom) {
            gridsOpen = true;
            UnlockGrids();
        }else if (gridsOpen && !isRoomCleared && isPlayerInsideTheRoom) {
            gridsOpen = false;
            LockGrids();
        }
    }

    private void OnDestroy() {
        SceneEvents.onPlayerDeath -= OnPlayerDeath;
    }

    public void OnPlayerDeath() {
        InitRoom();
    }
}
