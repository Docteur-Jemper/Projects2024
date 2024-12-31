using NUnit.Framework;
using UnityEngine;
using TMPro;

public class CheckersGameTests
{
    private CheckersGame checkersGame;

    [SetUp]
    public void Setup()
    {
        // Cr�e un GameObject et attache le script CheckersGame
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
        // Teste que les messages d'erreur sont affich�s correctement
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
        // Simulez des pions blancs uniquement
        var whitePawn = new GameObject("WhitePawn").AddComponent<PawnScript>();
        whitePawn.name = "white_pawn";

        var darkPawn = new GameObject("DarkPawn").AddComponent<PawnScript>();
        darkPawn.name = "dark_pawn";

        // Supprimer tous les pions noirs
        Object.Destroy(darkPawn.gameObject);

        // Appeler UpdatePositionColliders pour d�clencher CheckEndGame
        checkersGame.UpdatePositionColliders();

        // V�rifier que le message de fin est affich�
        Assert.AreEqual("Les blancs gagnent !", checkersGame.endGameText.text);
        Assert.IsTrue(checkersGame.endGameText.gameObject.activeSelf);
    }


    [Test]
    public void ApplyBoardSelection_ActivatesCorrectBoard()
    {
        // Initialiser les GameObjects simul�s
        checkersGame.checkersClassic = new GameObject("ClassicBoard");
        checkersGame.checkersWood = new GameObject("WoodBoard");

        // Simulez la s�lection d'un plateau
        GameOptions.Instance.SetBoard("checkers_classic");
        checkersGame.ApplyBoardSelection();

        // V�rifie que le plateau classique est activ�
        Assert.IsTrue(checkersGame.checkersClassic.activeSelf);
        Assert.IsFalse(checkersGame.checkersWood.activeSelf);

        // Changer de plateau
        GameOptions.Instance.SetBoard("checkers_wood");
        checkersGame.ApplyBoardSelection();

        // V�rifier que le plateau en bois est activ�
        Assert.IsTrue(checkersGame.checkersWood.activeSelf);
        Assert.IsFalse(checkersGame.checkersClassic.activeSelf);
    }

}
