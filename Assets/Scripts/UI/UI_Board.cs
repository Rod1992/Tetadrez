//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_Board : MonoBehaviour, IGameService
    {
        public static Action OnBoardChanged;

        [SerializeField]
        UI_Cell prefabCell;
        [SerializeField]
        RectTransform board;
        [SerializeField]
        Image boardImage;
        [SerializeField]
        BoardConfig boardConfig;

        [SerializeField]
        UI_Cell[,] cells;

        public UI_Cell[,] Cells => cells;

        public BoardConfig BoardConfig
        {
            get => boardConfig;
        }

        void Awake()
        {
            ResizeCells(false);
            ServiceLocator.AddService<UI_Board>(this);
        }

        private void ResizeCells(bool isPooled)
        {
            if (!isPooled)
            {
                cells = new UI_Cell[boardConfig.GridSize.x, boardConfig.GridSize.y];
            }

            float widthBoard = board.rect.width;
            float heightBoard = board.rect.height;

            float widthCell = widthBoard / boardConfig.GridSize.x;
            float heightCell = heightBoard / boardConfig.GridSize.y;

            float startCenterY = (heightCell - heightBoard) / 2;
            float centerX = (widthCell - widthBoard) / 2;
            float centerY = startCenterY;

            UI_Cell cell;
            for (int i = 0; i < boardConfig.GridSize.x; i++)
            {
                for (int j = 0; j < boardConfig.GridSize.y; j++)
                {
                    if (!isPooled)
                    {
                        cell = Instantiate<UI_Cell>(prefabCell, this.transform);
                        cells[i, j] = cell;
                    }

                    cells[i, j].SetSize(widthCell, heightCell, new Vector2(centerX, centerY), new Vector2Int(i, j));
                    centerY += heightCell;
                }
                centerX += widthCell;
                centerY = startCenterY;
            }
        }
    }
}
