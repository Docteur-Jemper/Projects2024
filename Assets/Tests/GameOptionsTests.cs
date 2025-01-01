using NUnit.Framework;
using UnityEngine;

public class GameOptionsTests
{
    private GameOptions gameOptions;

    [SetUp]
    public void Setup()
    {
        // Cr�e un GameObject et attache le script GameOptions pour pr�parer le test
        var gameObject = new GameObject();
        gameOptions = gameObject.AddComponent<GameOptions>();
    }

    [Test]
    public void DefaultSelectedBoard_IsCheckersWood()
    {
        // V�rifie que la s�lection par d�faut du plateau est "checkers_wood"
        Assert.AreEqual("checkers_wood", gameOptions.selectedBoard);
    }

    [Test]
    public void SetBoard_UpdatesSelectedBoard()
    {
        // Change le plateau s�lectionn� et v�rifie que la mise � jour est correcte
        gameOptions.SetBoard("checkers_classic");
        Assert.AreEqual("checkers_classic", gameOptions.selectedBoard);
    }
}