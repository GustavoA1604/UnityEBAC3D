using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;
    private List<GameObject> _items = new List<GameObject>();
    [Header("Animation")]
    public float tweenStartDuration = 1f;
    public float tweenEndDuration = 1f;
    public Ease tweenEase = Ease.OutBack;
    public Vector2 randomRange = new Vector2(-1f, 1f);

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    private void CreateItems()
    {
        for (int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            item.transform.DOScale(0, tweenStartDuration).SetEase(tweenEase).From();
            _items.Add(item);
        }
    }

    public override void Collect()
    {
        base.Collect();
        foreach (var i in _items)
        {
            i.transform.DOMoveY(2f, tweenEndDuration).SetRelative();
            i.transform.DOScale(0, tweenEndDuration / 2).SetDelay(tweenEndDuration / 2);
            ItemManager.AddItem(ItemType.COIN);
        }
    }
}
