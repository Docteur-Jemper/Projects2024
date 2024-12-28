using UnityEngine;

public class CheckersGame : MonoBehaviour
{
    private int[,] board =
    {
        { 0, 2, 0, 2, 0, 2, 0, 2 },
        { 2, 0, 2, 0, 2, 0, 2, 0 },
        { 0, 2, 0, 2, 0, 2, 0, 2 },
        { 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 1, 0, 1, 0, 1, 0 },
        { 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0 }
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrintBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrintBoard()
    {
        string boardRepresentation = "";
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                boardRepresentation += board[row, col] + " ";
            }
            boardRepresentation += "\n";
        }
        Debug.Log(boardRepresentation);
    }

}
