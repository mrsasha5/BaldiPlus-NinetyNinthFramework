using BaldiPlusNineNineFramework.Utilities;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.UI;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Patches
{
    [HarmonyPatch]
    internal class Mtm101DevApiLoadingPatch
    {
        private static ModLoadingScreenManager manager;

        private static float oldRefPixelsPerUnit;

        private static Canvas canvas;

        [HarmonyPatch(typeof(MTM101BaldiDevAPI), "OnSceneUnload")]
        [HarmonyPostfix]
        private static void OnUnload() => NineNineCore.OnSceneUnload();

        [HarmonyPatch(typeof(ModLoadingScreenManager), "Start")]
        [HarmonyPrefix]
        private static bool OnStart(ModLoadingScreenManager __instance)
        {
            if (manager == null)
            {
                manager = __instance;
                InitializeWelcomeScreen();
                return false;
            }
            return false;
        }

        private static void InitializeWelcomeScreen()
        {
            canvas = manager.transform.parent.GetComponent<Canvas>();
            oldRefPixelsPerUnit = canvas.referencePixelsPerUnit;
            canvas.referencePixelsPerUnit = 1f; // Instead of 100f

            TextMeshProUGUI text = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans36, "Welcome back!", 
                manager.transform, Vector2.zero, false);
            text.rectTransform.anchorMin = text.rectTransform.anchorMin = Vector2.one / 2f;
            text.transform.localPosition = Vector3.up * 150f;
            text.rectTransform.sizeDelta = new Vector2(250f, 50f);
            text.color = Color.black;

            StandardMenuButton playButton = CreateButton(new Vector2(128f, 250f), new Vector3(-150f, -25f, 0f), 
                NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Play"));
            playButton.name = "PlayButton";
            StandardMenuButton updatesButton = CreateButton(new Vector2(128f, 250f), new Vector3(0f, -25f, 0f), 
                NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Updates"));
            updatesButton.name = "UpdateCenterButton";
        }

        private static StandardMenuButton CreateButton(Vector2 size, Vector3 position, Sprite thumbnail)
        {
            Image bordersImage = UIHelpers.CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("TooltipBG"), manager.transform,
                Vector3.zero, false);
            bordersImage.name = "Button";
            bordersImage.type = Image.Type.Sliced;
            bordersImage.rectTransform.sizeDelta = size;
            bordersImage.rectTransform.anchorMin = bordersImage.rectTransform.anchorMax = Vector3.one / 2f;
            bordersImage.transform.localPosition = position;

            Image maskImage = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_ButtonMask"), 
                bordersImage.transform, Vector3.zero, false);
            maskImage.rectTransform.anchorMin = maskImage.rectTransform.anchorMax = Vector3.one / 2f;
            maskImage.transform.localPosition = Vector3.zero;
            maskImage.gameObject.AddComponent<Mask>();

            if (thumbnail != null)
            {
                UIHelpers.CreateImage(thumbnail, maskImage.transform, Vector3.zero, false);
            }

            return bordersImage.gameObject.ConvertToButton<StandardMenuButton>();
        }
    }
}
