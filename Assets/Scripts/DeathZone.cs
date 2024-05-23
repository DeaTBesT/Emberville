using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private bool _isReloadScene = true;
    [SerializeField] private Transform _respawnPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            if (_isReloadScene)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                player.transform.position = _respawnPoint.position;
            }
        }
    }
}