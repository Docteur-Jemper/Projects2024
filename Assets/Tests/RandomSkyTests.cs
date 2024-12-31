using NUnit.Framework;
using UnityEngine;

public class RandomSkyTests
{
    private RandomSky randomSky;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        randomSky = gameObject.AddComponent<RandomSky>();
    }

    [Test]
    public void ApplyRandomSky_AssignsRandomSkybox()
    {
        // Préparation d'une liste de matériaux simulée
        var skyboxMaterials = new Material[]
        {
            new Material(Shader.Find("Standard")),
            new Material(Shader.Find("Standard"))
        };
        randomSky.skyboxes = skyboxMaterials;

        // Appel de la méthode à tester
        randomSky.ApplyRandomSky();

        // Vérification qu'un des matériaux a bien été assigné
        Assert.Contains(RenderSettings.skybox, skyboxMaterials);
    }
}
