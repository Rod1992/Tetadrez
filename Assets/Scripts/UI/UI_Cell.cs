//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UI_Cell : MonoBehaviour
    {
        /// <summary>
        /// returns pos of cell
        /// </summary>
        public Action<ChessPiece> SetOwner;
        public Action<CellModel> OnSwap;
        public Action OnStartSelection;

        public Vector2Int Pos { get; private set; }

        CellModel Model
        {
            get
            {
                ServiceLocator.GetGameService<CellModels>(out CellModels models);

                return models.Get(Pos);
            }
        }

        [SerializeField]
        RectTransform rectTransform;

        public void SetSize(float width, float height, Vector2 pos, Vector2Int gridPos)
        {
            Pos = gridPos;
            rectTransform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0, 0, 0));
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        public void Select()
        {
            if (UI_ChessPiece.currentMode == EUIChessPieceMode.Dragging && Model.chessPiece == null && UI_ChessPiece.HandlerDragging.TryGetSelection(out ChessPiece newChessPiece))
            {
                OnDraggingSelection(newChessPiece);
            }
            else if (UI_ChessPiece.currentMode == EUIChessPieceMode.Grid)
            {
                OnGridSelection();
            }
        }

        private void OnDraggingSelection(ChessPiece newChessPiece)
        {
            SetOwner?.Invoke(newChessPiece);
            Model.chessPiece.ViewChessPiece.transform.position = this.transform.position;
            UI_ChessPiece.HandlerDragging.EndDragging();
            Model.chessPiece.ViewChessPiece.SetDocked(true);
        }

        private void OnGridSelection()
        {
            ChessPiece selected;
            bool hasSelection = UI_ChessPiece.HandlerSelectGrid.TryGetSelection(out selected, out CellModel other);
            if (Model.chessPiece == null && hasSelection)
            {
                if(ServiceLocator.GetGameService<BoardState>(out BoardState boardState))
                {
                    if (boardState.CanMoveFromTo(other, Model))
                    {
                        OnSwap?.Invoke(other);
                        Model.chessPiece.ViewChessPiece.transform.position = this.transform.position;
                        UI_ChessPiece.HandlerSelectGrid.EndSelection();
                    }
                    else
                    {
                        Debug.LogWarning("TriedIllegalMove");
                    }
                }
            }
            else if (Model.chessPiece != null && !hasSelection)
            {
                OnStartSelection?.Invoke();
                UI_ChessPiece.HandlerSelectGrid.StartSelection(Model.chessPiece, Model);
            }
            else if (Model.chessPiece == selected)
            {
                UI_ChessPiece.HandlerSelectGrid.EndSelection();
            }
        }
    }
}
