using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EnemySpawnInfo;

namespace Assets.Scripts.Map
{
    public class Puzzle1Manager : PuzzleManager
    {

        [SerializeField] private EnemySpawnInfo infocolumns;
        List<GameObject> columns = new List<GameObject>();
        [SerializeField] private RedButtonBehavior button;
        //private ButtonBehavior buttonBehavior;
        // Use this for initialization
        void Start()
        {
            foreach (SpawnInfo c in infocolumns.Enemies)
            {
                GameObject col = Instantiate(c.prefab, c.position+transform.position, Quaternion.identity, transform);
                columns.Add(col);
                
            }
        }

        public override void setUpPuzzle()
        {
            
            button.ToIdle();
            for(int i = 0; i < columns.Count; i++)
            {
                columns[i].transform.position = infocolumns.Enemies[i].position + transform.position;
            }
        }

        public override void aButtonHaveBeenPressed()
        {
            solved();
        }
    }
}