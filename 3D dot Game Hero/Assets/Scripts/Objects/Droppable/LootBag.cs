using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{

    public List<LootObj> lootList = new List<LootObj>();
    // Start is called before the first frame update
    LootObj GetDroopedItem()
    {
        int randomNumber = Random.Range(0, 101);
        List<LootObj> possibleItems = new List<LootObj>();
        foreach(LootObj item in lootList)
        {
            if(randomNumber <= item.dropChance) { possibleItems.Add(item); }
        }
        if(possibleItems.Count > 0)
        {
            LootObj obj = possibleItems[Random.Range(0,possibleItems.Count)];
            return obj;
        }
        return null;
    }

    public void InstantiateLoot(Vector3 pos)
    {
        LootObj obj = GetDroopedItem();
        if(obj != null)
        {
            Instantiate(obj.prefav, pos, Quaternion.identity, SceneObjectsManager.Instance.transform);
        }
    }
}
