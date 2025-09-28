using UnityEngine;

public class DoorInteract : MonoBehaviour , IInteractable
{
    [SerializeField] private string interactText;
    private Animator animator;
    private bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
        animator.SetBool("isOpen", isOpen);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
        Debug.Log("isOpen:" + isOpen);
    }

    public string GetInteractText()
    {
        return interactText;
        // return "Open/Close Door ";
    }

    public void Interact(GameObject obj)
    {
        ToggleDoor();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
