using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class BoardState : IGameService
{
    /// <summary>
    /// returns winning player
    /// </summary>
    public static Action<Player> OnGameOver;

    public BoardState()
    {
        UI_Board.OnBoardChanged += OnBoardChanged;
        ServiceLocator.AddService<BoardState>(this);
    }

    private void OnBoardChanged()
    {
        Debug.Log("Check Board State");

        if(ServiceLocator.GetGameService<UI_Board>(out UI_Board board))
        {

        }
    }
}
