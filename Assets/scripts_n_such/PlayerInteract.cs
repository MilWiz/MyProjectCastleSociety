using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

    //public FirstPersonController PlayerCamera;
    public DSPlayerMovement PlayerCamera;

    public LayerMask layerMask;
    private RaycastHit hit;
    private float interactRange = 2f;
    private IInteractable interactable;
    private void Update()
    {
        interactable = GetInteractableObject();
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, interactRange))
        {
            if (interactable != null)
            {
                Show(interactable);
            }
        }
        else
        {
            Hide();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactable != null)
            {
                interactable.Interact(hit.transform.gameObject);
            }
        }
    }

    private void Show(IInteractable interactable)
    {
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = interactable.GetInteractText();
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }


    public IInteractable GetInteractableObject()
    {
        IInteractable Interactable;
        float interactRange = 2f;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out RaycastHit hit, interactRange))
        {
            Collider[] colliderArray = Physics.OverlapSphere(hit.collider.gameObject.transform.position, interactRange);
            Interactable = null;
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    Interactable = interactable;
                }

            }
            return Interactable;
        }
        else
        {
            return null;
        }

    }
}

/*

            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                interactable.Interact(transform);
            }
// Old one just in case we need it again
public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        float interactRange = 4f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);

            }

        }
        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) <
                    Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                { // get the one closest 
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }
}
*/

