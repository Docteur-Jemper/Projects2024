using System.Linq;
using UnityEngine;

public class CheckersGame : MonoBehaviour
{
    public static CheckersGame Instance;

    public GameObject checkersClassic; // R�f�rence � l'objet checkers_classic
    public GameObject checkersWood;   // R�f�rence � l'objet checkers_wood
    public GameObject boardPositionsClassic; // R�f�rence � BoardPositionsClassic
    public GameObject boardPositionsWood;    // R�f�rence � BoardPositionsWood
    public AudioClip moveSound; // Clip audio pour le d�placement des pions
    private AudioSource audioSource; // Source audio pour jouer les sons
    private GameObject selectedPawn;  // Pion actuellement s�lectionn�
    public GameObject boardPositions; // Position actuelle du plateau

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {   
        // R�cup�rer ou ajouter un AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Appliquer le plateau s�lectionn�
        ApplyBoardSelection();

        // Met � jour les colliders des positions
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

        Debug.Log("Plateau appliqu� : " + selectedBoard);
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
        Debug.Log("Pion s�lectionn� : " + selectedPawn.name);
    }

    public void OnPositionSelected(GameObject position)
    {
        if (selectedPawn == null) return;

        selectedPawn.transform.position = position.transform.position;
        Debug.Log("Pion d�plac� vers : " + position.name);

        // V�rifie si le pion doit devenir une dame
        CheckForQueen(selectedPawn, position.name);

        PlayMoveSound();
        UpdatePositionColliders();
    }

    public void DeletePawnAtPosition(Vector3 position)
    {
        Debug.Log($"DeletePawnAtPosition appel� pour la position : {position}");

        // Trouver tous les pions dans la sc�ne
        PawnScript[] allPawns = FindObjectsOfType<PawnScript>();

        foreach (PawnScript pawn in allPawns)
        {          
            if (Vector3.Distance(pawn.transform.position, position) < 0.01f)
            {
                Debug.Log($"Pion supprim� : {pawn.gameObject.name}");
                Destroy(pawn.gameObject);
                UpdatePositionColliders();
                return;
            }
        }

        Debug.Log("Aucun pion trouv� � la position donn�e.");
    }

    private void CheckForQueen(GameObject pawn, string positionName)
    {
        PawnScript pawnScript = pawn.GetComponent<PawnScript>();
        if (pawnScript == null || pawnScript.isQueen) return;

        // Positions de promotion pour les pions blancs
        string[] whiteQueenPositions = { "Pos29", "Pos30", "Pos31", "Pos32" };

        // Positions de promotion pour les pions noirs
        string[] blackQueenPositions = { "Pos1", "Pos2", "Pos3", "Pos4" };

        if (pawn.name.ToLower().Contains("white") && whiteQueenPositions.Contains(positionName))
        {
            pawnScript.TransformToQueen();
        }
        else if (pawn.name.ToLower().Contains("dark") && blackQueenPositions.Contains(positionName))
        {
            pawnScript.TransformToQueen();
        }
    }

    public void UpdatePositionColliders()
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
                    // Couleur de s�lection pour pion blanc classique
                    pawnScript.Highlight(new Color(0.9f, 0.9f, 1.0f));
                }
                else if (pawn.name.ToLower().Contains("dark"))
                {
                    // Couleur de s�lection pour pion noir classique
                    pawnScript.Highlight(new Color(0.3f, 0.3f, 0.5f));
                }
            }
            else if (selectedBoard == "checkers_wood")
            {
                if (pawn.name.ToLower().Contains("white"))
                {
                    // Couleur de s�lection pour pion bois clair
                    pawnScript.Highlight(new Color(1.0f, 0.85f, 0.6f));
                }
                else if (pawn.name.ToLower().Contains("dark"))
                {
                    // Couleur de s�lection pour pion bois fonc� 
                    pawnScript.Highlight(new Color(0.1f, 0.1f, 0.1f));
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
