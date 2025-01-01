using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Tests unitaires pour la classe UIScript qui g�re les interactions de l'utilisateur avec l'interface utilisateur.
/// </summary>
public class UIScriptTests
{
    private UIScript uiScript; // R�f�rence au script UIScript � tester

    [SetUp]
    /// <summary>
    /// Pr�pare le test en instanciant un objet avec le script UIScript attach�.
    /// </summary>
    public void Setup()
    {
        // Cr�e un objet de test et attache le script UIScript
        var gameObject = new GameObject();
        uiScript = gameObject.AddComponent<UIScript>();
    }

    [Test]
    /// <summary>
    /// V�rifie que la m�thode PlayGame charge correctement la sc�ne de jeu.
    /// </summary>
    public void PlayGame_LoadsGameScene()
    {
        // Simule le chargement de la sc�ne de jeu
        uiScript.PlayGame();
        Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
    }

    [Test]
    /// <summary>
    /// V�rifie que la m�thode ReturnToMenu charge correctement le menu principal.
    /// </summary>
    public void ReturnToMenu_LoadsMainMenu()
    {
        // Simule le retour au menu principal
        uiScript.ReturnToMenu();
        Assert.AreEqual("MainMenu", SceneManager.GetActiveScene().name);
    }

    [Test]
    /// <summary>
    /// V�rifie que la m�thode QuitGame ne lance pas d'exception lorsqu'elle est appel�e.
    /// </summary>
    public void QuitGame_ClosesApplication()
    {
        // Simule la fermeture de l'application
        Assert.DoesNotThrow(() => uiScript.QuitGame());
    }

    [Test]
    /// <summary>
    /// V�rifie que la m�thode SelectClassicBoard s�lectionne correctement le plateau classique.
    /// </summary>
    public void SelectClassicBoard_SetsCorrectBoardOption()
    {
        // V�rifie que le plateau classique est bien s�lectionn�
        uiScript.SelectClassicBoard();
        Assert.AreEqual("checkers_classic", GameOptions.Instance.selectedBoard);
    }

    [Test]
    /// <summary>
    /// V�rifie que la m�thode SelectWoodBoard s�lectionne correctement le plateau en bois.
    /// </summary>
    public void SelectWoodBoard_SetsCorrectBoardOption()
    {
        // V�rifie que le plateau en bois est bien s�lectionn�
        uiScript.SelectWoodBoard();
        Assert.AreEqual("checkers_wood", GameOptions.Instance.selectedBoard);
    }
}
