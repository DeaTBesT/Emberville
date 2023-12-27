using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerReload : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
