using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private UnityEvent _onStartDialogue;
    [SerializeField] private UnityEvent _onEndDialogue;
    
    private Queue<string> sentences;

    private Dialogue _dialogue;
    
    private void Start() 
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _dialogue = dialogue;
        
        _onStartDialogue?.Invoke();
        _dialogue.onStartDialogue?.Invoke();
        _dialoguePanel.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void EndDialogue()
    {
        _dialoguePanel.SetActive(false);
        _onEndDialogue?.Invoke();
        _dialogue.onEndDialogue?.Invoke();
    }
}