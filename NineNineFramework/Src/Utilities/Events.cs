using System;

namespace BaldiPlusNineNineFramework.Utilities
{
    public class Events
    {
        public static event Action OnFileNameLoaded;
        public static event Action OnFileNameLoadFailed;

        // Actually it does not allow to call those delegates outside
        // so I've made this.
        internal static void InvokeFileNameLoaded() => OnFileNameLoaded?.Invoke();
        internal static void InvokeFileNameLoadFailed() => OnFileNameLoadFailed?.Invoke();
    }
}
