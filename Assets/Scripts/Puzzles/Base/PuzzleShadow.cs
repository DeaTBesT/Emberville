using System;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleShadow : Puzzle
{
    [SerializeField] private GameObject[] _puzzleItems;

    [SerializeField] private Transform _mainItem;
    [SerializeField] private float _mainItemSpeed;
    [SerializeField] private Transform _needPosition;
    [SerializeField] private Vector2 _tolerance;

    [Space, SerializeField] private UnityEvent _onResolvePuzzle;
    [SerializeField] private UnityEvent _onActivatePuzzle;
        
    public Action<bool> IsActivatePuzzle;

    private bool _isActivate;

    private const string _horizontalAxis = "Horizontal";
    private const string _verticalAxis = "Vertical";

    private void FixedUpdate()
    {
        if (!_isActivate)
        {
            return;
        }

        Control();
    }

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

    private void CheckItemPosition()
    {
        if (_mainItem.position == _needPosition.position)
        {
        }

        if (((_mainItem.position.x < _needPosition.position.x + _tolerance.x) &&
             (_mainItem.position.x > _needPosition.position.x - _tolerance.x) &&
             ((_mainItem.position.z < _needPosition.position.z + _tolerance.y) &&
              (_mainItem.position.z > _needPosition.position.z - _tolerance.y))))
        {
            ResolvePuzzle();
        }
    }

    public override void ActivatePuzzle()
    {
        _onActivatePuzzle?.Invoke();
        
        foreach (var item in _puzzleItems)
        {
            item.SetActive(true);
        }

        _isActivate = true;
        IsActivatePuzzle?.Invoke(true);
    }

    public override void DiactivatePuzzle()
    {
        foreach (var item in _puzzleItems)
        {
            item.SetActive(false);
        }

        _isActivate = false;
        IsActivatePuzzle?.Invoke(false);
    }

    public override void ResolvePuzzle()
    {
        DiactivatePuzzle();
        _onResolvePuzzle?.Invoke();
    }
}