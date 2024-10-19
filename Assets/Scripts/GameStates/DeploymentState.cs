using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace GameStates
{
    public class DeploymentState : IGameState
    {
        Action OnEndState;
        ConfigPieces configPieces;

        public DeploymentState(Action OnEndState, ConfigPieces configPieces)
        {
            this.OnEndState = OnEndState;
            this.configPieces = configPieces;
        }

        public void OnPhaseEnded()
        {
           
        }

        public void OnPhaseStarted()
        {
            if (ServiceLocator.GetGameService<UI_Board>(out UI_Board ui_Board))
            {
                UI_ChessPiece piece;

                foreach (Player player in Container<Player>.GetItems())
                {
                    Color color = player.Color;
                    UI_PlayerPanel panel = Container<UI_PlayerPanel>.FindItem((panel) => panel.PlayerIndex == player.Id); 

                    foreach (ChessPieceObject pieceObject in configPieces.ChessPieces)
                    {
                        piece = pieceObject.CreateObject(panel.transform, color);
                        Container<UI_ChessPiece>.AddItem(piece);
                    }
                }
            }
        }
    }
}