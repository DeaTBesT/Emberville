using UnityEngine;

namespace Puzzles.Laboratory
{
    public class LaboratoryItem : MonoBehaviour
    {
        [SerializeField] private PuzzleLaboratory _puzzleLaboratory;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController _))
            {
                _puzzleLaboratory.TakeItem();
            }
        }
    }
}