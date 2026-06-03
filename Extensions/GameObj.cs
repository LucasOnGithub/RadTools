using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RadTools.Extensions;

public static class GameObj
{
    internal static readonly Dictionary<Type, object[]> typePool = new();
    private static readonly Dictionary<Type, float> receiveTypeDelay = new();

    internal static void Destroy(this GameObject obj, float delay = 0f)
    {
        Object.Destroy(obj, delay);
    }

    internal static void DestroyImmediate(this GameObject obj)
    {
        Object.DestroyImmediate(obj);
    }

    internal static void DontDestroyOnLoad(this GameObject obj)
    {
        Object.DontDestroyOnLoad(obj);
    }

    /// <summary>
    ///     Gets all objects of type <typeparamref name="T" /> in the scene using
    ///     <see cref="UnityEngine.Object.FindObjectsOfType{T}" />
    ///     with caching to reduce performance overhead.
    /// </summary>
    /// <typeparam name="T">The Unity object type to find</typeparam>
    /// <param name="decayTime">How long the cached results remain valid</param>
    /// <returns>An array of all instances of <typeparamref name="T" /></returns>
    public static T[] GetAllType<T>(float decayTime = 5f) where T : Object
    {
        var type = typeof(T);

        var lastReceivedTime = receiveTypeDelay.GetValueOrDefault(type, -1f);

        if (Time.time > lastReceivedTime)
        {
            typePool.Remove(type);
            receiveTypeDelay[type] = Time.time + decayTime;
        }

        if (!typePool.ContainsKey(type))
            typePool.Add(type, Object.FindObjectsOfType<T>());

        return (T[])typePool[type];
    }
}