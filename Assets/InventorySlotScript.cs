using System;
using TMPro;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour,ISelectHandler
{
    private ItemsInfo Item;
    public UnityEngine.UI.Image img;
    public TMP_Text N, D;
    void Awake()
    {
        Item = null;
    }

    public bool IsEmpty()
    {
        if (Item == null)
        {
            return true;
        }
        return false;
    }

    public void AddItem(ItemsInfo NewItem)
    {
        Item = NewItem;
        img.sprite = NewItem.icon;
    }

    public void ClearSlot()
    {
        Item = null;
        img = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (Item != null)
        {
            N.text = Item.Name.text;
            D.text = Item.Description.text;
        }
        else
        {
            N.text = "";
            D.text = "";
        }
    }


    void Update()
    {
    }
}
