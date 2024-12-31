using UnityEngine;

public class RandomSky : MonoBehaviour
{
    public Material[] mat; // Liste de nos ciels (skyboxes)

    void Start()
    {
        int random = Random.Range(0, mat.Length); // Nombre aléatoire
        RenderSettings.skybox = mat[random]; // Modification du ciel parmis celles présentes dans notre liste (liste définie sur Unity)
    }

    private void Update()
    {
        // Applique une légère rotation sur la skybox (fond de la scène de jeu)
        RenderSettings.skybox.SetFloat("_Rotation", 0.5f * Time.time);
    }
}
