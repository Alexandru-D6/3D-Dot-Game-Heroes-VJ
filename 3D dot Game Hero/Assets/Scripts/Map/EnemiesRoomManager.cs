using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnemySpawnInfo;

public class EnemiesRoomManager : MonoBehaviour
{

    [SerializeField] private EnemySpawnInfo spawns;
    [SerializeField] private GameObject spawnParticles;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private int enemiesaLive;
    [SerializeField] private List<GameObject> enemies;
    private bool active;

    private void Start()
    {
        active= false;
        enemiesaLive = 0;
    }

    private void Update()
    {
        
    }

    public void SpawnAllEnemies()
    {
        active= true;
        enemiesaLive = 0;
        if(spawns != null) foreach (var x in spawns.Enemies)
        {
            Instantiate(spawnParticles, x.position, Quaternion.identity, transform);
            StartCoroutine(SpawnAnEnemy(x));
            enemiesaLive++;
        }

        if(enemiesaLive <= 0)
        {
            roomManager.setRoomCleared();
        }
    }

    public bool HasEnemies() {
        return enemiesaLive > 0;
    }

    IEnumerator SpawnAnEnemy(SpawnInfo enemy)
    {
        yield return new WaitForSeconds(1f);
        GameObject enem = Instantiate(enemy.prefab, enemy.position, Quaternion.identity, transform);
        enem.GetComponent<Manager>().setMyManager(this);
        enemies.Add(enem);

    }

    public void anEnemyHasBeenKilled(GameObject enemy)
    {
        enemiesaLive--;
        enemies.Remove(enemy);
        if(enemiesaLive <= 0)
        {
            active= false;
            roomManager.setRoomCleared();
        }
    }

    public void DestroyAllEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            if (enemy != null) {
                if(enemy.tag == Tags.Zombie.ToString())
                {
                    enemy.GetComponent<ZombieHealth>().readyToDestroy();
                }
                else if (enemy.tag == Tags.Creeper.ToString())
                {
                    enemy.GetComponent<CreeperHealth>().readyToDestroy();
                }
                else if (enemy.tag == Tags.Enderman.ToString())
                {
                    enemy.GetComponent<EndermanHealth>().readyToDestroy();
                }
                else if (enemy.tag == Tags.Skeleton.ToString())
                {
                    enemy.GetComponent<SkeletonHealth>().readyToDestroy();
                }
            }
        }

        enemiesaLive = 0;
        roomManager.setRoomCleared();
    }
}
