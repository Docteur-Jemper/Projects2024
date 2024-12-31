using UnityEngine;

public class GameOptions : MonoBehaviour
{
    public static GameOptions Instance;

    public string selectedBoard = "checkers_wood"; // Plateau par défaut

    private void Awake()
    {
        // Singleton pour garder les paramètres entre les scènes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBoard(string boardName)
    {
        selectedBoard = boardName;
        Debug.Log("Plateau sélectionné : " + selectedBoard);
    }
}
