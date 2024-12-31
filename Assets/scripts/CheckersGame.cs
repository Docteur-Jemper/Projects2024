using System.Linq;
using UnityEngine;

public class CheckersGame : MonoBehaviour
{
    public static CheckersGame Instance;

    public GameObject checkersClassic; // Référence à l'objet checkers_classic
    public GameObject checkersWood;   // Référence à l'objet checkers_wood
    public GameObject boardPositionsClassic; // Référence à BoardPositionsClassic
    public GameObject boardPositionsWood;    // Référence à BoardPositionsWood
    public AudioClip moveSound; // Clip audio pour le déplacement des pions
    private AudioSource audioSource; // Source audio pour jouer les sons
    private GameObject selectedPawn;  // Pion actuellement sélectionné
    public GameObject boardPositions; // Position actuelle du plateau

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Récupérer ou ajouter un AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Appliquer le plateau sélectionné
        ApplyBoardSelection();

        // Met à jour les colliders des positions
        UpdatePositionColliders();
    }

    private void ApplyBoardSelection()
    {
        string selectedBoard = GameOptions.Instance.selectedBoard;

        if (selectedBoard == "checkers_classic")
        {
            checkersClassic.SetActive(true);
            checkersWood.SetActive(false);
            boardPositions = boardPositionsClassic;
        }
        else if (selectedBoard == "checkers_wood")
        {
            checkersClassic.SetActive(false);
            checkersWood.SetActive(true);
            boardPositions = boardPositionsWood;
        }

        Debug.Log("Plateau appliqué : " + selectedBoard);
    }

    public void OnPawnSelected(GameObject pawn)
    {
        if (selectedPawn != null)
        {
            PawnScript previousPawnScript = selectedPawn.GetComponent<PawnScript>();
            if (previousPawnScript != null)
            {
                previousPawnScript.ResetColor();
            }
        }

        selectedPawn = pawn;
        HighlightPawn(selectedPawn);
        Debug.Log("Pion sélectionné : " + selectedPawn.name);
    }

    public void OnPositionSelected(GameObject position)
    {
        if (selectedPawn == null) return;

        selectedPawn.transform.position = position.transform.position;
        Debug.Log("Pion déplacé vers : " + position.name);
        PlayMoveSound();
        UpdatePositionColliders();
    }

    private void UpdatePositionColliders()
    {
        foreach (Transform position in boardPositions.transform)
        {
            BoxCollider collider = position.GetComponent<BoxCollider>();
            if (collider == null) continue;

            bool isOccupied = IsPositionOccupied(position.position);
            collider.enabled = !isOccupied;
        }
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        PawnScript[] allPawns = FindObjectsOfType<PawnScript>();

        foreach (PawnScript pawn in allPawns)
        {
            if (Vector3.Distance(pawn.transform.position, position) < 0.01f)
            {
                return true;
            }
        }

        return false;
    }

    private void HighlightPawn(GameObject pawn)
    {
        PawnScript pawnScript = pawn.GetComponent<PawnScript>();
        if (pawnScript != null)
        {
            string selectedBoard = GameOptions.Instance.selectedBoard;

            if (selectedBoard == "checkers_classic")
            {
                if (pawn.name.ToLower().Contains("white"))
                {
                    // Couleur blanche pour plateau classique (dérivée de blanc)
                    pawnScript.Highlight(new Color(0.9f, 0.9f, 1.0f));
                }
                else if (pawn.name.ToLower().Contains("dark"))
                {
                    // Couleur noire pour plateau classique (dérivée de noir)
                    pawnScript.Highlight(new Color(0.4f, 0.4f, 0.5f));
                }
            }
            else if (selectedBoard == "checkers_wood")
            {
                if (pawn.name.ToLower().Contains("white"))
                {
                    // Surbrillance pour pion bois clair
                    pawnScript.Highlight(new Color(1.0f, 0.85f, 0.6f)); 
                }
                else if (pawn.name.ToLower().Contains("dark"))
                {
                    // Surbrillance pour pion bois foncé
                    pawnScript.Highlight(new Color(0.6f, 0.4f, 0.2f)); 
                }
            }
        }
    }

    private void PlayMoveSound()
    {
        if (audioSource != null && moveSound != null)
        {
            audioSource.PlayOneShot(moveSound);
        }
    }
}
