using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Manager : MonoBehaviour
    {

        private EnemiesRoomManager mymanager;

        public void setMyManager(EnemiesRoomManager manager)
        {
            mymanager = manager;
        }

        public void isDead()
        {
            mymanager.anEnemyHasBeenKilled(this.gameObject);
        }
    }
}