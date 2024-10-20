using System.Collections;
using System.Collections.Generic;
using GameStates;
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
    ConfigPieces configPieces;

    public Game(ConfigPieces configPieces)
    {
        currentGamePhase = EGamePhase.None;
        this.configPieces = configPieces;
        NextPhase();
    }

    public void ForcePhase(EGamePhase gamePhase)
    {
        OnPhaseEnded();
        currentGamePhase = gamePhase;
        SetPhase();
    }

    public void NextPhase()
    {
        OnPhaseEnded();
        currentGamePhase++;
        SetPhase();
    }

    private void SetPhase()
    {
        switch (currentGamePhase)
        {
            case EGamePhase.ChooseStartingPlayer:
                currentState = new ChooseStartingPlayerState(NextPhase);
                break;
            case EGamePhase.Deployment:
                BoardState.OnGameOver += OnGameOver;
                currentState = new DeploymentState(NextPhase, configPieces);
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

    public void OnGameOver(Player player)
    {
        Debug.Log("Game Over");
        ForcePhase(EGamePhase.GameOver);
    }
}
