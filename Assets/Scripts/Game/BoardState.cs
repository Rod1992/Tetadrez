//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using PieceMovement;

public class BoardState : IGameService
{
    /// <summary>
    /// returns winning player
    /// </summary>
    public static Action<Player> OnGameOver;

    Cell[,] cells;

    public BoardState()
    {
        SetBoardState();
        ServiceLocator.AddService<CellModels>(new CellModels());
        ServiceLocator.AddService<BoardState>(this);
    }

    private void SetBoardState()
    {
        if(ServiceLocator.GetGameService<UI_Board>(out UI_Board board))
        {
            BoardConfig config = board.BoardConfig;

            cells = new Cell[config.GridSize.x, config.GridSize.y];

            for (int i = 0; i < config.GridSize.x; i++)
            {
                for (int j = 0; j < config.GridSize.y; j++)
                {
                    cells[i, j] = new Cell(new Vector2Int(i, j), board.Cells[i, j]);

                    cells[i, j].SetOwner += OnBoardChanged;

                    if (i - 1 > -1)
                        Cell.SetLeftRight(cells[i - 1, j], cells[i, j]);

                    if (j - 1 > -1)
                        Cell.SetUpDown(cells[i, j - 1], cells[i, j]);
                }
            }
        }
    }

    public void ResetBoardState()
    {
        if (ServiceLocator.GetGameService<UI_Board>(out UI_Board board))
        {
            BoardConfig config = board.BoardConfig;
            for (int i = 0; i < config.GridSize.x; i++)
            {
                for (int j = 0; j < config.GridSize.y; j++)
                {
                    cells[i, j].Model.chessPiece = null;
                }
            }
            ResetActiveCells();
        }
    }

    private void OnBoardChanged(Vector2Int pos, Cell cell)
    {
        Player player = cell.Model.Player;

        if (player == null)
            return;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                Cell cell1 = null;
                if (cells[pos.x, pos.y].TryGetCellFromMovement(new Vector2Int(i, j), ref cell1))
                {
                    if (player != cell1.Model.Player)
                    {
                        continue;
                    }

                    Cell cell2 = null;
                    Cell cellOpp = null;
                    if (cells[pos.x, pos.y].TryGetCellFromMovement(new Vector2Int(i * 2, j * 2), ref cell2) ||
                        cells[pos.x, pos.y].TryGetCellFromMovement(new Vector2Int(i * -1, j * -1), ref cellOpp))
                    {
                        if (player == cell2?.Model.Player || player == cellOpp?.Model.Player)
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

    public void SetActiveCells(Player player)
    {
        foreach (Cell cell in cells)
        {
            cell.Active = player == cell.Model.Player || cell.Model.Player == null;
        }
    }

    public void ResetActiveCells()
    {
        foreach (Cell cell in cells)
        {
            cell.Active = true;
        }
    }

    public bool CanMoveFromTo(CellModel from, CellModel to)
    {
        ChessPiece chessPiece = from.chessPiece;
        //no chess piece
        if (chessPiece == null)
            return false;
        //Cell already occupied
        if (to.chessPiece != null)
            return false;

        if (ServiceLocator.GetGameService<UI_Board>(out UI_Board board))
        {
            BoardConfig config = board.BoardConfig;
            Movement[] movements = chessPiece.Movement.GetPossibleMovements(board.BoardConfig);

            foreach (Movement movement in movements)
            {
                if (movement.Destination + from.Pos != to.Pos)
                    continue;

                if (movement.Collision)
                {
                    bool collided = false;
                    foreach (Vector2Int vector in movement.Path)
                    {
                        Vector2Int pos = from.Pos + vector;

                        if(pos.x > -1 && pos.y > -1 && pos.x < board.BoardConfig.GridSize.x && pos.y < board.BoardConfig.GridSize.y)
                        {
                            if (cells[pos.x, pos.y].Model.Player != null)
                            {
                                collided = true;
                                break;
                            }
                        }
                        
                    }
                    if(!collided)
                        return true;
                }
                else
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool CanPlayerMove(Player player)
    {
        foreach (Cell from in cells)
        {
            if (from.Model.Player != player)
                continue;

            foreach (Cell to in cells)
            {
                if (CanMoveFromTo(from.Model, to.Model))
                    return true;
            }
        }

        return false;
    }
}
