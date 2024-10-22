//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PieceMovement
{
    public class Rook : IChessPieceMovement
    {
        static List<Movement> movements;

        public Movement[] GetPossibleMovements(BoardConfig boardConfig)
        {
            if(movements == null)
            {
                movements = new List<Movement>();

                ChessPieceMovementFunctions.AddPaths(boardConfig, new Vector2Int(1, 0), new List<Vector2Int>(), movements);
                ChessPieceMovementFunctions.AddPaths(boardConfig, new Vector2Int(-1, 0), new List<Vector2Int>(), movements);
                ChessPieceMovementFunctions.AddPaths(boardConfig, new Vector2Int(0, 1), new List<Vector2Int>(), movements);
                ChessPieceMovementFunctions.AddPaths(boardConfig, new Vector2Int(0, -1), new List<Vector2Int>(), movements);

            }

            return movements.ToArray();
        }
    }
}
