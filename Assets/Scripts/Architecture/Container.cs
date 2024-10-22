//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections.Generic;
using System;

/// <summary>
/// Singleton Container with "sealed" instance
/// </summary>
/// <typeparam name="T"></typeparam>
public class Container<T>
{
    List<T> items;

    static Container<T> instance;

    static Container<T> Instance {
        get
        {
            if (instance == null)
                instance = new Container<T>();
            return instance;
        }
    }

    public static T GetItem(int index)
    {
        if (index < Count && index > -1)
            return Instance.items[index];
        else
            return default(T);
    }

    public static int Count
    {
        get => Instance.items.Count;
    }

    private Container()
    {
        items = new List<T>();
    }

    private void InternalAddItem(T newItem)
    {
        items.Add(newItem);
    }

    private void InternalRemoveItem(T item)
    {
        items.Remove(item);
    }

    private void InternalClear()
    {
        items.Clear();
    }

    private T InternalFindItem(Predicate<T> predicate)
    {
        return items.Find(predicate);
    }

    private void InternalReverse()
    {
        items.Reverse();
    }

    public static void AddItem(T newItem)
    {
        Instance.InternalAddItem(newItem);
    }

    public static void RemoveItem(T item)
    {
        Instance.InternalRemoveItem(item);
    }

    public static void Clear()
    {
        Instance.InternalClear();
    }

    public static T FindItem(Predicate<T> predicate)
    {
        return Instance.InternalFindItem(predicate);
    }

    public static void Reverse()
    {
        Instance.InternalReverse();
    }

    public static T[] GetItems()
    {
        return Instance.items.ToArray();
    }

    public static T[] Filter(Predicate<T> match)
    {
        return Instance.items.FindAll(match).ToArray();
    }
}
