//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using PieceMovement;
using UI;
using UnityEngine;

public enum EChessPieceType
{
    Bishop,
    Rook,
    Knight
}

public class ChessPiece
{
    public UI_ChessPiece ViewChessPiece { get; }
    public IChessPieceMovement Movement { get; }
    public Player Player { get; }

    public ChessPiece( UI_ChessPiece viewChessPiece, IChessPieceMovement movement, Player player)
    {
        ViewChessPiece = viewChessPiece;
        Movement = movement;
        Player = player;
    }

    public ChessPiece()
    {

    }
}

[CreateAssetMenu]
public class ChessPieceScriptable : ScriptableObject
{
    [SerializeField]
    EChessPieceType chessPieceType;

    [SerializeField]
    UI_ChessPiece viewChessPiece;

    public UI_ChessPiece ViewChessPiece { get => viewChessPiece;}

    public IChessPieceMovement GetMovement()
    {
        switch (chessPieceType)
        {
            case EChessPieceType.Bishop:
                return new Bishop();
            case EChessPieceType.Rook:
                return new Rook();
            case EChessPieceType.Knight:
                return new Knight();
        }

        return default;
    }

    public ChessPiece CreateObject(Transform parent, Player player)
    {
        UI_ChessPiece ui_Chess = Instantiate<UI_ChessPiece>(viewChessPiece, parent);
        ui_Chess.SetColor(player.Color);
        ui_Chess.transform.position = parent.transform.position;

        IChessPieceMovement movement = GetMovement();

        ChessPiece chessPiece = new ChessPiece(ui_Chess, movement, player);

        ui_Chess.OnStartDragging += () => StartDragging(chessPiece);
        return chessPiece;
    }

    void StartDragging(ChessPiece chessPiece)
    {
        UI_ChessPiece.HandlerDragging.StartDragging(chessPiece);
    }
}
