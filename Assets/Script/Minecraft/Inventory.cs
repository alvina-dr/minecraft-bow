using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemEntry> itemEntryList = new List<ItemEntry>();

    public void AddItem(ItemData _data, int _num)
    {
        ItemEntry _entry = itemEntryList.Find(x => x.data == _data);
        if (_entry != null && _entry.data.maxStack > _entry.number)
        {
            _entry.number += _num;
        } else
        {
            itemEntryList.Add(new ItemEntry(_data, _num));
        }
        GPCtrl.Instance.UICtrl.inventoryBar.UpdateInventory(this);
    }

    public void RemoveItem(ItemData _data, int _num)
    {
        ItemEntry _entry = itemEntryList.Find(x => x.data == _data);
        if (_entry != null)
        {
            _entry.number -= _num;
            if (_entry.number <= 0)
            {
                itemEntryList.Remove(_entry);
            }
        }
        GPCtrl.Instance.UICtrl.inventoryBar.UpdateInventory(this);
    }

    public bool SearchItem(ItemData _data)
    {
        return itemEntryList.Find(x => x.data == _data) != null;
    }

    [System.Serializable]
    public class ItemEntry
    {
        public ItemData data;
        public int number;
        public ItemEntry (ItemData _data, int _number)
        {
            data = _data;
            number = _number;
        }
    }
}
