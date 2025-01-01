using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Tests unitaires pour la classe RandomSky qui gère les changements aléatoires de skybox.
/// </summary>
public class RandomSkyTests
{
    private RandomSky randomSky; // Référence à l'objet RandomSky pour effectuer les tests

    [SetUp]
    /// <summary>
    /// Prépare le test en instanciant un objet avec le script RandomSky attaché.
    /// </summary>
    public void Setup()
    {
        // Création d'un objet de test et ajout du composant RandomSky
        var gameObject = new GameObject();
        randomSky = gameObject.AddComponent<RandomSky>();
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode ApplyRandomSky applique correctement une skybox aléatoire à partir d'une liste de matériaux.
    /// </summary>
    public void ApplyRandomSky_AssignsRandomSkybox()
    {
        // Préparation d'une liste de matériaux simulée
        var skyboxMaterials = new Material[]
        {
            new Material(Shader.Find("Standard")), // Skybox simulée 1
            new Material(Shader.Find("Standard")) // Skybox simulée 2
        };
        randomSky.skyboxes = skyboxMaterials;

        // Appel de la méthode ApplyRandomSky pour appliquer une skybox aléatoire
        randomSky.ApplyRandomSky();

        // Confirme qu'une des skyboxes assignées a bien été appliquée
        Assert.Contains(RenderSettings.skybox, skyboxMaterials);
    }
}
