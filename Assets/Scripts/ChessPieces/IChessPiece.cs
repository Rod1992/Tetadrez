using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChessPieceMovement
{
    public Vector2Int[] GetPossibleMovements(BoardConfig boardConfig);
}
