using UnityEngine;

public class InteractorController : Controller
{
    [SerializeField] private LayerMask _interactLayer;
    [SerializeField] private float _interactRadius = 2f;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (TryToSearchPuzzle(out IPuzzle puzzle))
            {
                puzzle.ActivatePuzzle();
            }
        }
    }

    private bool TryToSearchPuzzle(out IPuzzle puzzle)
    {
        bool isSearched = false;
        puzzle = null;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position,  _interactRadius,_interactLayer);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IPuzzle searchedPuzzle))
            {
                puzzle = searchedPuzzle;
                isSearched = true;
                break;
            }
        }
        
        return isSearched;
    }
}
