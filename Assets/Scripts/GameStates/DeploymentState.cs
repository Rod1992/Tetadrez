//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
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
            Debug.Log("Ended Deployment");
            TurnSystem.OnPassTurn -= OnPassTurn;
        }

        public void OnPhaseStarted()
        {
            SpawnPieces();
            UI_ChessPiece.currentMode = EUIChessPieceMode.Dragging;
            StartTurnSystem();
        }

        private void SpawnPieces()
        {
            if (ServiceLocator.GetGameService<UI_Board>(out UI_Board ui_Board))
            {
                if (Container<ChessPiece>.Count == 0)
                {
                    InitPieces();
                }
                else
                {
                    RepoolPieces();
                }
            }
        }

        private static void RepoolPieces()
        {
            foreach (Player player in Container<Player>.GetItems())
            {
                UI_PlayerPanel panel = Container<UI_PlayerPanel>.FindItem((panel) => panel.PlayerIndex == player.Id);

                int index = 0;

                foreach (ChessPiece cPiece in Container<ChessPiece>.GetItems())
                {
                    if (cPiece.Player == player)
                    {
                        cPiece.ViewChessPiece.Reset();
                        panel.SetPieceToPosition(cPiece.ViewChessPiece, index);
                        index++;
                    }
                }
            }
        }

        private void InitPieces()
        {
            ChessPiece piece;
            foreach (Player player in Container<Player>.GetItems())
            {
                Color color = player.Color;
                UI_PlayerPanel panel = Container<UI_PlayerPanel>.FindItem((panel) => panel.PlayerIndex == player.Id);

                int index = 0;

                foreach (ChessPieceScriptable pieceObject in configPieces.ChessPieces)
                {
                    piece = pieceObject.CreateObject(panel.transform, player);
                    panel.SetPieceToPosition(piece.ViewChessPiece, index);
                    Container<ChessPiece>.AddItem(piece);
                    index++;
                }
            }
        }

        private void StartTurnSystem()
        {
            if(ServiceLocator.GetGameService<BoardState>(out BoardState boardState))
            {
                boardState.ResetBoardState();
            }else
                boardState = new BoardState();

            TurnSystem.OnPassTurn += OnPassTurn;

            if(ServiceLocator.GetGameService<TurnSystem>(out TurnSystem turnSystem))
            {
                turnSystem.PassTurn();
            }
            else
            {
                turnSystem = new TurnSystem();
            }
        }

        private void OnPassTurn(int indexPlayer, Player player)
        {
            foreach (ChessPiece chessPiece in Container<ChessPiece>.GetItems())
            {
                chessPiece.ViewChessPiece.SetActive(chessPiece.Player == player);
            }

            ChessPiece draggableItem = Container<ChessPiece>.FindItem((x) => x.ViewChessPiece.CanBeDragged);

            //If we can't find a draggable Item, it means that we have ended the Deployment State
            if (draggableItem == null)
            {
                OnEndState?.Invoke();
            }
        }
    }
}