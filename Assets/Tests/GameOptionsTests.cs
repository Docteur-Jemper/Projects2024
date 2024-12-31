using NUnit.Framework;
using UnityEngine;

public class GameOptionsTests
{
    private GameOptions gameOptions;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        gameOptions = gameObject.AddComponent<GameOptions>();
    }

    [Test]
    public void DefaultSelectedBoard_IsCheckersWood()
    {
        Assert.AreEqual("checkers_wood", gameOptions.selectedBoard);
    }

    [Test]
    public void SetBoard_UpdatesSelectedBoard()
    {
        gameOptions.SetBoard("checkers_classic");
        Assert.AreEqual("checkers_classic", gameOptions.selectedBoard);
    }
}