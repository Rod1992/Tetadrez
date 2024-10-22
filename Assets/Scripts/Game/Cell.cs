//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System;

public class Cell
{
    /// <summary>
    /// returns pos of cell
    /// </summary>
    public Action<Vector2Int, Cell> SetOwner;

    UI_Cell ui_Cell;

    public bool Active
    {
        get
        {
            return ui_Cell.gameObject.activeSelf;
        }
        set
        {
            ui_Cell.gameObject.SetActive(value);
        }
    }

    public Vector2Int Pos { get; private set; }

    public Cell Up
    {
        get; private set;
    }
    public Cell Down
    {
        get; private set;
    }
    public Cell Left
    {
        get; private set;
    }
    public Cell Right
    {
        get; private set;
    }

    public UI_Cell UI_Cell
    {
        get => ui_Cell;
    }

    public static void SetLeftRight(Cell left, Cell right)
    {
        if (left == null || right == null)
            return;

        left.Right = right;
        right.Left = left;
    }

    public static void SetUpDown(Cell up, Cell down)
    {
        if (up == null || down == null)
            return;

        up.Down = down;
        down.Up = up;
    }

    public Cell(Vector2Int pos, UI_Cell ui_Cell)
    {
        this.Pos = pos;
        this.ui_Cell = ui_Cell;
        ui_Cell.SetOwner += this.OnSetOwner;
        ui_Cell.OnSwap += this.OnSwapPieces;
    }

    public CellModel Model
    {
        get
        {
            ServiceLocator.GetGameService<CellModels>(out CellModels models);

            return models.Get(Pos);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movement"></param>
    /// <param name="cell"></param>
    /// <returns>true if cell found</returns>
    public bool TryGetCellFromMovement(Vector2Int movement, ref Cell cell)
    {
        Vector2Int consumedmovement = Vector2Int.zero;

        if (movement.x != 0)
        {
            cell = movement.x > 0 ? Right : Left;
            consumedmovement = new Vector2Int(movement.x > 0 ? movement.x - 1 : movement.x + 1, movement.y);
        } else if (movement.y != 0)
        {
            cell = movement.y > 0 ? Up : Down;
            consumedmovement = new Vector2Int(movement.x, movement.y > 0 ? movement.y - 1 : movement.y + 1);
        }

        if (cell == null)
        {
            return false;
        }
        else if (consumedmovement == Vector2Int.zero)
        {
            return true;
        }
        else {

            return cell.TryGetCellFromMovement(consumedmovement, ref cell);
        }
    }

    public List<Cell> GetCellsFromMovements(Vector2Int[] movements)
    {
        List<Cell> cells = new List<Cell>();

        Cell cell = null;
        for (int i = 0; i < movements.Length; i++)
        {
            Vector2Int movement = movements[i];
            if (TryGetCellFromMovement(movement, ref cell))
            {
                cells.Add(cell);
            }
        }

        return cells;
    }

    public void SetPiece(ChessPiece chessPiece, bool notify = true)
    {
        Model.chessPiece = chessPiece;

        if (notify)
        {
            SetOwner?.Invoke(Pos, this);
        }
    }

    public void OnSwapPieces(CellModel other)
    {
        SwapPieces(other);

        PassTurn();
    }

    void OnSetOwner(ChessPiece chessPiece)
    {
        Model.chessPiece = chessPiece;
        this.SetOwner?.Invoke(this.Pos, this);
        PassTurn();
    }

    private static void PassTurn()
    {
        if (ServiceLocator.GetGameService<TurnSystem>(out TurnSystem turnSystem))
        {
            turnSystem.PassTurn();
        }
    }
    
    public void SwapPieces(CellModel other)
    {
        ChessPiece aux = this.Model.chessPiece;

        this.SetPiece(other.chessPiece, false);
        other.chessPiece = aux;

        this.SetOwner?.Invoke(this.Pos, this);
    }
}
