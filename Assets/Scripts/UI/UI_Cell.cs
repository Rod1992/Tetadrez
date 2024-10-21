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
        public Action<Vector2Int, Player> SetOwner;

        public Vector2Int Pos { get; private set; }

        [SerializeField]
        RectTransform rectTransform;

        ChessPiece chessPiece;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetSize(float width, float height, Vector2 pos, Vector2Int gridPos)
        {
            Pos = gridPos;
            rectTransform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0, 0, 0));
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        public void Select()
        {
            Debug.Log("SelectedCell");
            if (chessPiece == null && UI_ChessPiece.HandlerDraggingChessPiece.TryGetSelection(out chessPiece))
            {
                Debug.Log("Added ChessPiece to Cell");
                chessPiece.ViewChessPiece.transform.position = this.transform.position;
                UI_ChessPiece.HandlerDraggingChessPiece.EndDragging();
                chessPiece.ViewChessPiece.SetDocked(true);
                SetOwner?.Invoke(Pos, chessPiece.Player);

                if(ServiceLocator.GetGameService<TurnSystem>(out TurnSystem turnSystem))
                {
                    turnSystem.PassTurn();
                }
            }
        }
    }
}
