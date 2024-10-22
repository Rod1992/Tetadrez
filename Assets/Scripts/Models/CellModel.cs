//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellModel
{
    public Vector2Int Pos;

    public ChessPiece chessPiece;

    public Player Player
    {
        get => chessPiece?.Player;
    }

    public CellModel(Vector2Int Pos)
    {
        this.Pos = Pos;
    }
}
