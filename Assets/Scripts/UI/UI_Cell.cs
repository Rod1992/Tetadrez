using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UI_Cell : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectTransform;

        [SerializeField]
        UI_ChessPiece chessPiece;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetSize(float width, float height, Vector2 pos)
        {
            rectTransform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0, 0, 0));
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }



        public void Select()
        {
            Debug.Log("SelectedCell");
            if (UI_ChessPiece.HandlerDraggingChessPiece.TryGetSelection(out chessPiece))
            {
                Debug.Log("Added ChessPiece to Cell");
                chessPiece.transform.position = this.transform.position;
                UI_ChessPiece.HandlerDraggingChessPiece.EndDragging();
                chessPiece.SetDocked(true);

                if(ServiceLocator.GetGameService<TurnSystem>(out TurnSystem turnSystem))
                {
                    turnSystem.PassTurn();
                }
            }
        }
    }
}
