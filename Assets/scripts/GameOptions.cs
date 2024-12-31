using UnityEngine;

public class GameOptions : MonoBehaviour
{
    public static GameOptions Instance; // Singleton pour g�rer les options globales

    public string selectedBoard = "checkers_wood"; // Plateau par d�faut (en bois)

    private void Awake()
    {
        // Impl�mentation du singleton pour garantir qu'une seule instance de GameOptions existe
        if (Instance == null)
        {
            Instance = this; // Assigne l'instance actuelle
            DontDestroyOnLoad(gameObject); // Conserve l'objet entre les sc�nes
        }
        else
        {
            Destroy(gameObject); // D�truit les instances suppl�mentaires
        }
    }

    public void SetBoard(string boardName)
    {
        // D�finit le plateau s�lectionn�
        selectedBoard = boardName;
        Debug.Log("Plateau s�lectionn� : " + selectedBoard);
    }
}
