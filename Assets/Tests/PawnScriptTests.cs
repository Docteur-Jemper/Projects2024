using NUnit.Framework;
using UnityEngine;

public class PawnScriptTests
{
    private PawnScript pawnScript;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        pawnScript = gameObject.AddComponent<PawnScript>();
    }

    [Test]
    public void TransformToQueen_SetsIsQueenAndShowsCrown()
    {
        var crown = new GameObject();
        crown.SetActive(false);
        pawnScript.crown = crown;

        pawnScript.TransformToQueen();

        Assert.IsTrue(pawnScript.isQueen);
        Assert.IsTrue(crown.activeSelf);
    }

    [Test]
    public void Highlight_ChangesPawnColor()
    {
        var renderer = pawnScript.gameObject.AddComponent<MeshRenderer>();
        var material = new Material(Shader.Find("Standard"));
        renderer.material = material;

        pawnScript.Highlight(Color.red);

        Assert.AreEqual(Color.red, renderer.material.color);
    }

    [Test]
    public void ResetColor_RestoresOriginalColor()
    {
        var renderer = pawnScript.gameObject.AddComponent<MeshRenderer>();
        var material = new Material(Shader.Find("Standard"));
        renderer.material = material;

        Color originalColor = Color.blue;
        renderer.material.color = originalColor;

        pawnScript.Highlight(Color.red);
        pawnScript.ResetColor();

        Assert.AreEqual(originalColor, renderer.material.color);
    }
}
