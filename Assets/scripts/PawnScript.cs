using UnityEngine;

public class PawnScript : MonoBehaviour
{
    public bool isQueen = false; // Indique si ce pion est une dame
    private Renderer pawnRenderer; // Référence au composant Renderer du pion
    public GameObject crown; // Référence à la couronne (assignée dans l'inspecteur)
    private Color originalColor; // Stocke la couleur d'origine

    private void Start()
    {
        // Initialisation du Renderer pour accéder au matériau du pion
        pawnRenderer = GetComponent<Renderer>();
        if (pawnRenderer != null)
        {
            // Sauvegarde de la couleur initiale du pion
            originalColor = pawnRenderer.material.color; 
        }

        // Cache la couronne au démarrage, car le pion n'est pas encore une dame
        if (crown != null)
        {
            crown.SetActive(false); 
        }
    }

    public void Highlight(Color highlightColor)
    {
        if (pawnRenderer != null)
        {
            pawnRenderer.material.color = highlightColor; // Applique la couleur spécifiée
        }
    }

    public void ResetColor()
    {
        if (pawnRenderer == null)
        {         
            return; // Sort de la méthode si le Renderer n'est pas initialisé
        }

        // Réinitialise la couleur au matériau d'origine
        pawnRenderer.material.color = originalColor;
    }

    public void TransformToQueen()
    {
        isQueen = true; // Marque le pion comme étant une dame
        if (crown != null)
        {
            crown.SetActive(true); // Affiche la couronne pour indiquer la promotion
        }
        Debug.Log(gameObject.name + " est devenu une dame !");
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // Clic gauche
        {
            CheckersGame.Instance.OnPawnSelected(gameObject);
        }
        else if (Input.GetMouseButtonDown(1)) // Clic droit
        {
            // Désactiver le pion pour qu'il ne soit plus détecté
            gameObject.SetActive(false);

            // Mettre à jour les cases jouables
            CheckersGame.Instance.UpdatePositionColliders();

            // Détruit définitivement le pion
            Destroy(gameObject);            
        }
    }
}
