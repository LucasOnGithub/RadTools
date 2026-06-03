using System.Collections.Generic;
using UnityEngine;

namespace RadTools.Extensions;

public static class Transforms
{
    public static IEnumerable<GameObject> Children(this Transform t)
    {
        var list = new List<GameObject>();
        for (var i = 0; i < t.childCount; i++)
            list.Add(t.GetChild(i).gameObject);
        return list;
    }
}