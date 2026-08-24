using BaldiPlusNineNineFramework.Components.UI;
using BaldiPlusNineNineFramework.Extensions;
using BaldiPlusNineNineFramework.Managers;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.UI;
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
                welcomeInstantiated = true;
                return false;
            }
            return true;
        }

        private static void InitializeWelcomeScreen(ModLoadingScreenManager loadingScreen)
        {
            loadingScreen.name = "ModLoadingScreen";

            Image mainImage = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("WhitePixel"), loadingScreen.transform, 
                Vector3.zero, false);
            mainImage.name = "WelcomeBackScreen";
            mainImage.rectTransform.SetCenterAnchor();
            mainImage.rectTransform.sizeDelta = new Vector3(480f, 360f);
            mainImage.gameObject.AddComponent<Mask>();

            WelcomeMenu screen = mainImage.gameObject.AddComponent<WelcomeMenu>();
            screen.Initialize(loadingScreen, mainImage);
            screen.InitializeButtons(true);
            if (ConfigManager.welcomeMenuAnimations.Value) screen.RevealButtons(animationDelayTime: 0.75f);
            else screen.RevealButtonsNoAnimation();

            if (ConfigManager.webContent.Value) WebContentManager.LoadWelcomeMenuPollButton();
        }
    }
}