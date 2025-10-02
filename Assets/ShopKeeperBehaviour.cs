using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopKeeperBehaviour : MonoBehaviour
{

    public ItemsInfo[] MyItems;
    public int[] Prices;
    private Button[] ItemButton;
    public GameObject SpaceForButtons;
    public Button ButtonPrefab;

    public GameObject[] InventorySlots;

    public GameObject AreYouSure;
    public Button Yes, No;

    private ItemsInfo SelectedItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemButton = new Button[MyItems.Length];
        if (Prices.Length == MyItems.Length)
        {
            for (int i = 0; i < MyItems.Length; i++)
            {
                ItemButton[i] = Instantiate(ButtonPrefab, SpaceForButtons.transform) as Button;
                ItemButton[i].gameObject.SetActive(true);
                ItemButton[i].onClick.AddListener(ItemButtonListener);
                ItemButton[i].transform.SetParent(SpaceForButtons.transform, false);
                Variables.Object(ItemButton[i]).Set("Item", MyItems[i]);
                TMP_Text temp;
                temp = ItemButton[i].GetComponentInChildren<TMP_Text>();
                if (temp != null)
                {
                    temp.text = MyItems[i].name + " - " + Prices[i] + " coins.";
                }
                else
                {
                    Debug.Log("PROBLEM 2");
                }
                Yes.onClick.AddListener(YesButtonListner);
                No.onClick.AddListener(NoButtonListner);
            }
        }
        else
        {
            Debug.Log("Problem 1");
        }
    }

    private void ItemButtonListener()
    {
        AreYouSure.SetActive(true);
        SelectedItem = Variables.Object(EventSystem.current.currentSelectedGameObject.gameObject).Get("Item") as ItemsInfo;

    }

    private void YesButtonListner()
    {
        AreYouSure.SetActive(false);
        int temp = System.Array.IndexOf(MyItems, SelectedItem);
        if (temp != -1)
        {
            //MyItems = RemoveElem<ItemsInfo>(MyItems, temp);
            //Prices = RemoveElem<int>(Prices, temp);
            for (int i = 0; i < InventorySlots.Length; i++)
            {
                InventorySlotScript a = InventorySlots[i].GetComponent<InventorySlotScript>();
                if (a != null)
                {
                    if (a.IsEmpty())
                    {
                        a.AddItem(SelectedItem);
                        break;
                    }
                }
            }
            RemoveAt<ItemsInfo>(ref MyItems, temp);
            RemoveAt<int>(ref Prices, temp);
            Destroy(ItemButton[temp].gameObject);
            RemoveAt<Button>(ref ItemButton, temp);
            SelectedItem = null;
        }
    }
    private void NoButtonListner()
    {
        AreYouSure.SetActive(false);
        SelectedItem = null;
    }
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        if (index < 0 || index >= arr.Length || arr == null)
        {

        }
        else{
            for (int a = index; a < arr.Length - 1; a++)
            {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            // finally, let's decrement Array's size by one
            System.Array.Resize(ref arr, arr.Length - 1);
        }
    }

/*
            public T[] RemoveElem<T>(T[] arr, int index)
            {
                if (arr == null)
                    throw new ArgumentNullException(nameof(arr), "Input array cannot be null.");

                if (index < 0 || index >= arr.Length)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

                T[] temp = new T[arr.Length - 1];
                Debug.Log("Temp.length = " + temp.Length);
                for (int i = 0, j = 0; i < arr.Length; i++)
                {
                    if (i != index)
                    {
                        Debug.Log("i = " + i);
                        Debug.Log("j = " + j);
                        Debug.Log("arr[i] = " + arr[i]);
                        temp[j] = arr[i];
                        j++;
                    }
                }

                return temp;
            }
            */

        // Update is called once per frame
        void Update()
    {
        
    }
}
