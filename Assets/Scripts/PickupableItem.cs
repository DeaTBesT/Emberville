using UnityEngine;
using UnityEngine.Events;

public class PickupableItem : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPickUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            _onPickUp?.Invoke();
        }
    }
}
