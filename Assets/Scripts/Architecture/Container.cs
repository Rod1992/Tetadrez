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

    private void InternalFindItem(Predicate<T> predicate)
    {
        items.Find(predicate);
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

    public static void FindItem(Predicate<T> predicate)
    {
        Instance.InternalFindItem(predicate);
    }

    
}
