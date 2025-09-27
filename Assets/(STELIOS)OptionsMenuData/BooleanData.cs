using UnityEngine;

[CreateAssetMenu(fileName = "BooleanData", menuName = "Scriptable Objects/BooleanData")]
public class BooleanData : ScriptableObject
{
    [SerializeField]
    private bool boolvalue;

    public bool BooleanValue
    {
        get { return boolvalue; }
        set { boolvalue = value; }
    }
}
