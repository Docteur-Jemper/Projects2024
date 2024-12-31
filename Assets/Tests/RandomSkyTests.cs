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
        // Pr�paration d'une liste de mat�riaux simul�e
        var skyboxMaterials = new Material[]
        {
            new Material(Shader.Find("Standard")),
            new Material(Shader.Find("Standard"))
        };
        randomSky.skyboxes = skyboxMaterials;

        // Appel de la m�thode � tester
        randomSky.ApplyRandomSky();

        // V�rification qu'un des mat�riaux a bien �t� assign�
        Assert.Contains(RenderSettings.skybox, skyboxMaterials);
    }
}
