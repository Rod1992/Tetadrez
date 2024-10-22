//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModelProvider<T>
{
    public void AddItem(T item);

    public void RemoveItem(T item);
}
