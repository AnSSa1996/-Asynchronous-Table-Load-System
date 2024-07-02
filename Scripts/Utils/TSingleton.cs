using System;
using System.IO;
using System.Reflection;
using UnityEngine;

public abstract class TSingleton<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
                (_instance as TSingleton<T>).Initialize();
            }

            return _instance;
        }
    }

    protected virtual void Initialize()
    {
    }
}