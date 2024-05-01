using UnityEngine.SceneManagement;
using UnityEngine;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        public void LoadGame() 
        {
            SceneManager.LoadSceneAsync("Game AR");
        }
    }
}