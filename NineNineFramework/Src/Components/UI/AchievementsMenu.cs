using BaldiPlusNineNineFramework.Extensions;
using BaldiPlusNineNineFramework.Utilities;
using MTM101BaldAPI;
using MTM101BaldAPI.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Components.UI
{
    internal class AchievementsMenu : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        public StandardMenuButton exitButton;

        internal static AchievementsMenu prefab;

        internal static void InitializePrefab()
        {
            // Actually adding canvas assigner
            // But setting false due to mtm101's api mistake
            Canvas canvas = UIHelpers.CreateBlankUIScreen("AchievementsMenu", addCanvasAssigner: false);
            canvas.renderMode = RenderMode.ScreenSpaceCamera; // Another mistake in the api

            canvas.gameObject.ConvertToPrefab(false); // What the point of setActive argument if I cannot disable prefab using it
                                                      // Seems to be a weird and confusing design
            canvas.gameObject.SetActive(false);
            UIHelpers.AddCursorInitiatorToCanvas(canvas);
            AchievementsMenu component = canvas.gameObject.AddComponent<AchievementsMenu>();
            component.canvas = canvas;

            // Initializing elements
            TextMeshProUGUI title =
                UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans36, "Achievements", canvas.transform, Vector3.zero, false);
            title.rectTransform.SetCenterAnchor();
            title.rectTransform.sizeDelta = new Vector2(300f, 50f);
            title.alignment = TextAlignmentOptions.Center;
            title.rectTransform.localPosition = Vector3.up * 150f;
            title.color = Color.black;

            Image backArrow = UIHelpers.CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("BackArrow_0"), canvas.transform, 
                Vector3.zero, false);
            backArrow.rectTransform.SetCenterAnchor();
            backArrow.rectTransform.localPosition = new Vector3(-224f, 164f, 0f);
            component.exitButton = backArrow.gameObject.ConvertToButton<StandardMenuButton>();
            component.exitButton.swapOnHigh = true;
            component.exitButton.highlightedSprite = NineNineCore.VanillaAssets.Get<Sprite>("BackArrow_1");
            component.exitButton.unhighlightedSprite = NineNineCore.VanillaAssets.Get<Sprite>("BackArrow_0");

            UIHelpers.AddBordersToCanvas(canvas);

            prefab = component;
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            Vector3 xOffset = Vector3.right * 80f; // 64 + 16
            Vector3 yOffset = Vector3.down * 80f;
            Vector3 startPos = xOffset * -2f - yOffset;

            Vector3 position = startPos;
            for (int i = 0; i < 20; i++)
            {
                CreateAchievement(null, position);
                if ((i + 1) % 5 == 0)
                { 
                    position += yOffset;
                    position.x = startPos.x;
                }
                else position += xOffset;
            }
        }

        private void CreateAchievement(Sprite iconSprite, Vector3 position)
        {
            Image icon = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("MissingAsset"), canvas.transform, Vector3.zero,
                false);
            if (iconSprite != null) icon.sprite = iconSprite;
            icon.rectTransform.SetCenterAnchor();
            icon.rectTransform.localPosition = position;
            icon.rectTransform.SetSiblingIndex(CursorController.Instance.transform.GetSiblingIndex() - 1);
        }
    }
}
