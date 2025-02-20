using Assets.Scripts.Map;
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
    [SerializeField] private bool hasAPuzzle;
    [SerializeField] private bool isSolved;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private GameObject chest;

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

        if (isPlayerInsideTheRoom) {
            if (enemiesRoomManager != null) {
                enemiesRoomManager.SpawnAllEnemies();
                isRoomCleared = false;
            }else {
                isRoomCleared = true;
            }
        } else {
            if (enemiesRoomManager != null) {
                enemiesRoomManager.DestroyAllEnemies();
            }

            SceneObjectsManager.Instance.DestroyObjects();

            if (hasAPuzzle) puzzleManager.setUpPuzzle();
        }
    }

    public void InitRoom(bool value) {
        gridsOpen = true;
        isRoomCleared = isSpawnRoom;
        isPlayerInsideTheRoom = isSpawnRoom;
        isSolved= false;

        if (enemiesRoomManager != null && !value) enemiesRoomManager.DestroyAllEnemies();
        if (VaseManager != null) VaseManager.EnableAllVases();
        if (hasAPuzzle) puzzleManager.setUpPuzzle();

        UnlockGrids();
    }

    public bool IsPlayerInsideTheRoom() {
        return isPlayerInsideTheRoom;
    }

    public void Start() {
        InitRoom(true);
        if (VaseManager != null) VaseManager.SpawnAllVases();
        SceneEvents.onPlayerDeath += OnPlayerDeath;
    }

    void Update() {
        if (!gridsOpen && isRoomCleared && isPlayerInsideTheRoom) {
            gridsOpen = true;
            UnlockGrids();
        }else if (gridsOpen && !isRoomCleared && isPlayerInsideTheRoom && !hasAPuzzle) {
            gridsOpen = false;
            LockGrids();
        }
    }

    private void OnDestroy() {
        SceneEvents.onPlayerDeath -= OnPlayerDeath;
    }

    public void OnPlayerDeath() {
        InitRoom(false);
    }

    public void setRoomCleared() { 
        isRoomCleared = true;
        if (chest != null && !hasAPuzzle && !isSolved) {
            chest.GetComponent<ChestScript>().RestoreChest(true);
            isSolved = true;
        }
    }

    public void roomSolved() {
        if (!isSolved) {
            isSolved= true;
            if (chest != null) chest.GetComponent<ChestScript>().RestoreChest(true); 
            isRoomCleared= true;
        }
    }

}
