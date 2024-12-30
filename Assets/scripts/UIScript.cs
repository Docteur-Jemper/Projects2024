using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    // Action de lancement de la partie (Interface menu)
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    // Action de quitter le jeu (Interface menu)
    public void QuitGame()
    {
        Application.Quit();
    }
}
