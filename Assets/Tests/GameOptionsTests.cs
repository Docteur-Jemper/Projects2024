using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Tests unitaires pour la classe GameOptions qui g�re les param�tres globaux du jeu.
/// </summary>
public class GameOptionsTests
{
    private GameOptions gameOptions;

    [SetUp]
    /// <summary>
    /// Pr�pare le test en instanciant un objet avec le script GameOptions attach�.
    /// </summary>
    public void Setup()
    {
        // Cr�e un GameObject et attache le script GameOptions pour pr�parer le test
        var gameObject = new GameObject();
        gameOptions = gameObject.AddComponent<GameOptions>();
    }

    [Test]
    /// <summary>
    /// V�rifie que la s�lection par d�faut du plateau est "checkers_wood".
    /// </summary>
    public void DefaultSelectedBoard_IsCheckersWood()
    {
        // V�rifie que la s�lection par d�faut du plateau est "checkers_wood"
        Assert.AreEqual("checkers_wood", gameOptions.selectedBoard);
    }

    [Test]
    /// <summary>
    /// V�rifie que la m�thode SetBoard met � jour correctement le plateau s�lectionn�.
    /// </summary>
    public void SetBoard_UpdatesSelectedBoard()
    {
        // Change le plateau s�lectionn� et v�rifie que la mise � jour est correcte
        gameOptions.SetBoard("checkers_classic");
        Assert.AreEqual("checkers_classic", gameOptions.selectedBoard);
    }
}