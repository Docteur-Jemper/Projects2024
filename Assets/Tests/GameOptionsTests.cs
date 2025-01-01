using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Tests unitaires pour la classe GameOptions qui gère les paramètres globaux du jeu.
/// </summary>
public class GameOptionsTests
{
    private GameOptions gameOptions;

    [SetUp]
    /// <summary>
    /// Prépare le test en instanciant un objet avec le script GameOptions attaché.
    /// </summary>
    public void Setup()
    {
        // Crée un GameObject et attache le script GameOptions pour préparer le test
        var gameObject = new GameObject();
        gameOptions = gameObject.AddComponent<GameOptions>();
    }

    [Test]
    /// <summary>
    /// Vérifie que la sélection par défaut du plateau est "checkers_wood".
    /// </summary>
    public void DefaultSelectedBoard_IsCheckersWood()
    {
        // Vérifie que la sélection par défaut du plateau est "checkers_wood"
        Assert.AreEqual("checkers_wood", gameOptions.selectedBoard);
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode SetBoard met à jour correctement le plateau sélectionné.
    /// </summary>
    public void SetBoard_UpdatesSelectedBoard()
    {
        // Change le plateau sélectionné et vérifie que la mise à jour est correcte
        gameOptions.SetBoard("checkers_classic");
        Assert.AreEqual("checkers_classic", gameOptions.selectedBoard);
    }
}