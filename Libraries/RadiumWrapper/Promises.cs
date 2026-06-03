using BepInEx.Unity.IL2CPP.Utils.Collections;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Runtime;
using RecRoom.Async;
using System.Collections;

namespace RadTools.Libraries.RadiumWrapper;

internal class Promises
{
    internal static IPromise<bool> ReturnValue(bool value)
    {
        var promise = new Promise<bool>();
        Core.Instance.instance.StartCoroutine(ResolveNextFrame(promise, value).WrapToIl2Cpp());
        return Il2CppObjectPool.Get<IPromise<bool>>(IL2CPP.Il2CppObjectBaseToPtr(promise));
    }

    private static IEnumerator ResolveNextFrame(Promise<bool> promise, bool value)
    {
        yield return null;
        promise.Complete(value);
    }
}