using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>,ISaveable
{
    public ItemDataList_SO itemData;

    [SerializeField]
    private List<ItemName> itemList = new List<ItemName>();

    void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStartNewGameEvent(int obj)
    {
        itemList.Clear();
    }

    private void OnAfterSceneUnloadEvent()
    {
        if (itemList.Count == 0)
        {
            EventHandler.CallUpdateUIEvent(null, -1);
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }
    }

    private void OnChangeItemEvent(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);
        }
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemInex(itemName);
        itemList.RemoveAt(index);

        if (itemList.Count == 0)
        {
            EventHandler.CallUpdateUIEvent(null, -1);
        }
    }

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //对应UI显示
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
        }
    }

    private int GetItemInex(ItemName itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
            {
                return i;
            }
        }

        return -1;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemList=this.itemList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        saveData.itemList=this.itemList;
    }
}
