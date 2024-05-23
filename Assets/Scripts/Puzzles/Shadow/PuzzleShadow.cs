using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleShadow : MonoBehaviour, IPuzzle
{
    [SerializeField] private GameObject[] _puzzleItems;

    [SerializeField] private Transform _mainItem;
    [SerializeField] private float _mainItemSpeed;
    [SerializeField] private Transform _needPosition;
    [SerializeField] private Vector2 _tolerance;

    [Space, SerializeField] private UnityEvent _onResolvePuzzle;
    [SerializeField] private UnityEvent _onActivatePuzzle;
        
    public Action<bool> IsActivatePuzzle;

    private const string _horizontalAxis = "Horizontal";
    private const string _verticalAxis = "Vertical";

    private void Control()
    {
        Vector3 input = Vector3.zero;

        input.y = Input.GetAxisRaw(_horizontalAxis);
        input.z = Input.GetAxisRaw(_verticalAxis);

        _mainItem.Translate(input * _mainItemSpeed * Time.fixedTime);

        if (Mathf.Abs(input.x) + Mathf.Abs(input.z) != 0)
        {
            CheckItemPosition();
        }
    }

    private IEnumerator UpdateHandler()
    {
        while (true)
        {
            Control();
            yield return null;
        }   
    }
    
    private void CheckItemPosition()
    {
        if (((_mainItem.position.x < _needPosition.position.x + _tolerance.x) &&
             (_mainItem.position.x > _needPosition.position.x - _tolerance.x) &&
             ((_mainItem.position.z < _needPosition.position.z + _tolerance.y) &&
              (_mainItem.position.z > _needPosition.position.z - _tolerance.y))))
        {
            ResolvePuzzle();
        }
    }

    public bool ActivatePuzzle()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateHandler());
        
        _onActivatePuzzle?.Invoke();
        
        foreach (var item in _puzzleItems)
        {
            item.SetActive(true);
        }
        
        IsActivatePuzzle?.Invoke(true);
        return true;
    }

    public void DiactivatePuzzle()
    {
        foreach (var item in _puzzleItems)
        {
            item.SetActive(false);
        }
        
        StopAllCoroutines();
        IsActivatePuzzle?.Invoke(false);
    }

    public void ResolvePuzzle()
    {
        DiactivatePuzzle();
        _onResolvePuzzle?.Invoke();
    }
}