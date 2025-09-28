using UnityEngine;

public class NPCInteractable : MonoBehaviour , IInteractable 
{
    [SerializeField] private string interactText;

    public void Interact(GameObject obj)
    {

    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

}
