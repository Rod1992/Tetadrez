//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name { get; private set; }
    public int Id { get; private set; }
    public Color Color { get; set; }

    public Player(string name, Color color, int id)
    {
        this.Name = name;
        this.Color = color;
        this.Id = id;
    }


}
