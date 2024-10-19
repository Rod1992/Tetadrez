using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UI_PlayerPanel : MonoBehaviour
    {
        [SerializeField]
        private int playerIndex;

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
    }
}
