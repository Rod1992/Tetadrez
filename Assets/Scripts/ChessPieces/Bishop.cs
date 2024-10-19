using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PieceMovement
{
    public class Bishop : IChessPieceMovement
    {
        public Vector2Int[] GetPossibleMovements(BoardConfig boardConfig)
        {
            int maxAxis = Mathf.Max(boardConfig.GridSize.x, boardConfig.GridSize.y);

            List<Vector2Int> movements = new List<Vector2Int>();

            for (int x = 1; x < maxAxis; x++)
            {
                movements.Add(new Vector2Int(x, x));
                movements.Add(new Vector2Int(-x, x));
                movements.Add(new Vector2Int(x, -x));
                movements.Add(new Vector2Int(-x, -x));
            }

            return movements.ToArray();
        }
    }
}
