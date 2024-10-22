//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UI_PlayerPanel : MonoBehaviour
    {
        [SerializeField]
        private int playerIndex;

        [SerializeField]
        private List<GameObject> spawners;

        public int PlayerIndex { get => playerIndex;}

        void Awake()
        {
            Container<UI_PlayerPanel>.AddItem(this);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPieceToPosition(UI_ChessPiece piece, int index)
        {
            piece.transform.SetParent(this.transform);

            if(spawners.Count > index && index > -1)
            {
                piece.transform.position = spawners[index].transform.position;
            }
        }
    }
}
