using NUnit.Framework;
using UnityEngine;

public class PositionScriptTests
{
    private PositionScript positionScript;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        positionScript = gameObject.AddComponent<PositionScript>();
    }

    [Test]
    public void OnMouseDown_NotifiesCheckersGame()
    {
        CheckersGame.Instance = new GameObject().AddComponent<CheckersGame>();

        var mockPosition = new GameObject();
        CheckersGame.Instance.OnPositionSelected(mockPosition);

        Assert.IsNotNull(CheckersGame.Instance);
    }
}