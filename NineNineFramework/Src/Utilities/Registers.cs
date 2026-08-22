using System;

namespace BaldiPlusNineNineFramework.Utilities
{
    public class Registers
    {
        public static event Action OnSingletonsInstantiated;

        internal static void InvokeSingletons() => OnSingletonsInstantiated?.Invoke();
    }
}
