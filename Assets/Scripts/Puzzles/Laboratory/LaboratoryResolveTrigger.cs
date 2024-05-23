using UnityEngine;

namespace Puzzles.Laboratory
{
    public class LaboratoryResolveTrigger : MonoBehaviour
    {
        [SerializeField] private PuzzleLaboratory _puzzleLaboratory;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController _))
            {
                _puzzleLaboratory.ResolvePuzzle();
            }
        }
    }
}