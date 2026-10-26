using BaldiPlusNineNineFramework.Utilities;
using HarmonyLib;

namespace BaldiPlusNineNineFramework.Patches
{
    [HarmonyPatch(typeof(PlayerFileManager))]
    internal class FileNameLoadPatch
    {
        [HarmonyPatch("Load")]
        [HarmonyPostfix]
        private static void InvokeEvent(bool __result)
        {
            if (__result) Events.InvokeFileNameLoaded();
            else Events.InvokeFileNameLoadFailed();
        }
    }
}