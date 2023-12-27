using UnityEngine;
using UnityEngine.Events;

public class PuzzleCastle : Puzzle
{
    [SerializeField] private int _needPassword;

    [SerializeField] private UnityEvent _onActivatePuzzle;
    [SerializeField] private UnityEvent _onResolvePuzzle;
    
    private int _password;
    public string Password
    {
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            
            _password = int.Parse(value);

            if (_password == _needPassword)
            {
                ResolvePuzzle();
            }
        }
    }

    private bool _isCanUse = false;
    
    public void IsCanUse()
    {
        _isCanUse = true;
    }
    
    public override void ActivatePuzzle()
    {
        if (!_isCanUse)
        {
            return;
        }
        
        _onActivatePuzzle?.Invoke();
    }

    public override void DiactivatePuzzle()
    {
        throw new System.NotImplementedException();
    }

    public override void ResolvePuzzle()
    {
        _onResolvePuzzle?.Invoke();
    }
}