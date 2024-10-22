//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum EUIChessPieceMode
    {
        None,
        Dragging,
        Grid
    }

    public class UI_ChessPiece : MonoBehaviour
    {
        public class HandlerDragging
        {
            static HandlerDragging currentHandler;

            ChessPiece piece;

            private HandlerDragging(ChessPiece chessPiece)
            {
                piece = chessPiece;
                piece.ViewChessPiece.Highlight(true);
            }

            public static void StartDragging(ChessPiece chessPiece)
            {
                EndDragging();
                currentHandler = new HandlerDragging(chessPiece);
            }

            public static bool TryGetSelection(out ChessPiece chessPiece)
            {
                chessPiece = currentHandler?.piece;
                return currentHandler != null && currentHandler.piece != null;
            }

            public static void EndDragging()
            {
                currentHandler?.piece.ViewChessPiece.EndDragging();
                currentHandler?.piece.ViewChessPiece.Highlight(false);
                currentHandler = null;
            }
        }

        public class HandlerSelectGrid
        {
            static HandlerSelectGrid currentHandler;

            ChessPiece piece;
            CellModel selectedCell;

            private HandlerSelectGrid(ChessPiece chessPiece, CellModel selected)
            {
                piece = chessPiece;
                selectedCell = selected;
                piece.ViewChessPiece.Highlight(true);
            }

            public static void StartSelection(ChessPiece chessPiece, CellModel selected)
            {
                EndSelection();
                currentHandler = new HandlerSelectGrid(chessPiece, selected);
            }

            public static bool TryGetSelection(out ChessPiece chessPiece, out CellModel cell)
            {
                chessPiece = currentHandler?.piece;
                cell = currentHandler?.selectedCell;
                return currentHandler != null && currentHandler.piece != null;
            }

            public static void EndSelection()
            {
                currentHandler?.piece.ViewChessPiece.Highlight(false);
                currentHandler = null;
            }
        }

        public static EUIChessPieceMode currentMode = EUIChessPieceMode.None;
        public Action OnStartDragging;

        bool isDragging = false;
        bool isActive = false;
        bool isDocked = false;

        public bool CanBeDragged
        {
            get => isActive && !isDocked;
        }

        Coroutine draggingCoroutine;

        [SerializeField]
        Image image;

        [SerializeField]
        Image button;

        private Color defaultColor;

        public void SetColor(Color color)
        {
            defaultColor = color;
            image.color = color;
        }

        public void Highlight(bool isHighlighted)
        {
            if (isHighlighted)
            {
                image.color = Color.red;
            }
            else
            {
                SetColor(defaultColor);
            }
        }

        public void Select()
        {
            if(UI_ChessPiece.currentMode == EUIChessPieceMode.Dragging && CanBeDragged)
            {
                if (!isDragging)
                {
                    StartDragging();
                }
                else
                {
                    EndDragging();
                }
            }
        }

        private void StartDragging()
        {
            isDragging = true;
            OnStartDragging?.Invoke();
            button.raycastTarget = false;
            draggingCoroutine = StartCoroutine(OnDrag());
        }

        private void EndDragging()
        {
            isDragging = false;
            button.raycastTarget = true;
            if (draggingCoroutine != null)
                StopCoroutine(draggingCoroutine);
        }

        private IEnumerator OnDrag()
        {
            int i = 0;
            while (i < int.MaxValue)
            {
                yield return null;
                this.transform.position = Input.mousePosition;
                i++;
            }

            EndDragging();
        }

        public void SetActive(bool active)
        {
            isActive = active;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docked">true if docked to a cell</param>
        public void SetDocked(bool docked)
        {
            isDocked = docked;
            button.raycastTarget = !docked;
        }

        public void SetGridMode()
        {
            isDragging = false;
            SetDocked(true);
            SetActive(false);
        }

        public void Reset()
        {
            isDragging = false;
            SetActive(true);
            SetDocked(false);
        }
    }
}
