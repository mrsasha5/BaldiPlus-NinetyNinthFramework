using BaldiPlusNineNineFramework.Utilities;
using HarmonyLib;

namespace BaldiPlusNineNineFramework.Patches
{
    [HarmonyPatch(typeof(GlobalSystemInitializer))]
    internal class SingletonsInitPatch
    {
        [HarmonyPatch("LoadMain")]
        [HarmonyPostfix]
        private static void OnSingletonsLoaded()
        {
            Registers.InvokeSingletons();
        }
    }
}
