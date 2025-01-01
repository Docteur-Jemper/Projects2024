using NUnit.Framework;
using UnityEngine;

public class GameOptionsTests
{
    private GameOptions gameOptions;

    [SetUp]
    public void Setup()
    {
        // Crée un GameObject et attache le script GameOptions pour préparer le test
        var gameObject = new GameObject();
        gameOptions = gameObject.AddComponent<GameOptions>();
    }

    [Test]
    public void DefaultSelectedBoard_IsCheckersWood()
    {
        // Vérifie que la sélection par défaut du plateau est "checkers_wood"
        Assert.AreEqual("checkers_wood", gameOptions.selectedBoard);
    }

    [Test]
    public void SetBoard_UpdatesSelectedBoard()
    {
        // Change le plateau sélectionné et vérifie que la mise à jour est correcte
        gameOptions.SetBoard("checkers_classic");
        Assert.AreEqual("checkers_classic", gameOptions.selectedBoard);
    }
}