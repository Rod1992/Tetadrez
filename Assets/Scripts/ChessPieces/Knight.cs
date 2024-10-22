//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
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
        static readonly Vector2Int[] destinations = new Vector2Int[] {
            new Vector2Int(2, 1),
            new Vector2Int(-2, 1),
            new Vector2Int(2, -1),
            new Vector2Int(-2, -1),
            new Vector2Int(1, 2),
            new Vector2Int(-1, 2),
            new Vector2Int(1, -2),
            new Vector2Int(-1, -2)
        };

        static Movement[] movements;

        public Movement[] GetPossibleMovements(BoardConfig boardConfig)
        {
            if(movements == null)
            {
                movements = new Movement[destinations.Length];
                Vector2Int[] emptypath = new Vector2Int[0];

                for (int i = 0; i < destinations.Length; i++)
                {
                    Vector2Int vector = destinations[i];
                    movements[i] = new Movement(vector, emptypath, false);
                }
            }
           
            return movements;
        }
    }
}
