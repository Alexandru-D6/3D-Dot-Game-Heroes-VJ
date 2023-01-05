using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EnemySpawnInfo;

namespace Assets.Scripts.Map
{
    public class Puzzle3Manager : PuzzleManager
    {

        [SerializeField] private EnemySpawnInfo infocolumns;
        List<GameObject> columns = new List<GameObject>();
        [SerializeField] List<GameObject> detectors = new List<GameObject>();
        public int numcolumnsOnPos;
        //private ButtonBehavior buttonBehavior;
        // Use this for initialization
        void Start()
        {
            numcolumnsOnPos = 0;
            foreach (SpawnInfo c in infocolumns.Enemies)
            {
                GameObject col = Instantiate(c.prefab, c.position, Quaternion.identity, transform);
                columns.Add(col);
                
            }
        }

        public override void setUpPuzzle()
        {
            
            numcolumnsOnPos= 0;
            for(int i = 0; i < columns.Count; i++)
            {
                columns[i].transform.position = infocolumns.Enemies[i].position;
                columns[i].GetComponent<ObsMangEnd>().isBlocked= false;
            }

            foreach(var x in detectors) {
                x.GetComponent<Detector>().SetDetectedToFalse();
            }
        }

        public override void aButtonHaveBeenPressed()
        {
            numcolumnsOnPos++;
            if(numcolumnsOnPos == detectors.Count) solved();
        }
    }
}