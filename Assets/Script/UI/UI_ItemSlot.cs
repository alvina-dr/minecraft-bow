using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_ItemSlot : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI number;

    public void UpdateSlot(Inventory.ItemEntry _entry)
    {
        if (_entry == null)
        {
            image.gameObject.SetActive(false);
            number.text = "";
        } else
        {
            image.gameObject.SetActive(true);
            image.sprite = _entry.data.sprite;
            if (_entry.number == 1)
                number.text = "";
            else 
                number.text = _entry.number.ToString();
        }
    }

}
