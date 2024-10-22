//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : IGameService
{
    /// <summary>
    /// int is index player, Player is Active Player
    /// </summary>
    public static Action<int, Player> OnPassTurn;

    int indexActivePlayer;

    public Player ActivePlayer
    {
        get
        {
            return Container<Player>.GetItem(indexActivePlayer);
        }
    }

    public TurnSystem()
    {
        indexActivePlayer = 0;
        ServiceLocator.AddService<TurnSystem>(this);
        OnPassTurn?.Invoke(indexActivePlayer, ActivePlayer);
    }

    public void PassTurn()
    {
        if(Container<Player>.Count <= ++indexActivePlayer)
        {
            indexActivePlayer = 0;
        }
        OnPassTurn?.Invoke(indexActivePlayer, ActivePlayer);
        Debug.Log("Pass Turn");
    }

}
