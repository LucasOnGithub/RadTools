using System;

namespace RadTools.Libraries.RadiumWrapper;

internal class GameRoot
{
    internal static event Action RootGameStarted;

    /// <summary>
    ///     NEVER RUN THIS METHOD. This is only for internal use to invoke the RootGameStarted event when the game starts.
    /// </summary>
    internal static void InternalInvokeRootGameStarted()
    {
        RootGameStarted?.Invoke();
    }
}