using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates
{
    public class ChooseStartingPlayerState : IGameState
    {
        Action OnEndState;

        public ChooseStartingPlayerState(Action OnEndState)
        {
            this.OnEndState = OnEndState;
        }

        public void OnPhaseEnded()
        {
        }

        public void OnPhaseStarted()
        {
            Player white = new Player(Color.white, 0);
            Player black = new Player(Color.black, 1);
            Container<Player>.AddItem(white);
            Container<Player>.AddItem(black);

            int numberPlayers = Container<Player>.Count;

            int startingPlayer = UnityEngine.Random.Range(1, numberPlayers + 1);

            if(startingPlayer == 2)
            {
                Container<Player>.Reverse();
            }

            Debug.Log("Starting Player is :" + startingPlayer);
            OnEndState?.Invoke();
        }
    }
}
