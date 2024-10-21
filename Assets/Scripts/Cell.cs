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

    public UI_Cell UI_Cell
    {
        get => ui_Cell;
    }

    public Player Player
    {
        get;
        set;
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
        ui_Cell.SetOwner += SetOwner;
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
        } else if(movement.y != 0)
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
        else{
            
            return cell.TryGetCellFromMovement(consumedmovement, ref cell);
        }
    }

    public List<Cell> GetCellsFromMovements(Vector2Int[] movements)
    {
        List<Cell> cells = new List<Cell>();

        Cell cell = null;
        for(int i = 0; i < movements.Length; i++)
        {
            Vector2Int movement = movements[i];
            if(TryGetCellFromMovement(movement, ref cell))
            {
                cells.Add(cell);
            }
        }

        return cells;
    }

    public void SetOwner(Vector2Int pos, Player player)
    {
        Player = player;
    }
}
