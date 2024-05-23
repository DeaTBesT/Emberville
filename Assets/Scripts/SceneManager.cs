using UnityEngine;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        public void LoadScene(int index)
        {
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        }
        
        public void ExitMenu()
        {
            Application.Quit();
        }
    }
}