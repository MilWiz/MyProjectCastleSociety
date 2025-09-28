using UnityEngine;

public class LampInterract : MonoBehaviour, IInteractable
{
   [SerializeField] private string interactText;
    [SerializeField] private GameObject Light;
    private Light LampsLight;
    private void Awake()
    {
        LampsLight = Light.GetComponent<Light>();
        LampsLight.enabled = false;
    }

    public void ToggleLight()
    {
        LampsLight.enabled = !LampsLight.enabled;
    }

    public string GetInteractText()
    {
        return interactText;
        // return "Open/Close Door ";
    }

    public void Interact(GameObject obj)
    {
        ToggleLight();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

