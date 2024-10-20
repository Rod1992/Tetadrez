using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int Id { get; private set; }
    public Color Color { get; set; }

    public Player(Color color, int id)
    {
        this.Color = color;
        this.Id = id;
    }


}
