using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class BoardState : IGameService
{
    /// <summary>
    /// returns winning player
    /// </summary>
    public static Action<Player> OnGameOver;

    Cell[,] cells;

    public BoardState()
    {
        //UI_Board.OnBoardChanged += OnBoardChanged;
        SetBoardState();
        ServiceLocator.AddService<BoardState>(this);
    }

    private void SetBoardState()
    {
        if(ServiceLocator.GetGameService<UI_Board>(out UI_Board board))
        {
            BoardConfig config = board.BoardConfig;

            cells = new Cell[config.GridSize.x, config.GridSize.y];

            for(int i = 0; i < config.GridSize.x; i++)
            {
                for (int j = 0; j < config.GridSize.y; j++)
                {
                    cells[i, j] = new Cell(new Vector2Int(i, j), board.Cells[i,j]);

                    board.Cells[i, j].SetOwner += OnBoardChanged;

                    if (i - 1 > - 1)
                        Cell.SetLeftRight(cells[i - 1, j], cells[i, j]);

                    if(j - 1 > - 1)
                        Cell.SetUpDown(cells[i, j - 1], cells[i, j]);
                }
            }
        }
    }

    private void OnBoardChanged(Vector2Int pos, Player player)
    {
        Debug.Log("Check Board State");

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                Cell cell1 = null;
                if (cells[pos.x, pos.y].TryGetCellFromMovement(new Vector2Int(i, j), ref cell1))
                {
                    if (player != cell1.Player)
                    {
                        continue;
                    }

                    Cell cell2 = null;
                    Cell cellOpp = null;
                    if (cells[pos.x, pos.y].TryGetCellFromMovement(new Vector2Int(i * 2, j * 2), ref cell2) ||
                        cells[pos.x, pos.y].TryGetCellFromMovement(new Vector2Int(i * -1, j * -1), ref cellOpp))
                    {
                        if (player == cell2?.Player || player == cellOpp?.Player)
                        {
                            //Tic Tac Toe Achieved
                            OnGameOver?.Invoke(player);
                            return;
                        }
                    }
                }
                
            }
        }
       
    }

}
