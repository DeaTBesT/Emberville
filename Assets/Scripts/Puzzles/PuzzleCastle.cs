using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PuzzleCastle : MonoBehaviour, IPuzzle
{
    [SerializeField] private string _taskTitle;
    [SerializeField] private string[] _answers;
        
    [Header("UI")]
    [SerializeField] private GameObject _puzzleCanvas;
    [SerializeField] private TextMeshProUGUI _textTask;
    [SerializeField] private TMP_InputField _answerInputField;
    [SerializeField] private Button _buttonSubmitAnswer;

    [Header("Events")] 
    [SerializeField] private UnityEvent _onResolve;
    
    private string _password;
    
    public bool IsCanUse { get; set; } = false;

    private void OnEnable()
    {
        _answerInputField.onValueChanged.AddListener(OnAnswerChanged);
        _buttonSubmitAnswer.onClick.AddListener(SubmitAnswer);
    }

    private void OnDisable()
    {
        _answerInputField.onValueChanged.RemoveListener(OnAnswerChanged);
        _buttonSubmitAnswer.onClick.RemoveListener(SubmitAnswer);
    }

    public bool ActivatePuzzle()
    {
        if (!IsCanUse)
        {
            return false;
        }
        
        _puzzleCanvas.SetActive(true);
        _textTask.text = _taskTitle;

        return true;
    }

    public void DiactivatePuzzle()
    {
        _puzzleCanvas.SetActive(false);
    }

    public void ResolvePuzzle()
    {
        _puzzleCanvas.SetActive(false);
        
        _onResolve?.Invoke();
    }

    private void SubmitAnswer()
    {
        if (_answers.Contains(_password))
        {
            ResolvePuzzle();
        }
    }

    private void OnAnswerChanged(string text)
    {
        _password = text;
    }
}