using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawnInfo;

public class RoomManager : MonoBehaviour {

    #region Parameters

    [SerializeField] List<GridScript> gridsInsideTheRoom;
    [SerializeField] private bool gridsOpen = true;
    [SerializeField] private bool isRoomCleared = false;
    [SerializeField] private bool isPlayerInsideTheRoom = true;
    [SerializeField] private bool isSpawnRoom = false;
    [SerializeField] private EnemiesRoomManager enemiesRoomManager;
    [SerializeField] private VaseManager VaseManager;

    #endregion

    public void UnlockGrids() {
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

        if (isPlayerInsideTheRoom)
        {
            if (enemiesRoomManager != null) enemiesRoomManager.SpawnAllEnemies();   
        }
        else
        {
            if (enemiesRoomManager != null) enemiesRoomManager.DestroyAllEnemies();
        }
    }

    public void InitRoom() {
        gridsOpen = true;
        isRoomCleared = false;
        isPlayerInsideTheRoom = isSpawnRoom;
        if (isPlayerInsideTheRoom)
        {
            if (enemiesRoomManager != null) enemiesRoomManager.SpawnAllEnemies();
            if (VaseManager != null) VaseManager.EnableAllVases();
        }
        UnlockGrids();
    }

    public void Start() {
        InitRoom();
        SceneEvents.onPlayerDeath += OnPlayerDeath;
        if (VaseManager != null) VaseManager.SpawnAllVases();

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

    public void setRoomCleared()
    { isRoomCleared = true; }


}
