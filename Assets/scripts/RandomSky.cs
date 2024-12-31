using UnityEngine;

public class RandomSky : MonoBehaviour
{
    public Material[] mat; // Liste de nos ciels (skyboxes)

    void Start()
    {
        int random = Random.Range(0, mat.Length); // Nombre al�atoire
        RenderSettings.skybox = mat[random]; // Modification du ciel parmis celles pr�sentes dans notre liste (liste d�finie sur Unity)
    }

    private void Update()
    {
        // Applique une l�g�re rotation sur la skybox (fond de la sc�ne de jeu)
        RenderSettings.skybox.SetFloat("_Rotation", 0.5f * Time.time);
    }
}
