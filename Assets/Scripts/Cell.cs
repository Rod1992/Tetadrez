using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class Cell
{
    UI_Cell ui_Cell;

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
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movement"></param>
    /// <param name="cell"></param>
    /// <returns>true if cell found</returns>
    public bool TryGetCellFromMovement(Vector2Int movement, out Cell cell)
    {
        Vector2Int consumedmovement = Vector2Int.zero;
        cell = null;

        if (movement.x != 0)
        {
            consumedmovement = new Vector2Int(movement.x > 0 ? movement.x - 1 : movement.x + 1, movement.y);
            cell = movement.x > 0 ? Right : Left;
            
        } else if(movement.y != 0)
        {
            consumedmovement = new Vector2Int(movement.x, movement.y > 0 ? movement.y - 1 : movement.y + 1);
            cell = movement.y > 0 ? Up : Down;
        }

        if (consumedmovement == Vector2Int.zero)
        {
            cell = this;
            return true;
        }
        else if(cell == null)
        {
            return false;
        } else {
            
            return TryGetCellFromMovement(consumedmovement, out cell);
        }
    }

    public List<Cell> GetCellsFromMovements(Vector2Int[] movements)
    {
        List<Cell> cells = new List<Cell>();

        Cell cell;
        for(int i = 0; i < movements.Length; i++)
        {
            Vector2Int movement = movements[i];
            if(TryGetCellFromMovement(movement, out cell))
            {
                cells.Add(cell);
            }
        }

        return cells;
    }
}
