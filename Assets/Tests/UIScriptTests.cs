using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Tests unitaires pour la classe UIScript qui gère les interactions de l'utilisateur avec l'interface utilisateur.
/// </summary>
public class UIScriptTests
{
    private UIScript uiScript; // Référence au script UIScript à tester

    [SetUp]
    /// <summary>
    /// Prépare le test en instanciant un objet avec le script UIScript attaché.
    /// </summary>
    public void Setup()
    {
        // Crée un objet de test et attache le script UIScript
        var gameObject = new GameObject();
        uiScript = gameObject.AddComponent<UIScript>();
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode PlayGame charge correctement la scène de jeu.
    /// </summary>
    public void PlayGame_LoadsGameScene()
    {
        // Simule le chargement de la scène de jeu
        uiScript.PlayGame();
        Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode ReturnToMenu charge correctement le menu principal.
    /// </summary>
    public void ReturnToMenu_LoadsMainMenu()
    {
        // Simule le retour au menu principal
        uiScript.ReturnToMenu();
        Assert.AreEqual("MainMenu", SceneManager.GetActiveScene().name);
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode QuitGame ne lance pas d'exception lorsqu'elle est appelée.
    /// </summary>
    public void QuitGame_ClosesApplication()
    {
        // Simule la fermeture de l'application
        Assert.DoesNotThrow(() => uiScript.QuitGame());
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode SelectClassicBoard sélectionne correctement le plateau classique.
    /// </summary>
    public void SelectClassicBoard_SetsCorrectBoardOption()
    {
        // Vérifie que le plateau classique est bien sélectionné
        uiScript.SelectClassicBoard();
        Assert.AreEqual("checkers_classic", GameOptions.Instance.selectedBoard);
    }

    [Test]
    /// <summary>
    /// Vérifie que la méthode SelectWoodBoard sélectionne correctement le plateau en bois.
    /// </summary>
    public void SelectWoodBoard_SetsCorrectBoardOption()
    {
        // Vérifie que le plateau en bois est bien sélectionné
        uiScript.SelectWoodBoard();
        Assert.AreEqual("checkers_wood", GameOptions.Instance.selectedBoard);
    }
}
