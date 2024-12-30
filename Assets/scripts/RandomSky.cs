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
        RenderSettings.skybox.SetFloat("_Rotation", 0.5f * Time.time);
    }
}
