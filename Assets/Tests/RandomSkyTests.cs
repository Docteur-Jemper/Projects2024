using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Tests unitaires pour la classe RandomSky qui g�re les changements al�atoires de skybox.
/// </summary>
public class RandomSkyTests
{
    private RandomSky randomSky; // R�f�rence � l'objet RandomSky pour effectuer les tests

    [SetUp]
    /// <summary>
    /// Pr�pare le test en instanciant un objet avec le script RandomSky attach�.
    /// </summary>
    public void Setup()
    {
        // Cr�ation d'un objet de test et ajout du composant RandomSky
        var gameObject = new GameObject();
        randomSky = gameObject.AddComponent<RandomSky>();
    }

    [Test]
    /// <summary>
    /// V�rifie que la m�thode ApplyRandomSky applique correctement une skybox al�atoire � partir d'une liste de mat�riaux.
    /// </summary>
    public void ApplyRandomSky_AssignsRandomSkybox()
    {
        // Pr�paration d'une liste de mat�riaux simul�e
        var skyboxMaterials = new Material[]
        {
            new Material(Shader.Find("Standard")), // Skybox simul�e 1
            new Material(Shader.Find("Standard")) // Skybox simul�e 2
        };
        randomSky.skyboxes = skyboxMaterials;

        // Appel de la m�thode ApplyRandomSky pour appliquer une skybox al�atoire
        randomSky.ApplyRandomSky();

        // Confirme qu'une des skyboxes assign�es a bien �t� appliqu�e
        Assert.Contains(RenderSettings.skybox, skyboxMaterials);
    }
}
