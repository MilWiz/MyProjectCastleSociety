using UnityEngine;

[CreateAssetMenu(fileName = "IntegerData", menuName = "Scriptable Objects/IntegerData")]
public class IntegerData : ScriptableObject
{
    [SerializeField]
    private int intvalue;

    public int IntgerValue
    {
        get { return intvalue; }
        set { intvalue = value; }
    }
}
