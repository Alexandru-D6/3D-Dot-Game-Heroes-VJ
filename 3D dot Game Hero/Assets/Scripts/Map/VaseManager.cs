using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawnInfo;

public class VaseManager : MonoBehaviour
{
    [SerializeField] private VaseSpawnInfo spawns;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private List<GameObject> vases;
    [SerializeField] private GameObject prefabVase;
    [SerializeField] private GameObject VasesContainer;
    
    public void SpawnAllVases()
    {
        if (spawns != null) foreach (var x in spawns.Vases)
            {
                GameObject vase = Instantiate(prefabVase, x.position+ transform.position, Quaternion.identity, VasesContainer.transform);
                vases.Add(vase);
            }
    }


    public void EnableAllVases()
    {
        if (spawns != null) foreach (var vase in vases)
            {
                vase.SetActive(true);
            }
    }

    public void DisableVases()
    {
        if (spawns != null) foreach (GameObject vase in vases)
        {
            vase.SetActive(false);
        }
    }
}


