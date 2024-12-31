using UnityEngine;

public class RandomSky : MonoBehaviour
{
    public Material[] mat; // Liste de nos ciels

    void Start()
    {
        int random = Random.Range(0, mat.Length); // Nombre al�atoire
        RenderSettings.skybox = mat[random]; // Modification du ciel 
    }

    private void Update()
    {
        // Applique une l�g�re rotation sur la skybox (fond de la sc�ne de jeu)
        RenderSettings.skybox.SetFloat("_Rotation", 0.5f * Time.time);
    }
}
