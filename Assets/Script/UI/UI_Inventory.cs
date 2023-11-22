using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private List<UI_ItemSlot> itemSlotList = new List<UI_ItemSlot>();

    public void UpdateInventory(Inventory _inventory)
    {
        for (int i = 0; i < itemSlotList.Count; i++)
        {
            if (i >= _inventory.itemEntryList.Count)
                itemSlotList[i].UpdateSlot(null);
            else 
                itemSlotList[i].UpdateSlot(_inventory.itemEntryList[i]);
        }
    }
}
