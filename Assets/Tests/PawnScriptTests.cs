using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Tests unitaires pour la classe PawnScript qui gère les fonctionnalités d'un pion.
/// </summary
public class PawnScriptTests
{
    private PawnScript pawnScript;

    [SetUp]
    /// <summary>
    /// Prépare le test en instanciant un objet avec le script PawnScript attaché.
    /// </summary>
    public void Setup()
    {
        // Crée un GameObject et attache le script PawnScript pour les tests
        var gameObject = new GameObject();
        pawnScript = gameObject.AddComponent<PawnScript>();
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode TransformToQueen transforme correctement un pion en reine et affiche la couronne.
    /// </summary>
    public void TransformToQueen_SetsIsQueenAndShowsCrown()
    {
        // Teste que la transformation d'un pion en reine fonctionne correctement
        var crown = new GameObject();
        crown.SetActive(false); // La couronne est masquée au départ
        pawnScript.crown = crown;

        // Appelle TransformToQueen pour transformer le pion en reine
        pawnScript.TransformToQueen();

        // Vérifie que le pion est marqué comme une reine et que la couronne est affichée
        Assert.IsTrue(pawnScript.isQueen);
        Assert.IsTrue(crown.activeSelf);
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode Highlight change la couleur du pion.
    /// </summary>
    public void Highlight_ChangesPawnColor()
    {
        // Teste que la méthode Highlight change bien la couleur du pion
        var renderer = pawnScript.gameObject.AddComponent<MeshRenderer>();
        var material = new Material(Shader.Find("Standard"));
        renderer.material = material;

        // Applique une couleur rouge au pion
        pawnScript.Highlight(Color.red);

        // Vérifie que la couleur du matériel du pion est bien rouge
        Assert.AreEqual(Color.red, renderer.material.color);
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode ResetColor restaure la couleur d'origine d'un pion.
    /// </summary>
    public void ResetColor_RestoresOriginalColor()
    {
        // Teste que ResetColor restaure la couleur originale d'un pion
        var renderer = pawnScript.gameObject.AddComponent<MeshRenderer>();
        var material = new Material(Shader.Find("Standard"));
        renderer.material = material;

        // Définit une couleur pour le pion
        Color originalColor = Color.blue;
        renderer.material.color = originalColor;

        // Change la couleur du pion puis la restaure
        pawnScript.Highlight(Color.red);
        pawnScript.ResetColor();

        // Vérifie que la couleur originale a bien été restaurée
        Assert.AreEqual(originalColor, renderer.material.color);
    }
}
