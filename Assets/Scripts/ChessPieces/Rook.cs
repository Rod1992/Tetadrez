using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PieceMovement
{
    public class Rook : IChessPieceMovement
    {
        public Vector2Int[] GetPossibleMovements(BoardConfig boardConfig)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            //left/right
            for (int x = 1; x < boardConfig.GridSize.x; x++)
            {
                movements.Add(new Vector2Int(x, 0));
                movements.Add(new Vector2Int(-x, 0));
            }
            //up/down
            for (int y = 1; y < boardConfig.GridSize.y; y++)
            {
                movements.Add(new Vector2Int(0, y));
                movements.Add(new Vector2Int(0, -y));
            }

            return movements.ToArray();
        }
    }
}
