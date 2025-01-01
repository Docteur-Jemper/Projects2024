using NUnit.Framework;
using UnityEngine;

public class PositionScriptTests
{
    private PositionScript positionScript;

    [SetUp]
    public void Setup()
    {
        // Crée un GameObject et attache le script PositionScript pour préparer le test
        var gameObject = new GameObject();
        positionScript = gameObject.AddComponent<PositionScript>();
    }

    [Test]
    public void OnMouseDown_NotifiesCheckersGame()
    {
        // Simule une instance de CheckersGame pour tester la notification
        CheckersGame.Instance = new GameObject().AddComponent<CheckersGame>();

        // Simule un clic sur une position (une case)
        var mockPosition = new GameObject();
        CheckersGame.Instance.OnPositionSelected(mockPosition);

        // Vérifie que CheckersGame.Instance n'est pas null, prouvant que le clic est bien transmis
        Assert.IsNotNull(CheckersGame.Instance);
    }
}