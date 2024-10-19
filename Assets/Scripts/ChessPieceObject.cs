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

[CreateAssetMenu]
public class ChessPieceObject : ScriptableObject
{
    [SerializeField]
    EChessPieceType chessPieceType;
    IChessPieceMovement movement;

    [SerializeField]
    UI_ChessPiece viewChessPiece;

    public UI_ChessPiece ViewChessPiece { get => viewChessPiece;}

    public void SetMovement()
    {
        switch (chessPieceType)
        {
            case EChessPieceType.Bishop:
                movement = new Bishop();
                break;
            case EChessPieceType.Rook:
                movement = new Rook();
                break;
            case EChessPieceType.Knight:
                movement = new Knight();
                break;
        }
    }

    public UI_ChessPiece CreateObject(Transform parent, Color color)
    {
        UI_ChessPiece piece = Instantiate<UI_ChessPiece>(viewChessPiece, parent);
        piece.SetColor(color);
        piece.transform.position = parent.transform.position;

        return piece;
    }
}
