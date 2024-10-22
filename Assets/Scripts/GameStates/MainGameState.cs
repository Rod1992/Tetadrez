//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace GameStates
{
    public class MainGameState : IGameState
    {
        Action OnEndState;

        public MainGameState(Action OnEndState)
        {
            OnEndState += OnEndState;
        }

        public void OnPhaseEnded()
        {
            TurnSystem.OnPassTurn -= OnPassTurn;
        }

        public void OnPhaseStarted()
        {
            UI_ChessPiece.currentMode = EUIChessPieceMode.Grid;
            TurnSystem.OnPassTurn += OnPassTurn;
            UI_ChessPiece.HandlerDragging.EndDragging();

            foreach(ChessPiece chessPiece in Container<ChessPiece>.GetItems())
            {
                chessPiece.ViewChessPiece.SetGridMode();
            }

            if(ServiceLocator.GetGameService<TurnSystem>(out TurnSystem turnSystem))
                OnPassTurn(0, turnSystem.ActivePlayer);
        }

        private void OnPassTurn(int indexPlayer, Player player)
        {
            if(ServiceLocator.GetGameService<BoardState>(out BoardState boardState))
            {
                if (!boardState.CanPlayerMove(player) && ServiceLocator.GetGameService<TurnSystem>(out TurnSystem turnSystem))
                {
                    turnSystem.PassTurn();
                }
                else
                {
                    boardState.SetActiveCells(player);
                }
            }
        }
    }
}
