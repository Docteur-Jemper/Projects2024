using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public TextMeshProUGUI turnText; // Texte pour afficher le joueur actuel
    public TextMeshProUGUI errorText; // Texte pour afficher les messages d'erreur
    public TextMeshProUGUI endGameText; // Texte pour afficher le message de fin de partie

    private bool isWhiteTurn = true; // Détermine si c'est au tour des blancs
    private bool hasMoved = false;   // Indique si le joueur a effectué un mouvement pendant son tour

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

        // Met à jour les colliders (zones de détection) des positions 
        UpdatePositionColliders();

        // Initialiser le texte du Canvas pour afficher qui doit jouer
        UpdateTurnText();

        // Masquer le texte d'erreur et de fin au démarrage
        if (errorText != null)
        {
            errorText.gameObject.SetActive(false);
        }
        if (endGameText != null)
        {
            endGameText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Validation du tour par appui sur la touche "Entrée"
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!hasMoved)
            {
                ShowErrorMessage("Jouez avant de passer votre tour !");
                return;
            }

            SwitchTurn();
        }
    }

    private void SwitchTurn()
    {
        // Alterne entre les tours
        isWhiteTurn = !isWhiteTurn;

        // Réinitialiser le pion sélectionné
        if (selectedPawn != null)
        {
            PawnScript previousPawnScript = selectedPawn.GetComponent<PawnScript>();
            if (previousPawnScript != null)
            {
                previousPawnScript.ResetColor();
            }
            selectedPawn = null;
        }

        // Réinitialise l'état du mouvement pour le prochain joueur
        hasMoved = false;

        // Met à jour le texte du Canvas
        UpdateTurnText();
    }

    private void UpdateTurnText()
    {
        // Mis à jour du texte du Canvas pour le changement de tour
        if (turnText != null)
        {
            turnText.text = $"Au tour du joueur : {(isWhiteTurn ? "blanc" : "noir")}";
        }
    }

    private void ShowErrorMessage(string message)
    {
        // Affichage d'un message d'erreur dans le Canvas
        if (errorText != null)
        {
            errorText.text = message;
            StartCoroutine(DisplayErrorMessage());
        }
    }

    private IEnumerator DisplayErrorMessage()
    {
        errorText.gameObject.SetActive(true); // Affiche le message
        yield return new WaitForSeconds(2); // Attend 2 secondes
        errorText.gameObject.SetActive(false); // Cache le message
    }

    private void ApplyBoardSelection()
    {
        // Active le plateau sélectionné
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
        // Vérifie si le pion appartient au joueur qui doit jouer
        if (isWhiteTurn && !pawn.name.ToLower().Contains("white") || // Si le nom du pion sélectionné contient la couleur adverse et pas la nôtre
            !isWhiteTurn && !pawn.name.ToLower().Contains("dark"))
        {
            Debug.Log("Ce n'est pas votre tour !");
            return;
        }

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

        // Indique qu'un mouvement a été effectué
        hasMoved = true;

        // Vérifie si le pion doit devenir une dame
        CheckForQueen(selectedPawn, position.name);

        PlayMoveSound();
        UpdatePositionColliders();
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
        // Mise à jour des cases jouables
        foreach (Transform position in boardPositions.transform)
        {
            BoxCollider collider = position.GetComponent<BoxCollider>();
            if (collider == null) continue;

            bool isOccupied = IsPositionOccupied(position.position);
            collider.enabled = !isOccupied;
        }

        // Vérifie la fin de la partie après la mise à jour des positions
        CheckEndGame();
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        // Vérifier qu'une case est libre
        PawnScript[] allPawns = FindObjectsOfType<PawnScript>();

        foreach (PawnScript pawn in allPawns)
        {
            if (Vector3.Distance(pawn.transform.position, position) < 0.01f)
            {
                return true; // La case est occupée
            }
        }

        return false; // La case est libre
    }

    private void HighlightPawn(GameObject pawn)
    {
        // Changer la couleur des pions sélectionnés
        PawnScript pawnScript = pawn.GetComponent<PawnScript>();
        if (pawnScript != null)
        {
            string selectedBoard = GameOptions.Instance.selectedBoard;

            if (selectedBoard == "checkers_classic")
            {
                if (pawn.name.ToLower().Contains("white"))
                {
                    // Couleur de sélection pour pion blanc classique
                    pawnScript.Highlight(new Color(0.9f, 0.9f, 1.0f));
                }
                else if (pawn.name.ToLower().Contains("dark"))
                {
                    // Couleur de sélection pour pion noir classique
                    pawnScript.Highlight(new Color(0.3f, 0.3f, 0.5f));
                }
            }
            else if (selectedBoard == "checkers_wood")
            {
                if (pawn.name.ToLower().Contains("white"))
                {
                    // Couleur de sélection pour pion bois clair
                    pawnScript.Highlight(new Color(1.0f, 0.85f, 0.6f));
                }
                else if (pawn.name.ToLower().Contains("dark"))
                {
                    // Couleur de sélection pour pion bois foncé 
                    pawnScript.Highlight(new Color(0.1f, 0.1f, 0.1f));
                }
            }
        }
    }

    private void PlayMoveSound()
    {
        // Joueur un son au déplacement d'un pion
        if (audioSource != null && moveSound != null)
        {
            audioSource.PlayOneShot(moveSound);
        }
    }

    private void CheckEndGame()
    {
        // Vérifie si une couleur n'a plus de pions
        int whitePawns = FindObjectsOfType<PawnScript>().Count(pawn => pawn.name.ToLower().Contains("white"));
        int darkPawns = FindObjectsOfType<PawnScript>().Count(pawn => pawn.name.ToLower().Contains("dark"));

        if (whitePawns == 0)
        {
            EndGame("Les noirs gagnent !");
        }
        else if (darkPawns == 0)
        {
            EndGame("Les blancs gagnent !");
        }
    }

    private void EndGame(string message)
    {
        // Affiche un message de fin de partie et redirige vers le menu principal après 2 secondes
        if (endGameText != null)
        {
            endGameText.text = message;
            endGameText.gameObject.SetActive(true);
        }

        Debug.Log(message);

        // Lancer la redirection après 2 secondes
        StartCoroutine(RedirectToMainMenu());
    }

    private IEnumerator RedirectToMainMenu()
    {
        yield return new WaitForSeconds(2); // Attend 2 secondes
        SceneManager.LoadScene("MainMenu"); // Redirige vers le menu principal
    }
}
