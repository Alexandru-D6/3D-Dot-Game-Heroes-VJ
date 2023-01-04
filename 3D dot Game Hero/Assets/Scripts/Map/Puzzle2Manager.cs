using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnemySpawnInfo;

namespace Assets.Scripts.Map
{
    public class Puzzle2Manager : PuzzleManager
    {

        [SerializeField] List<RedButtonBehavior> buttons = new List<RedButtonBehavior>();
        private int numButtonsActive;
        [SerializeField] private float limittime;
        [SerializeField] private float time;
        // Use this for initialization
        void Start()
        {
           numButtonsActive= 0;
            time = 0;
        }

        private void Update()
        {
            if (numButtonsActive != 0) time += 1 * Time.deltaTime;
            if (limittime <= time)
            {
                numButtonsActive = 0;
                time = 0;
                foreach(RedButtonBehavior b in buttons)
                {
                    b.ToIdle();
                }
            }
        }

        public override void setUpPuzzle()
        {
            time= 0;
            numButtonsActive = 0;
            foreach (RedButtonBehavior b in buttons)
            {
                b.ToIdle();
            }
        }

        public override void aButtonHaveBeenPressed()
        {
            numButtonsActive++;
            time = 0;
            if(numButtonsActive == buttons.Count)
            {
                solved();
            }
        }
    }
}