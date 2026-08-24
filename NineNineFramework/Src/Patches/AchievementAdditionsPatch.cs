using BaldiPlusNineNineFramework.Components.UI;
using BaldiPlusNineNineFramework.Extensions;
using HarmonyLib;
using MTM101BaldAPI.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Patches
{
    [HarmonyPatch]
    internal class AchievementAdditionsPatch
    {
        private static MainMenu mainMenu;
        private static AchievementsMenu achievementsMenu;

        private static void SwitchMenu()
        {
            GlobalCam.Instance.Transition(UiTransition.Dither, 0.01666667f);

            mainMenu.gameObject.SetActive(!mainMenu.gameObject.activeSelf);
            achievementsMenu.gameObject.SetActive(!mainMenu.gameObject.activeSelf);
        }

        [HarmonyPatch(typeof(MainMenu))]
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void CreateAchievementsButton(MainMenu __instance)
        {
            mainMenu = __instance;
            Canvas canvas = __instance.GetComponent<Canvas>();

            Image buttonImage = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("TerriblePlaceholder"), canvas.transform,
                Vector3.zero, false);
            buttonImage.name = "AchievementsButton";
            buttonImage.rectTransform.SetCenterAnchor();
            buttonImage.transform.localPosition = new Vector3(-150f, -60f, 0f);
            StandardMenuButton button = buttonImage.gameObject.ConvertToButton<StandardMenuButton>();
            button.swapOnHigh = true;
            button.highlightedSprite = NineNineCore.Assets.Get<Sprite>("EvenMoreTerriblePlaceholder");
            button.unhighlightedSprite = NineNineCore.Assets.Get<Sprite>("TerriblePlaceholder");
            button.transform.SetSiblingIndex(CursorController.instance.transform.GetSiblingIndex() - 1);
            button.OnPress.AddListener(SwitchMenu);
        }

        [HarmonyPatch(typeof(MenuInitializer))]
        [HarmonyPatch("Awake")]
        [HarmonyPrefix]
        private static void InitializeMenu(MenuInitializer __instance)
        {
            __instance.StartCoroutine(AchievementsMenuInitializer(__instance));
        }

        private static IEnumerator AchievementsMenuInitializer(MenuInitializer menuInitializer)
        {
            while (!NineNineCore.CoreInitialized) yield return null;

            achievementsMenu = GameObject.Instantiate(AchievementsMenu.prefab);
            achievementsMenu.exitButton.OnPress.AddListener(SwitchMenu);
        }
    }
}