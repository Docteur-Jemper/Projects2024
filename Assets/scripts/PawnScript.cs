using UnityEngine;

public class PawnScript : MonoBehaviour
{
    public Material originalMaterial; // Mat�riau d'origine (� d�finir dans l'inspecteur)
    private Renderer pawnRenderer;

    private void Start()
    {
        // R�cup�rer le Renderer du pion
        pawnRenderer = GetComponent<Renderer>();
        if (pawnRenderer != null && originalMaterial == null)
        {
            // Si aucun mat�riau d'origine n'est d�fini dans l'inspecteur, le stocker automatiquement
            originalMaterial = pawnRenderer.material;
        }
    }

    public void Highlight(Color highlightColor)
    {
        // Appliquer une couleur de surbrillance
        if (pawnRenderer != null)
        {
            pawnRenderer.material.color = highlightColor;
        }
    }

    public void ResetColor()
    {
        // R�appliquer le mat�riau d'origine
        if (pawnRenderer != null && originalMaterial != null)
        {
            pawnRenderer.material = originalMaterial;
        }
    }

    private void OnMouseDown()
    {
        // Notifier le script CheckersGame lors d'un clic sur le pion
        CheckersGame.Instance.OnPawnSelected(this.gameObject);
    }
}
