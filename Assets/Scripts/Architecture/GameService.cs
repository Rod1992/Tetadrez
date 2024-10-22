//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    static List<IGameService> gameServices;

    static ServiceLocator()
    {
        gameServices = new List<IGameService>();
    }

    public static bool GetGameService<T>(out T service) where T : IGameService
    {
        service = (T)gameServices.Find((s) => s is T);
        return service != null;
    }

    public static void AddService<T>(IGameService service) where T : IGameService
    {
        if(!GetGameService<T>(out T dummy))
        {
            gameServices.Add(service);
        }
    }
}

public interface IGameService
{
    

    
}
