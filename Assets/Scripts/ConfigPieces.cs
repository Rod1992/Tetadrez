using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConfigPieces : ScriptableObject
{
    [SerializeField]
    List<ChessPieceObject> chessPieces;

    public List<ChessPieceObject> ChessPieces
    {
        get => chessPieces;
    }
}
