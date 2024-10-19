using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseStartingPlayerState : IGameState
{
    Action OnEndState;

    public ChooseStartingPlayerState(Action OnEndState)
    {
        this.OnEndState = OnEndState;
    }

    public void OnPhaseEnded()
    {
        OnEndState?.Invoke();
    }

    public void OnPhaseStarted()
    {
        Player white = new Player();
        Player black = new Player();
        Container<Player>.AddItem(white);
        Container<Player>.AddItem(black);

        int numberPlayers = Container<Player>.Count;

        int startingPlayer = UnityEngine.Random.Range(1, numberPlayers + 1);

        Debug.Log("Starting Player is :" + startingPlayer);
    }
}
