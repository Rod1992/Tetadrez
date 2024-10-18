using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    Cell up, down, left, right;

    public Cell(Cell up, Cell down, Cell left, Cell right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
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
            cell = movement.x > 0 ? right : left;
            
        } else if(movement.y != 0)
        {
            consumedmovement = new Vector2Int(movement.x, movement.y > 0 ? movement.y - 1 : movement.y + 1);
            cell = movement.y > 0 ? up : down;
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
