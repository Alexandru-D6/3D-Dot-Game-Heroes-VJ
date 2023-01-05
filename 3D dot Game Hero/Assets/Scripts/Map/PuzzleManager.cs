using System.Collections;
using System.Collections.Generic;
using static EnemySpawnInfo;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public abstract class PuzzleManager : MonoBehaviour
    {
        [SerializeField] RoomManager roomManager;        
        
        // Use this for initialization
        public abstract void setUpPuzzle();
        public abstract void aButtonHaveBeenPressed();
        public void solved()
        {
            roomManager.roomSolved();
        }
        
    }
}