using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

public class ShuttersInteract : MonoBehaviour, IInteractable
{
   [SerializeField] private string interactText;
    private Animator animator;
    private bool isOpen;
    private float Chance;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
        Chance = 0f;
        animator.SetBool("isOpen", isOpen);
        animator.SetFloat("Chance", Chance);
    }

    public void ToggleShatters()
    {
        System.Random rand = new System.Random();
        isOpen = !isOpen;
        Chance = 1f;
        animator.SetBool("isOpen", isOpen);
        animator.SetFloat("Chance",Chance);
        Debug.Log("Chance: " + Chance);
        Debug.Log("isOpen:" + isOpen);
    }

    public string GetInteractText()
    {
        return interactText;
        // return "Open/Close Door ";
    }

    public void Interact(GameObject obj)
    {
        ToggleShatters();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

