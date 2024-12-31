using UnityEngine;

public class RandomSky : MonoBehaviour
{
    public Material[] skyboxes; // Liste de nos ciels (skyboxes)

    void Start()
    {
        ApplyRandomSky(); // Appel de la méthode pour appliquer un ciel aléatoire
    }

    public void ApplyRandomSky()
    {
        if (skyboxes != null && skyboxes.Length > 0)
        {
            int random = Random.Range(0, skyboxes.Length); // Nombre aléatoire
            RenderSettings.skybox = skyboxes[random]; // Modification du ciel parmis celles présentes dans notre liste
        }
    }

    private void Update()
    {
        // Applique une légère rotation sur la skybox (fond de la scène de jeu)
        RenderSettings.skybox.SetFloat("_Rotation", 0.5f * Time.time);
    }
}
