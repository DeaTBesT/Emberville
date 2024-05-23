using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private bool _triggerDialogueOnStart = false;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private Dialogue dialogue;

    private void Start()
    {
        if (_triggerDialogueOnStart)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue ()
    {
        _dialogueManager.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            TriggerDialogue();
        }
    }
}
