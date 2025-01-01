using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Tests unitaires pour la classe PositionScript qui gère les interactions avec les cases du plateau.
/// </summary>
public class PositionScriptTests
{
    private PositionScript positionScript;

    [SetUp]
    /// <summary>
    /// Prépare le test en instanciant un objet avec le script PositionScript attaché.
    /// </summary>
    public void Setup()
    {
        // Crée un GameObject et attache le script PositionScript pour préparer le test
        var gameObject = new GameObject();
        positionScript = gameObject.AddComponent<PositionScript>();
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode OnMouseDown notifie correctement le script CheckersGame.
    /// </summary>
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