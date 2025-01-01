using UnityEngine;

public class PawnScript : MonoBehaviour
{
    public bool isQueen = false; // Indique si ce pion est une dame
    private Renderer pawnRenderer; // R�f�rence au composant Renderer du pion
    public GameObject crown; // R�f�rence � la couronne (assign�e dans l'inspecteur)
    private Color originalColor; // Stocke la couleur d'origine

    private void Start()
    {
        // Initialisation du Renderer pour acc�der au mat�riau du pion
        pawnRenderer = GetComponent<Renderer>();
        if (pawnRenderer != null)
        {
            // Sauvegarde de la couleur initiale du pion
            originalColor = pawnRenderer.material.color; 
        }

        // Cache la couronne au d�marrage, car le pion n'est pas encore une dame
        if (crown != null)
        {
            crown.SetActive(false); 
        }
    }

    public void Highlight(Color highlightColor)
    {
        if (pawnRenderer != null)
        {
            pawnRenderer.material.color = highlightColor; // Applique la couleur sp�cifi�e
        }
    }

    public void ResetColor()
    {
        if (pawnRenderer == null)
        {         
            return; // Sort de la m�thode si le Renderer n'est pas initialis�
        }

        // R�initialise la couleur au mat�riau d'origine
        pawnRenderer.material.color = originalColor;
    }

    public void TransformToQueen()
    {
        isQueen = true; // Marque le pion comme �tant une dame
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
            // D�sactiver le pion pour qu'il ne soit plus d�tect�
            gameObject.SetActive(false);

            // Mettre � jour les cases jouables
            CheckersGame.Instance.UpdatePositionColliders();

            // D�truit d�finitivement le pion
            Destroy(gameObject);            
        }
    }
}
