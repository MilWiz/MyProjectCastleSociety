using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using HeneGames.DialogueSystem;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Talks", story: "[Agent] talks to [Player] using [DialogueManager]", category: "Action", id: "92ef2182e85b6508f61c1705c45f5472")]
public partial class TalksAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<DialogueManager> DialogueManager;

    private float coolDownTimer;
    protected override Status OnStart()
    {
        if (DialogueManager != null)
        {
            Debug.Log("StartSuccess");
            return Status.Running;
        }
        Debug.Log("StartFailure");
        return Status.Failure;
    }

    protected override Status OnUpdate()
    {
        /*
        if (coolDownTimer > 0f)
        {
            coolDownTimer -= Time.deltaTime;
        }
        */

        //Trigger event inside DialogueTrigger component
        if (DialogueManager.Value.dialogueTrigger != null)
        {
            DialogueManager.Value.dialogueTrigger.startDialogueEvent.Invoke();
        }

        DialogueManager.Value.startDialogueEvent.Invoke();

        //If component found start dialogue
        DialogueUI.instance.StartDialogue(DialogueManager.Value);

         //Hide interaction UI
        DialogueUI.instance.ShowInteractionUI(false);

        DialogueManager.Value.dialogueIsOn = true;
        //DialogueManager.Value.Senten

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

