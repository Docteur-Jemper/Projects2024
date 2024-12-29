using System.Linq;
using UnityEngine;

public class CheckersGame : MonoBehaviour
{
    public static CheckersGame Instance;

    public GameObject boardPositions; // Référence à l'objet contenant les positions (BoardPositions)
    private GameObject selectedPawn;  // Pion actuellement sélectionné

    private void Awake()
    {
        // Créer un singleton pour un accès global
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Met à jour les colliders des positions au démarrage
        UpdatePositionColliders();
    }

    public void OnPawnSelected(GameObject pawn)
    {
        // Si un pion est déjà sélectionné, réinitialiser sa couleur
        if (selectedPawn != null)
        {
            PawnScript previousPawnScript = selectedPawn.GetComponent<PawnScript>();
            if (previousPawnScript != null)
            {
                previousPawnScript.ResetColor();
            }
        }

        // Mettre à jour le pion sélectionné
        selectedPawn = pawn;

        // Mettre en surbrillance le nouveau pion
        HighlightPawn(selectedPawn);

        Debug.Log("Pion sélectionné : " + selectedPawn.name);
    }

    public void OnPositionSelected(GameObject position)
    {
        // Si aucun pion n'est sélectionné, ignorer
        if (selectedPawn == null) return;

        // Déplacer le pion vers les coordonnées de la position sélectionnée
        selectedPawn.transform.position = position.transform.position;

        Debug.Log("Pion déplacé vers : " + position.name);

        // Met à jour les colliders des positions après déplacement
        UpdatePositionColliders();
    }

    private void UpdatePositionColliders()
    {
        // Obtenir toutes les positions (enfants de BoardPositions)
        foreach (Transform position in boardPositions.transform)
        {
            BoxCollider collider = position.GetComponent<BoxCollider>();
            if (collider == null) continue;

            // Vérifier si cette position est occupée par un pion
            bool isOccupied = IsPositionOccupied(position.position);

            // Activer/désactiver le collider en fonction de l'occupation
            collider.enabled = !isOccupied;
        }
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        // Obtenir tous les pions dans la scène
        PawnScript[] allPawns = FindObjectsOfType<PawnScript>();

        // Vérifier si un pion occupe déjà cette position (tolérance pour éviter les erreurs de flottants)
        foreach (PawnScript pawn in allPawns)
        {
            if (Vector3.Distance(pawn.transform.position, position) < 0.01f)
            {
                return true; // Position occupée
            }
        }

        return false; // Position libre
    }

    private void HighlightPawn(GameObject pawn)
    {
        PawnScript pawnScript = pawn.GetComponent<PawnScript>();
        if (pawnScript != null)
        {
            if (pawn.name.ToLower().Contains("white"))
            {
                pawnScript.Highlight(Color.yellow);
            }
            else if (pawn.name.ToLower().Contains("dark"))
            {
                pawnScript.Highlight(new Color(0.2f, 0.2f, 0.2f)); // Gris foncé
            }
        }
    }
}
