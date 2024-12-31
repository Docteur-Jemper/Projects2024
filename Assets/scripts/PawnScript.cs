using UnityEngine;

public class PawnScript : MonoBehaviour
{
    public bool isQueen = false; // Indique si ce pion est une dame
    private Renderer pawnRenderer;
    public GameObject crown; // Référence à la couronne (assignée dans l'inspecteur)
    private Color originalColor; // Stocke la couleur d'origine

    private void Start()
    {
        pawnRenderer = GetComponent<Renderer>();
        if (pawnRenderer != null)
        {
            originalColor = pawnRenderer.material.color; // Stocke la couleur d'origine
        }

        if (crown != null)
        {
            crown.SetActive(false); // Masquer la couronne au début
        }
    }

    public void Highlight(Color highlightColor)
    {
        if (pawnRenderer != null)
        {
            pawnRenderer.material.color = highlightColor;
        }
    }

    public void ResetColor()
    {
        if (pawnRenderer == null)
        {         
            return;
        }

        pawnRenderer.material.color = originalColor;
    }

    public void TransformToQueen()
    {
        isQueen = true; // Marquer comme une dame
        if (crown != null)
        {
            crown.SetActive(true); // Afficher la couronne
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

            // Supprimer le pion
            Destroy(gameObject);            
        }
    }
}
