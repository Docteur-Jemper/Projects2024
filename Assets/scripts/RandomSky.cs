using UnityEngine;

public class RandomSky : MonoBehaviour
{
    public Material[] skyboxes; // Liste de nos ciels (skyboxes)

    void Start()
    {
        ApplyRandomSky(); // Appel de la m�thode pour appliquer un ciel al�atoire
    }

    public void ApplyRandomSky()
    {
        if (skyboxes != null && skyboxes.Length > 0)
        {
            int random = Random.Range(0, skyboxes.Length); // Nombre al�atoire
            RenderSettings.skybox = skyboxes[random]; // Modification du ciel parmis celles pr�sentes dans notre liste
        }
    }

    private void Update()
    {
        // Applique une l�g�re rotation sur la skybox (fond de la sc�ne de jeu)
        RenderSettings.skybox.SetFloat("_Rotation", 0.5f * Time.time);
    }
}
