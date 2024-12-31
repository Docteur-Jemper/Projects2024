using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScriptTests
{
    private UIScript uiScript;

    [SetUp]
    public void Setup()
    {
        // Cr�e un objet de test et attache le script UIScript
        var gameObject = new GameObject();
        uiScript = gameObject.AddComponent<UIScript>();
    }

    [Test]
    public void PlayGame_LoadsGameScene()
    {
        // Simule le chargement de la sc�ne de jeu
        uiScript.PlayGame();
        Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
    }

    [Test]
    public void ReturnToMenu_LoadsMainMenu()
    {
        // Simule le retour au menu principal
        uiScript.ReturnToMenu();
        Assert.AreEqual("MainMenu", SceneManager.GetActiveScene().name);
    }

    [Test]
    public void QuitGame_ClosesApplication()
    {
        // Simule la fermeture de l'application
        Assert.DoesNotThrow(() => uiScript.QuitGame());
    }

    [Test]
    public void SelectClassicBoard_SetsCorrectBoardOption()
    {
        // V�rifie que le plateau classique est bien s�lectionn�
        uiScript.SelectClassicBoard();
        Assert.AreEqual("checkers_classic", GameOptions.Instance.selectedBoard);
    }

    [Test]
    public void SelectWoodBoard_SetsCorrectBoardOption()
    {
        // V�rifie que le plateau en bois est bien s�lectionn�
        uiScript.SelectWoodBoard();
        Assert.AreEqual("checkers_wood", GameOptions.Instance.selectedBoard);
    }
}
