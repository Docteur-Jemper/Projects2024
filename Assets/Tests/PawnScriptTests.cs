using NUnit.Framework;
using UnityEngine;

public class PawnScriptTests
{
    private PawnScript pawnScript;

    [SetUp]
    public void Setup()
    {
        // Cr�e un GameObject et attache le script PawnScript pour les tests
        var gameObject = new GameObject();
        pawnScript = gameObject.AddComponent<PawnScript>();
    }

    [Test]
    public void TransformToQueen_SetsIsQueenAndShowsCrown()
    {
        // Teste que la transformation d'un pion en reine fonctionne correctement
        var crown = new GameObject();
        crown.SetActive(false); // La couronne est masqu�e au d�part
        pawnScript.crown = crown;

        // Appelle TransformToQueen pour transformer le pion en reine
        pawnScript.TransformToQueen();

        // V�rifie que le pion est marqu� comme une reine et que la couronne est affich�e
        Assert.IsTrue(pawnScript.isQueen);
        Assert.IsTrue(crown.activeSelf);
    }

    [Test]
    public void Highlight_ChangesPawnColor()
    {
        // Teste que la m�thode Highlight change bien la couleur du pion
        var renderer = pawnScript.gameObject.AddComponent<MeshRenderer>();
        var material = new Material(Shader.Find("Standard"));
        renderer.material = material;

        // Applique une couleur rouge au pion
        pawnScript.Highlight(Color.red);

        // V�rifie que la couleur du mat�riel du pion est bien rouge
        Assert.AreEqual(Color.red, renderer.material.color);
    }

    [Test]
    public void ResetColor_RestoresOriginalColor()
    {
        // Teste que ResetColor restaure la couleur originale d'un pion
        var renderer = pawnScript.gameObject.AddComponent<MeshRenderer>();
        var material = new Material(Shader.Find("Standard"));
        renderer.material = material;

        // D�finit une couleur pour le pion
        Color originalColor = Color.blue;
        renderer.material.color = originalColor;

        // Change la couleur du pion puis la restaure
        pawnScript.Highlight(Color.red);
        pawnScript.ResetColor();

        // V�rifie que la couleur originale a bien �t� restaur�e
        Assert.AreEqual(originalColor, renderer.material.color);
    }
}
