using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_Board : MonoBehaviour, IGameService
    {
        [SerializeField]
        UI_Cell prefabCell;
        [SerializeField]
        RectTransform board;
        [SerializeField]
        Image boardImage;
        [SerializeField]
        BoardConfig boardConfig;

        [SerializeField]
        List<UI_Cell> cells;

        private void Start()
        {
            ResizeCells(false);
            ServiceLocator.AddService<UI_Board>(this);
        }

        private void ResizeCells(bool isPooled)
        {
            if (!isPooled)
            {
                cells = new List<UI_Cell>();
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
                    if (isPooled)
                    {
                        cell = cells[(i * boardConfig.GridSize.x) + j];
                    }
                    else
                    {
                        cell = Instantiate<UI_Cell>(prefabCell, this.transform);
                        cells.Add(cell);
                    }

                    cell.SetSize(widthCell, heightCell, new Vector2(centerX, centerY));
                    centerY += heightCell;
                }
                centerX += widthCell;
                centerY = startCenterY;
            }
        }

        /*private void OnRectTransformDimensionsChange()
        {
            Debug.Log("Board Size Changed");
            if(cells.Count > 0)
                ResizeCells(true);
        }*/
    }
}
