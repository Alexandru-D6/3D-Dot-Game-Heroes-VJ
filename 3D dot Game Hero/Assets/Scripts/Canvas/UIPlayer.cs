using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{

    [SerializeField] List<GameObject> hearts = new List<GameObject>();
    [SerializeField] Sprite RedHeart;
    [SerializeField] Sprite EmptyHeart;
    [SerializeField] Sprite GoldenHeart;
    [SerializeField] List<GameObject> inventory = new List<GameObject>();
    [SerializeField] Sprite SlotInvEmpty;
    [SerializeField] Sprite SlotInvSword;
    [SerializeField] Sprite SlotInvBoomerang;
    [SerializeField] Sprite SlotInvBow;
    [SerializeField] Sprite SlotInvBoomb;
    [SerializeField] Sprite SlotInvCoin;
    [SerializeField] Sprite SlotInvSkullKey;
    [SerializeField] Sprite SlotInvEnderKey;
    [SerializeField] Sprite SlotInvBossKey;
    [SerializeField] GameObject TextNumCoins;
    [SerializeField] GameObject TextNumBombs;

    private int numCoins;
    private int numBombs;
    private int damage;
    private bool isGodMode;
    public static UIPlayer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            Instance = this;
        }
        else Instance = this;
    }

    
    private void Start()
    {
        numCoins = 0;
        isGodMode= false;
        numBombs = 0;
        fillHearts();
        inventory[0].GetComponent<Image>().sprite = SlotInvSword;
        TextNumCoins.SetActive(false);
        TextNumBombs.SetActive(false);
    }

    public void fillHearts()
    {
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<Image>().sprite = RedHeart;
        }
    }
    
    public void UnlockedWeapon(Tags name)
    {
        switch (name)
        {
            case Tags.Boomerang:
                inventory[1].GetComponent<Image>().sprite = SlotInvBoomerang;
                break;
            case Tags.Bow:
                inventory[2].GetComponent<Image>().sprite = SlotInvBow;
                break;
            case Tags.Bomb:
                inventory[3].GetComponent<Image>().sprite = SlotInvBoomb;
                numBombs = 10;
                TextNumBombs.SetActive(true);
                TextNumBombs.GetComponent<TextMeshProUGUI>().SetText(numBombs.ToString());
                break;
        }
    }

    public void addCoin()
    {
        
        if (numCoins == 0)
        {
            inventory[4].GetComponent<Image>().sprite = SlotInvCoin;
            TextNumCoins.SetActive(true);

        }
        numCoins++;
        TextNumCoins.GetComponent<TextMeshProUGUI>().SetText(numCoins.ToString());
    }

    public void UpdateInventoy(Tags name, bool obtained)
    {
        switch (name)
        {
            case Tags.Sword:
                if (obtained) inventory[0].GetComponent<Image>().sprite = SlotInvSword;
                else inventory[0].GetComponent<Image>().sprite = SlotInvEmpty;
                break;
            case Tags.Boomerang:
                if (obtained) inventory[1].GetComponent<Image>().sprite = SlotInvBoomerang;
                else inventory[1].GetComponent<Image>().sprite = SlotInvEmpty;
                break;
            case Tags.Bow:
                if (obtained) inventory[2].GetComponent<Image>().sprite = SlotInvBow;
                else inventory[2].GetComponent<Image>().sprite = SlotInvEmpty;
                break; 
            case Tags.Bomb:
                if (obtained)
                {
                    inventory[3].GetComponent<Image>().sprite = SlotInvBoomb;
                    TextNumBombs.SetActive(true);
                    numBombs = 10;
                    TextNumBombs.GetComponent<TextMeshProUGUI>().SetText(numBombs.ToString());
                }
                else
                {
                    numBombs = 0;
                    TextNumBombs.GetComponent<TextMeshProUGUI>().SetText(numBombs.ToString());
                    TextNumBombs.SetActive(false);

                    inventory[3].GetComponent<Image>().sprite = SlotInvEmpty;
                }
                break;
            case Tags.Coin:
                if (obtained)
                {
                    inventory[4].GetComponent<Image>().sprite = SlotInvCoin;
                    numCoins= 1;
                    TextNumCoins.SetActive(true);
                    TextNumCoins.GetComponent<TextMeshProUGUI>().SetText(numCoins.ToString());
                }
                else
                {
                    numCoins = 0;
                    TextNumCoins.GetComponent<TextMeshProUGUI>().SetText(numCoins.ToString());
                    TextNumCoins.SetActive(false);
                    inventory[4].GetComponent<Image>().sprite = SlotInvEmpty;
                }
                break;
            case Tags.SkullKey:
                if (obtained) inventory[5].GetComponent<Image>().sprite = SlotInvSkullKey;
                else inventory[5].GetComponent<Image>().sprite = SlotInvEmpty;
                break;
            case Tags.EnderKey:
                if (obtained) inventory[6].GetComponent<Image>().sprite = SlotInvEnderKey;
                else inventory[6].GetComponent<Image>().sprite = SlotInvEmpty;
                break;
            case Tags.BossKey:
                if (obtained) inventory[7].GetComponent<Image>().sprite = SlotInvBossKey;
                else inventory[7].GetComponent<Image>().sprite = SlotInvEmpty;
                break;
        }
    }

    public void UsedaBomb()
    {
        numBombs--;
        TextNumBombs.GetComponent<TextMeshProUGUI>().SetText(numBombs.ToString());
        if(numBombs == 0)
        {
            inventory[3].GetComponent<Image>().sprite = SlotInvEmpty;
            TextNumBombs.SetActive(false);
        }
    }

    public void playerDamaged()
    {
        
        hearts[damage].GetComponent<Image>().sprite = EmptyHeart;
        damage++;
    }

    public void playerHealed()
    {
        damage--;
        if(isGodMode)hearts[damage].GetComponent<Image>().sprite = GoldenHeart;
        else hearts[damage].GetComponent<Image>().sprite = RedHeart;
    }
    public void isPlayerGodMode(bool isOnGodMode) {
        if (isOnGodMode)
        {
            isGodMode = true;
            for (int i = damage; i < hearts.Count; i++)
            {
                hearts[i].GetComponent<Image>().sprite = GoldenHeart;
            }
        }
        else
        {
            isGodMode = false;  
            for(int i = damage; i< hearts.Count;i++ )
            {
                hearts[i].GetComponent<Image>().sprite = RedHeart;
            }
        }
    }


}
