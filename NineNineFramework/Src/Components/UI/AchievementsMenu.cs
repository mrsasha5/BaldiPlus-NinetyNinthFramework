using BaldiPlusNineNineFramework.Extensions;
using BaldiPlusNineNineFramework.Utilities;
using MTM101BaldAPI;
using MTM101BaldAPI.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Components.UI
{
    internal class AchievementsMenu : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private Image scrollAreaImage;

        [SerializeField]
        private RectTransform contentRect;

        [SerializeField]
        private TooltipController tooltip;

        private List<Image> iconImages = new List<Image>();

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

            component.tooltip = canvas.gameObject.AddComponent<TooltipController>();
            //component.tooltip.tooltipBgRect = NineNineCore.VanillaAssets.Get<GameObject>("");
            // Initializing elements
            Image background = new GameObject("Background").AddComponent<Image>();
            background.gameObject.layer = LayerUtils.UI;
            background.rectTransform.SetParent(canvas.transform, false);
            background.rectTransform.SetCenterAnchor();
            background.rectTransform.sizeDelta = new Vector2(480f, 360f);
            // Initializing scroll background
            component.scrollAreaImage = new GameObject("ScrollArea").AddComponent<Image>();
            component.scrollAreaImage.gameObject.layer = LayerUtils.UI;
            component.scrollAreaImage.rectTransform.SetParent(canvas.transform, false);
            component.scrollAreaImage.rectTransform.SetCenterAnchor();
            component.scrollAreaImage.rectTransform.localPosition = Vector3.up * -22f;
            component.scrollAreaImage.rectTransform.sizeDelta = new Vector2(384f, 304f);
            component.scrollAreaImage.gameObject.AddComponent<Mask>();
            component.contentRect = new GameObject("Content", typeof(RectTransform)).GetComponent<RectTransform>();
            component.contentRect.gameObject.layer = LayerUtils.UI;
            component.contentRect.SetParent(component.scrollAreaImage.transform, false);
            component.contentRect.SetCenterAnchor();
            // Own scroll bar
            ScrollBar scrollBar = ScrollBar.Create(new Vector2(30f, 304f));
            ((RectTransform)scrollBar.transform).SetCenterAnchor();
            scrollBar.transform.SetParent(canvas.transform, false);
            scrollBar.transform.localPosition = new Vector3(216f, -22f, 0f);
            scrollBar.targetTransform = component.contentRect;
            scrollBar.minValue = 0f;
            scrollBar.maxValue = -800f;

            TextMeshProUGUI title =
                UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans36, "Achievements", canvas.transform, Vector3.zero, false);
            title.rectTransform.SetCenterAnchor();
            title.rectTransform.sizeDelta = new Vector2(300f, 50f);
            title.alignment = TextAlignmentOptions.Center;
            title.rectTransform.localPosition = Vector3.up * 160f;
            title.color = Color.black;

            Image backArrow = UIHelpers.CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("BackArrow_0"), canvas.transform, 
                Vector3.zero, false);
            backArrow.name = "BackButton";
            backArrow.rectTransform.SetCenterAnchor();
            backArrow.rectTransform.localPosition = new Vector3(-224f, 164f, 0f);
            component.exitButton = backArrow.gameObject.ConvertToButton<StandardMenuButton>();
            component.exitButton.swapOnHigh = true;
            component.exitButton.EnableTransition();
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
            for (int i = 0; i < 50; i++)
            {
                CreateAchievement(null);
            }
            ComposeElements();
        }

        private void ComposeElements()
        {
            Vector3 xOffset = Vector3.right * 80f; // 64 + 16
            Vector3 yOffset = Vector3.down * 80f;
            Vector3 startPos = xOffset * -2f - yOffset * 1.5f;

            Vector3 position = startPos;
            for (int i = 0; i < iconImages.Count; i++)
            {
                iconImages[i].rectTransform.localPosition = position;
                if ((i + 1) % 5 == 0)
                {
                    position += yOffset;
                    position.x = startPos.x;
                }
                else position += xOffset;
            }
        }

        private void CreateAchievement(Sprite iconSprite)
        {
            Image icon = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("MissingAsset"), canvas.transform, Vector3.zero,
                false);
            if (iconSprite != null) icon.sprite = iconSprite;
            icon.rectTransform.SetCenterAnchor();
            icon.rectTransform.SetParent(contentRect);
            icon.rectTransform.SetSiblingIndex(CursorController.Instance.transform.GetSiblingIndex() - 1);
            iconImages.Add(icon);
        }
    }
}
