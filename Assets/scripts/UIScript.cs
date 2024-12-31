using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    // Au démarrage (lancement du jeu)
    private void Start()
    {
        // Met à jour l'apparence du plateau sélectionné au démarrage
        UpdateBoardSelectionVisuals();
    }

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

    // Action de retour vers le menu principal
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectClassicBoard()
    {
        GameOptions.Instance.SetBoard("checkers_classic");
        UpdateBoardSelectionVisuals();
    }

    public void SelectWoodBoard()
    {
        GameOptions.Instance.SetBoard("checkers_wood");
        UpdateBoardSelectionVisuals();
    }

    private void UpdateBoardSelectionVisuals()
    {
        // Recherche des boutons par leur nom dans la hiérarchie
        GameObject classicButton = GameObject.Find("ImageCheckersClassic");
        GameObject woodButton = GameObject.Find("ImageCheckersWood");

        if (classicButton != null && woodButton != null)
        {
            // Réinitialiser les échelles des boutons
            classicButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            woodButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            // Mettre en avant le plateau sélectionné en changeant la taille
            if (GameOptions.Instance.selectedBoard == "checkers_classic")
            {
                classicButton.transform.localScale = Vector3.one * 1.8f; // Agrandir légèrement
            }
            else if (GameOptions.Instance.selectedBoard == "checkers_wood")
            {
                woodButton.transform.localScale = Vector3.one * 1.8f; // Agrandir légèrement
            }
        }
    }
}
