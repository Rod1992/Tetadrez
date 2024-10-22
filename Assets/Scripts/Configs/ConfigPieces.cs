//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConfigPieces : ScriptableObject
{
    [SerializeField]
    List<ChessPieceScriptable> chessPieces;

    public List<ChessPieceScriptable> ChessPieces
    {
        get => chessPieces;
    }
}
