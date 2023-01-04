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

        if (isPlayerInsideTheRoom)
        {
            //if (enemiesRoomManager != null) enemiesRoomManager.SpawnAllEnemies();
            
        }
        else
        {
            isRoomCleared= false;
            if (enemiesRoomManager != null) enemiesRoomManager.DestroyAllEnemies();
            if (hasAPuzzle)
            {
                puzzleManager.setUpPuzzle();
            }
        }
    }

    public void InitRoom() {
        gridsOpen = true;
        isRoomCleared = false;
        isPlayerInsideTheRoom = isSpawnRoom;
        isSolved= false;
        if (isPlayerInsideTheRoom)
        {
            //if (enemiesRoomManager != null) enemiesRoomManager.SpawnAllEnemies();
            if (VaseManager != null) VaseManager.EnableAllVases();
            if(hasAPuzzle)
            {
                puzzleManager.setUpPuzzle();
            }
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
        }else if (gridsOpen && !isRoomCleared && isPlayerInsideTheRoom && !hasAPuzzle) {
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

    public void roomSolved()
    {
        if (!isSolved) {
            isSolved= true;
            if (chest !=null){ }
            isRoomCleared= true;
        }
    }


}
