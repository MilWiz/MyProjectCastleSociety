using UnityEngine;

[CreateAssetMenu(fileName = "FloatData", menuName = "Scriptable Objects/FloatData")]
public class FloatData : ScriptableObject
{
    [SerializeField]
    private float floatvalue;

    public float FloatValue
    {
        get { return floatvalue; }
        set { floatvalue = value; }
    }
}
