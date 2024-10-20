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
        public class HandlerDraggingChessPiece
        {
            public static HandlerDraggingChessPiece currentHandler;

            UI_ChessPiece piece;

            private HandlerDraggingChessPiece(UI_ChessPiece chessPiece)
            {
                piece = chessPiece;
            }

            public static void StartDragging(UI_ChessPiece chessPiece)
            {
                EndDragging();
                currentHandler = new HandlerDraggingChessPiece(chessPiece);
            }

            public static bool TryGetSelection(out UI_ChessPiece chessPiece)
            {
                chessPiece = currentHandler?.piece;
                return currentHandler != null && currentHandler.piece != null;
            }

            public static void EndDragging()
            {
                currentHandler?.piece.EndDragging();
                currentHandler = null;
            }
        }

        public static EUIChessPieceMode currentMode = EUIChessPieceMode.None;

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

        public void SetColor(Color color)
        {
            image.color = color;
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
            Debug.Log("Selected");
            HandlerDraggingChessPiece.StartDragging(this);
            button.raycastTarget = false;
            draggingCoroutine = StartCoroutine(OnDrag());
        }

        private void EndDragging()
        {
            isDragging = false;
            HandlerDraggingChessPiece.currentHandler = null;
            
            if (draggingCoroutine != null)
                StopCoroutine(draggingCoroutine);
            Debug.Log("EndDragging");
        }

        private IEnumerator OnDrag()
        {
            Debug.Log("Dragging");
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

        public void SetDocked(bool docked)
        {
            isDocked = docked;
        }
    }
}
