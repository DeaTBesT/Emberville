using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleLaboratory : MonoBehaviour, IPuzzle
{
    [SerializeField] private GameObject _puzzlePanel;
    [SerializeField] private TextMeshProUGUI _textDescritpion;
   
    [SerializeField] private GameObject _resolveTrigger;
    [SerializeField] private TakeItems[] _items;

    [SerializeField] private UnityEvent _onPuzzleResolved;
    
    private int _currentTask = 0;
    
    [System.Serializable]
    public struct TakeItems
    {
        public string description;
        public GameObject laboratoryItem;
        public UnityEvent onTakeItem;
    }

    public void ActivatePuzzleEvent()
    {
        _resolveTrigger.SetActive(false);
        _currentTask = 0;
        _puzzlePanel.SetActive(true);
        _items[_currentTask].laboratoryItem.SetActive(true);
        _textDescritpion.text = _items[_currentTask].description;
    }
    
    public bool ActivatePuzzle()
    {
        _resolveTrigger.SetActive(false);
        _currentTask = 0;
        _puzzlePanel.SetActive(true);
        _items[_currentTask].laboratoryItem.SetActive(true);
        _textDescritpion.text = _items[_currentTask].description;

        return true;
    }

    public void DiactivatePuzzle()
    {
        throw new System.NotImplementedException();
    }

    public void ResolvePuzzle()
    {
        _puzzlePanel.SetActive(false);
        _onPuzzleResolved?.Invoke();
    }

    public void TakeItem()
    {
        _items[_currentTask].onTakeItem?.Invoke();
        _items[_currentTask].laboratoryItem.SetActive(false);
    }

    public void NextItem()
    {
        _currentTask++;
        
        _items[_currentTask].laboratoryItem.SetActive(true);
        _textDescritpion.text = _items[_currentTask].description;
    }
}