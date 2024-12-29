using UnityEngine;

public class PawnScript : MonoBehaviour
{
    public Material originalMaterial; // Matériau d'origine (à définir dans l'inspecteur)
    private Renderer pawnRenderer;

    private void Start()
    {
        // Récupérer le Renderer du pion
        pawnRenderer = GetComponent<Renderer>();
        if (pawnRenderer != null && originalMaterial == null)
        {
            // Si aucun matériau d'origine n'est défini dans l'inspecteur, le stocker automatiquement
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
        // Réappliquer le matériau d'origine
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
