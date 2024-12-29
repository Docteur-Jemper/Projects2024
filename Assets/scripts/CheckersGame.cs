using UnityEngine;

public class CheckersGame : MonoBehaviour
{
    public static CheckersGame Instance;

    private GameObject selectedPawn;

    private void Awake()
    {
        // Créer un singleton pour un accès global
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnPawnSelected(GameObject pawn)
    {
        // Désélectionner le pion actuellement sélectionné
        if (selectedPawn != null)
        {
            PawnScript previousPawnScript = selectedPawn.GetComponent<PawnScript>();
            if (previousPawnScript != null)
            {
                previousPawnScript.ResetColor();
            }
        }

        // Stocker le nouveau pion sélectionné
        selectedPawn = pawn;

        // Mettre en surbrillance le nouveau pion
        PawnScript pawnScript = selectedPawn.GetComponent<PawnScript>();
        if (pawnScript != null)
        {
            if (pawn.name.ToLower().Contains("white"))
            {
                pawnScript.Highlight(Color.yellow);
            }
            if (pawn.name.ToLower().Contains("dark"))
            {
                pawnScript.Highlight(new Color(0.3f, 0.3f, 0.3f)); // Gris foncé
            }
        }

        Debug.Log("Pion sélectionné : " + selectedPawn.name);
    }
}
