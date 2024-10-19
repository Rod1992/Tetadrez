using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PieceMovement
{
    public class Knight : IChessPieceMovement
    {
        /// <summary>
        /// Possible Knight movements are static and we can avoid calculating them
        /// or allocating them each time we try to get the knight movement
        /// </summary>
        static readonly Vector2Int[] movements = new Vector2Int[] {
            new Vector2Int(2, 1),
            new Vector2Int(-2, 1),
            new Vector2Int(2, -1),
            new Vector2Int(-2, -1),
            new Vector2Int(1, 2),
            new Vector2Int(-1, 2),
            new Vector2Int(1, -2),
            new Vector2Int(-1, -2)
    };

        public Vector2Int[] GetPossibleMovements(BoardConfig boardConfig)
        {
            return movements;
        }
    }
}
