using UnityEngine;

public class GameOptions : MonoBehaviour
{
    public static GameOptions Instance;

    public string selectedBoard = "checkers_wood"; // Plateau par d�faut

    private void Awake()
    {
        // Singleton pour garder les param�tres entre les sc�nes
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
        Debug.Log("Plateau s�lectionn� : " + selectedBoard);
    }
}
