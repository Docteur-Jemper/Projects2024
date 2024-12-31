using NUnit.Framework;
using UnityEngine;
using TMPro;

public class CheckersGameTests
{
    private CheckersGame checkersGame;

    [SetUp]
    public void Setup()
    {
        // Crée un GameObject et attache le script CheckersGame
        var gameObject = new GameObject();
        checkersGame = gameObject.AddComponent<CheckersGame>();

        // Configure les objets TextMeshPro pour les tests
        checkersGame.turnText = new GameObject().AddComponent<TextMeshProUGUI>();
        checkersGame.errorText = new GameObject().AddComponent<TextMeshProUGUI>();
        checkersGame.endGameText = new GameObject().AddComponent<TextMeshProUGUI>();

        // Configure les plateaux
        checkersGame.boardPositionsClassic = new GameObject();
        checkersGame.boardPositionsWood = new GameObject();
    }

    [Test]
    public void SwitchTurn_AlternatesPlayerTurns()
    {
        // Teste que le changement de tour alterne correctement entre les joueurs
        bool initialTurn = checkersGame.IsWhiteTurn;
        checkersGame.SwitchTurn();
        Assert.AreNotEqual(initialTurn, checkersGame.IsWhiteTurn);
    }

    [Test]
    public void ShowErrorMessage_DisplaysErrorText()
    {
        // Teste que les messages d'erreur sont affichés correctement
        checkersGame.ShowErrorMessage("Test Error");
        Assert.AreEqual("Test Error", checkersGame.errorText.text);
        Assert.IsTrue(checkersGame.errorText.gameObject.activeSelf);
    }

    [Test]
    public void UpdateTurnText_ReflectsCurrentPlayer()
    {
        // Teste que le texte du tour correspond au joueur actuel
        checkersGame.UpdateTurnText();
        Assert.AreEqual("Au tour du joueur : blanc", checkersGame.turnText.text);

        checkersGame.SwitchTurn();
        checkersGame.UpdateTurnText();
        Assert.AreEqual("Au tour du joueur : noir", checkersGame.turnText.text);
    }

    [Test]
    public void CheckEndGame_DisplaysWinningMessage()
    {
        // Simule une condition de victoire
        var pawns = Object.FindObjectsOfType<PawnScript>();
        foreach (var pawn in pawns)
        {
            if (pawn.name.ToLower().Contains("white"))
            {
                Object.Destroy(pawn.gameObject);
            }
        }

        checkersGame.UpdatePositionColliders();
        Assert.AreEqual("Les noirs gagnent !", checkersGame.endGameText.text);
        Assert.IsTrue(checkersGame.endGameText.gameObject.activeSelf);
    }

    [Test]
    public void ApplyBoardSelection_ActivatesCorrectBoard()
    {
        // Teste que le plateau sélectionné est activé
        GameOptions.Instance.SetBoard("checkers_classic");
        checkersGame.ApplyBoardSelection();
        Assert.IsTrue(checkersGame.boardPositionsClassic.activeSelf);
        Assert.IsFalse(checkersGame.boardPositionsWood.activeSelf);

        GameOptions.Instance.SetBoard("checkers_wood");
        checkersGame.ApplyBoardSelection();
        Assert.IsTrue(checkersGame.boardPositionsWood.activeSelf);
        Assert.IsFalse(checkersGame.boardPositionsClassic.activeSelf);
    }
}
