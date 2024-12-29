using System.Linq;
using UnityEngine;

public class CheckersGame : MonoBehaviour
{
    public static CheckersGame Instance;

    public GameObject boardPositions; // R�f�rence � l'objet contenant les positions (BoardPositions)
    private GameObject selectedPawn;  // Pion actuellement s�lectionn�

    private void Awake()
    {
        // Cr�er un singleton pour un acc�s global
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Met � jour les colliders des positions au d�marrage
        UpdatePositionColliders();
    }

    public void OnPawnSelected(GameObject pawn)
    {
        // Si un pion est d�j� s�lectionn�, r�initialiser sa couleur
        if (selectedPawn != null)
        {
            PawnScript previousPawnScript = selectedPawn.GetComponent<PawnScript>();
            if (previousPawnScript != null)
            {
                previousPawnScript.ResetColor();
            }
        }

        // Mettre � jour le pion s�lectionn�
        selectedPawn = pawn;

        // Mettre en surbrillance le nouveau pion
        HighlightPawn(selectedPawn);

        Debug.Log("Pion s�lectionn� : " + selectedPawn.name);
    }

    public void OnPositionSelected(GameObject position)
    {
        // Si aucun pion n'est s�lectionn�, ignorer
        if (selectedPawn == null) return;

        // D�placer le pion vers les coordonn�es de la position s�lectionn�e
        selectedPawn.transform.position = position.transform.position;

        Debug.Log("Pion d�plac� vers : " + position.name);

        // Met � jour les colliders des positions apr�s d�placement
        UpdatePositionColliders();
    }

    private void UpdatePositionColliders()
    {
        // Obtenir toutes les positions (enfants de BoardPositions)
        foreach (Transform position in boardPositions.transform)
        {
            BoxCollider collider = position.GetComponent<BoxCollider>();
            if (collider == null) continue;

            // V�rifier si cette position est occup�e par un pion
            bool isOccupied = IsPositionOccupied(position.position);

            // Activer/d�sactiver le collider en fonction de l'occupation
            collider.enabled = !isOccupied;
        }
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        // Obtenir tous les pions dans la sc�ne
        PawnScript[] allPawns = FindObjectsOfType<PawnScript>();

        // V�rifier si un pion occupe d�j� cette position (tol�rance pour �viter les erreurs de flottants)
        foreach (PawnScript pawn in allPawns)
        {
            if (Vector3.Distance(pawn.transform.position, position) < 0.01f)
            {
                return true; // Position occup�e
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
                pawnScript.Highlight(new Color(0.2f, 0.2f, 0.2f)); // Gris fonc�
            }
        }
    }
}
