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
