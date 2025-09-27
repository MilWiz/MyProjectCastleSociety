using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemsInfo", menuName = "Scriptable Objects/ItemsInfo")]
public class ItemsInfo : ScriptableObject
{
    public int id;
    public TMP_Text Name, Description;
    //public GameObject item;
    public Sprite icon;
}
