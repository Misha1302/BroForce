using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryGameObject;
    [SerializeField] private Vector2 itemsCount;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private Transform defaultItem;
    [SerializeField] private Vector2 itemsOffset;
    [SerializeField] private Vector2 topLeftPoint;

    private readonly List<List<Transform>> _itemsList = new();
    private bool showInventory;

    private void Start()
    {
        SetDefaultItemsToItemsList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) showInventory = !showInventory;

        inventoryGameObject.SetActive(showInventory);
    }

    private void SetDefaultItemsToItemsList()
    {
        var currentPosition = topLeftPoint;
        for (var x = 0; x < itemsCount.y; x++)
        {
            _itemsList.Add(new List<Transform>());
            _haveItemList.Add(new List<bool>());
            for (var y = 0; y < itemsCount.x; y++)
            {
                var newItem = Instantiate(defaultItem, itemsParent);

                newItem.localPosition = currentPosition;

                _itemsList[x].Add(newItem);
                _haveItemList[x].Add(false);
                currentPosition.x += itemsOffset.x;
            }
            currentPosition.y -= itemsOffset.y;
            currentPosition.x = topLeftPoint.x;
        }
    }

    private List<List<bool>> _haveItemList = new();
    public Vector2Int? GetFreePosition()
    {
        for (var i = 0; i < _haveItemList.Count; i++)
            for (var i1 = 0; i1 < _haveItemList.Count; i1++)
                if (!_haveItemList[i][i1]) 
                    return new Vector2Int(i1, i);

        return null;
    }

    public void SetItem(Transform item, int x, int y)
    {
        item.SetParent(itemsParent);
        item.localScale = new Vector3(1, 1, 1);
        var oldItem = _itemsList[y][x];
        item.position = oldItem.position;

        Destroy(oldItem.gameObject);

        _itemsList[y][x] = item;

        _haveItemList[y][x] = true;
    }
}
