using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EGamePhase
{
    None = - 1,
    ChooseStartingPlayer = 0,//Choose starting player
    Deployment = 1, //Place the pieces on the board
    MainGame = 2, // Play "Tic Tac Toe"
    GameOver = 3
}

public class Game
{
    EGamePhase currentGamePhase;
    IGameState currentState;

    public Game()
    {
        currentGamePhase = EGamePhase.None;
        NextPhase();
    }

    public void NextPhase()
    {
        OnPhaseEnded();
        currentGamePhase++;

        switch (currentGamePhase)
        {
            case EGamePhase.ChooseStartingPlayer:
                currentState = new ChooseStartingPlayerState(NextPhase);
                break;
            case EGamePhase.Deployment:
                break;
            case EGamePhase.MainGame:
                break;
            case EGamePhase.GameOver:
                break;
        }

        currentState.OnPhaseStarted();
    }

    public void OnPhaseEnded()
    {
        currentState?.OnPhaseEnded();
    }
}
