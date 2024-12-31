using UnityEngine;

public class GameOptions : MonoBehaviour
{
    public static GameOptions Instance; // Singleton pour gérer les options globales

    public string selectedBoard = "checkers_wood"; // Plateau par défaut (en bois)

    private void Awake()
    {
        // Implémentation du singleton pour garantir qu'une seule instance de GameOptions existe
        if (Instance == null)
        {
            Instance = this; // Assigne l'instance actuelle
            DontDestroyOnLoad(gameObject); // Conserve l'objet entre les scènes
        }
        else
        {
            Destroy(gameObject); // Détruit les instances supplémentaires
        }
    }

    public void SetBoard(string boardName)
    {
        // Définit le plateau sélectionné
        selectedBoard = boardName;
        Debug.Log("Plateau sélectionné : " + selectedBoard);
    }
}
