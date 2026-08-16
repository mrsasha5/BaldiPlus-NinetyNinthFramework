using BaldiPlusNineNineFramework.Components.UI;
using BaldiPlusNineNineFramework.Managers;
using HarmonyLib;
using MTM101BaldAPI;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Patches
{
    [HarmonyPatch]
    internal class Mtm101DevApiLoadingPatch
    {
        private static bool welcomeInstantiated;

        [HarmonyPatch(typeof(MTM101BaldiDevAPI), "OnSceneUnload")]
        [HarmonyPostfix]
        private static void OnUnload() => NineNineCore.OnSceneUnload();

        [HarmonyPatch(typeof(ModLoadingScreenManager), "Start")]
        [HarmonyPrefix]
        private static bool OnStart(ModLoadingScreenManager __instance)
        {
            if (!welcomeInstantiated && ConfigManager.welcomeMenu.Value)
            {
                InitializeWelcomeScreen(__instance);
                return false;
            }
            return true;
        }

        private static void InitializeWelcomeScreen(ModLoadingScreenManager loadingScreen)
        {
            loadingScreen.gameObject.AddComponent<Mask>();
            WelcomeScreen screen = loadingScreen.gameObject.AddComponent<WelcomeScreen>();
            screen.Initialize(loadingScreen);
            screen.InitializeButtons(true);
            if (ConfigManager.welcomeMenuAnimations.Value) screen.RevealButtons(animationDelayTime: 0.75f);
            else screen.RevealButtonsNoAnimation();
        }
    }
}