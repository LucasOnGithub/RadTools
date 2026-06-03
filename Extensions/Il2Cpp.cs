using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;

namespace RadTools.Extensions;

public static class Il2Cpp
{
    // -------------------------------------------------------------------------
    // object[] <-> Il2CppReferenceArray<Il2CppSystem.Object>
    // -------------------------------------------------------------------------

    public static Il2CppReferenceArray<Object> ToIl2CppArray(this object[] source)
    {
        if (source == null) return null;

        var result = new Il2CppReferenceArray<Object>(source.Length);
        for (var i = 0; i < source.Length; i++)
            result[i] = source[i] as Object;

        return result;
    }

    public static object[] ToObjectArray(this Il2CppReferenceArray<Object> source)
    {
        if (source == null) return null;

        var result = new object[source.Length];
        for (var i = 0; i < source.Length; i++)
            result[i] = source[i];

        return result;
    }

    // -------------------------------------------------------------------------
    // object[] <-> Il2CppSystem.Array
    // -------------------------------------------------------------------------

    public static Array ToIl2CppSystemArray(this object[] source)
    {
        if (source == null) return null;

        var result = new Il2CppReferenceArray<Object>(source.Length);
        for (var i = 0; i < source.Length; i++)
            result[i] = source[i] as Object;

        return new Array(result.Pointer);
    }

    public static object[] ToObjectArray(this Array source)
    {
        if (source == null) return null;

        var len = source.Length;
        var result = new object[len];
        for (var i = 0; i < len; i++)
            result[i] = source.GetValue(i);

        return result;
    }

    // -------------------------------------------------------------------------
    // List<T> <-> Il2CppSystem.Collections.Generic.List<T>
    // -------------------------------------------------------------------------

    /// <summary>
    ///     Converts a managed List&lt;T&gt; to an Il2CppSystem.Collections.Generic.List&lt;T&gt;.
    /// </summary>
    public static List<T> ToIl2CppList<T>(this System.Collections.Generic.List<T> source)
        where T : Object
    {
        if (source == null) return null;

        var result = new List<T>(source.Count);
        foreach (var item in source)
            result.Add(item);

        return result;
    }

    /// <summary>
    ///     Converts an Il2CppSystem.Collections.Generic.List&lt;T&gt; to a managed List&lt;T&gt;.
    /// </summary>
    public static System.Collections.Generic.List<T> ToManagedList<T>(this List<T> source)
        where T : Object
    {
        if (source == null) return null;

        var result = new System.Collections.Generic.List<T>(source.Count);
        foreach (var item in source)
            result.Add(item);

        return result;
    }

    // -------------------------------------------------------------------------
    // List<string> <-> Il2CppSystem.Collections.Generic.List<string>
    //   Strings are not Il2CppSystem.Object so they need a dedicated overload.
    // -------------------------------------------------------------------------

    /// <summary>
    ///     Converts a managed List&lt;string&gt; to an Il2CppSystem.Collections.Generic.List&lt;string&gt;.
    /// </summary>
    public static List<string> ToIl2CppList(this System.Collections.Generic.List<string> source)
    {
        if (source == null) return null;

        var result = new List<string>(source.Count);
        foreach (var item in source)
            result.Add(item);

        return result;
    }

    /// <summary>
    ///     Converts an Il2CppSystem.Collections.Generic.List&lt;string&gt; to a managed List&lt;string&gt;.
    /// </summary>
    public static System.Collections.Generic.List<string> ToManagedList(this List<string> source)
    {
        if (source == null) return null;

        var result = new System.Collections.Generic.List<string>(source.Count);
        foreach (var item in source)
            result.Add(item);

        return result;
    }

    // -------------------------------------------------------------------------
    // List<object> <-> Il2CppSystem.Collections.Generic.List<Il2CppSystem.Object>
    // -------------------------------------------------------------------------

    /// <summary>
    ///     Converts a managed List&lt;object&gt; to an Il2CppSystem.Collections.Generic.List&lt;Il2CppSystem.Object&gt;.
    /// </summary>
    public static List<Object> ToIl2CppObjectList(this System.Collections.Generic.List<object> source)
    {
        if (source == null) return null;

        var result = new List<Object>(source.Count);
        foreach (var item in source)
            result.Add(item as Object);

        return result;
    }

