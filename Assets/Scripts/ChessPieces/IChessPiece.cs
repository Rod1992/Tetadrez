//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PieceMovement
{
    public struct Movement
    {
        public Vector2Int Destination;
        public bool Collision;
        /// <summary>
        /// Does not include destination
        /// </summary>
        public Vector2Int[] Path;

        public Movement(Vector2Int destination, Vector2Int[] path, bool collision)
        {
            this.Destination = destination;
            this.Path = path;
            this.Collision = collision;
        }
    }

    public interface IChessPieceMovement
    {
        public Movement[] GetPossibleMovements(BoardConfig boardConfig);
    }

    public static class ChessPieceMovementFunctions
    {
        public static void AddPaths(BoardConfig boardConfig, Vector2Int dir, List<Vector2Int> path, List<Movement> movements)
        {
            if (Mathf.Abs(dir.x) < boardConfig.GridSize.x && Mathf.Abs(dir.y) < boardConfig.GridSize.y)
            {
                Movement move = new Movement(dir, path.ToArray(), true);
                movements.Add(move);
                path.Add(dir);
                dir += dir;
                AddPaths(boardConfig, dir, path, movements);
            }
        }
    }
}
