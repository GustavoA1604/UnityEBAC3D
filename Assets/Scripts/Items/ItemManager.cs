using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ItemType
{
    COIN,
    LIFE_PACK
}

[System.Serializable]
public class ItemSetup
{
    public ItemType itemType;
    public SOInt soInt;
}

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;
    public List<ItemSetup> itemSetups;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        itemSetups.ForEach(i => i.soInt.value = 0);
    }

    public static void AddItem(ItemType type, int number = 1)
    {
        _instance.itemSetups.Find(i => i.itemType == type).soInt.value += number;
    }

    public static void RemoveItem(ItemType type, int number = 1)
    {
        _instance.itemSetups.Find(i => i.itemType == type).soInt.value -= number;
    }
}