    /// <summary>
    ///     Converts an Il2CppSystem.Collections.Generic.List&lt;Il2CppSystem.Object&gt; to a managed List&lt;object&gt;.
    /// </summary>
    public static System.Collections.Generic.List<object> ToObjectList(this List<Object> source)
    {
        if (source == null) return null;

        var result = new System.Collections.Generic.List<object>(source.Count);
        foreach (var item in source)
            result.Add(item);

        return result;
    }

    // -------------------------------------------------------------------------
    // Il2CppReferenceArray<T> <-> List<T>
    // -------------------------------------------------------------------------

    /// <summary>
    ///     Converts a managed List&lt;T&gt; to an Il2CppReferenceArray&lt;T&gt;.
    /// </summary>
    public static Il2CppReferenceArray<T> ToIl2CppReferenceArray<T>(this System.Collections.Generic.List<T> source)
        where T : Object
    {
        if (source == null) return null;

        var result = new Il2CppReferenceArray<T>(source.Count);
        for (var i = 0; i < source.Count; i++)
            result[i] = source[i];

        return result;
    }

    /// <summary>
    ///     Converts an Il2CppReferenceArray&lt;T&gt; to a managed List&lt;T&gt;.
    /// </summary>
    public static System.Collections.Generic.List<T> ToManagedList<T>(this Il2CppReferenceArray<T> source)
        where T : Object
    {
        if (source == null) return null;

        var result = new System.Collections.Generic.List<T>(source.Length);
        for (long i = 0; i < source.Length; i++)
            result.Add(source[(int)i]);

        return result;
    }

    // -------------------------------------------------------------------------
    // Il2CppReferenceArray<T> <-> Il2CppSystem.Collections.Generic.List<T>
    // -------------------------------------------------------------------------

    /// <summary>
    ///     Converts an Il2CppReferenceArray&lt;T&gt; to an Il2CppSystem.Collections.Generic.List&lt;T&gt;.
    /// </summary>
    public static List<T> ToIl2CppList<T>(this Il2CppReferenceArray<T> source)
        where T : Object
    {
        if (source == null) return null;

        var result = new List<T>(source.Length);
        for (long i = 0; i < source.Length; i++)
            result.Add(source[(int)i]);

        return result;
    }

    /// <summary>
    ///     Converts an Il2CppSystem.Collections.Generic.List&lt;T&gt; to an Il2CppReferenceArray&lt;T&gt;.
    /// </summary>
    public static Il2CppReferenceArray<T> ToIl2CppReferenceArray<T>(this List<T> source)
        where T : Object
    {
        if (source == null) return null;

        var result = new Il2CppReferenceArray<T>(source.Count);
        for (var i = 0; i < source.Count; i++)
            result[i] = source[i];

        return result;
    }

    // -------------------------------------------------------------------------
    // Dictionary<TKey, TValue> <-> Il2CppSystem.Collections.Generic.Dictionary<TKey, TValue>
    // -------------------------------------------------------------------------

    /// <summary>
    ///     Converts an Il2CppSystem Dictionary to a managed Dictionary.
    /// </summary>
    public static System.Collections.Generic.Dictionary<TKey, TValue> ToManagedDictionary<TKey, TValue>(
        this Dictionary<TKey, TValue> source)
        where TKey : Object
        where TValue : Object
    {
        if (source == null) return null;

        var result = new System.Collections.Generic.Dictionary<TKey, TValue>(source.Count);
        foreach (var kvp in source)
            result[kvp.Key] = kvp.Value;

        return result;
    }

    /// <summary>
    ///     Converts a managed Dictionary to an Il2CppSystem Dictionary.
    /// </summary>
    public static Dictionary<TKey, TValue> ToIl2CppDictionary<TKey, TValue>(
        this System.Collections.Generic.Dictionary<TKey, TValue> source)
        where TKey : Object
        where TValue : Object
    {
        if (source == null) return null;

        var result = new Dictionary<TKey, TValue>(source.Count);
        foreach (var kvp in source)
            result[kvp.Key] = kvp.Value;

        return result;
    }
}