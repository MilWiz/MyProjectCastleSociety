using System;
using Unity.Behavior;
using UnityEngine;
using HeneGames.DialogueSystem;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckIfPlayerIsTalking", story: "[Player] is [Talking]", category: "Conditions", id: "f24ea2500d31af588e36a8af35f7d804")]
public partial class CheckIfPlayerIsTalkingCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<GameObject> Talking;

    private DialogueManager d;

    public override bool IsTrue()
    {
        return d.dialogueIsOn;
    }

    public override void OnStart()
    {
        d = Talking.Value.GetComponent<DialogueManager>();
    }

    public override void OnEnd()
    {
    }
}
