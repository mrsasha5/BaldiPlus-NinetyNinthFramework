using BaldiPlusNineNineFramework.Extensions;
using BaldiPlusNineNineFramework.Utilities;
using MTM101BaldAPI.UI;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Components.UI
{
    internal class ScrollBar : MonoBehaviour, IStandardUIElement
    {
        [SerializeField]
        private Image backgroundImage, scrollButtonImage;

        [SerializeField]
        private StandardMenuButton scrollButton;

        [SerializeField]
        private float padding;

        [SerializeField]
        private float buttonHeightPercent = 0.16f;

        private float value;

        private bool trackCursor;

        // The fields that are designed for public changes
        public float maxValue = 0;
        public float minValue = 0;
        public Transform targetTransform;

        public static ScrollBar Create(Vector2 size, float padding = 3f)
        {
            GameObject scrollBar = new GameObject("ScrollBar");
            scrollBar.gameObject.layer = LayerUtils.UI;

            Image bgImage = scrollBar.AddComponent<Image>();

            Image scrollButtonImage = new GameObject("SliderButton").AddComponent<Image>();
            scrollButtonImage.gameObject.layer = scrollBar.gameObject.layer;
            scrollButtonImage.rectTransform.SetParent(scrollBar.transform, false);
            scrollButtonImage.rectTransform.SetCenterAnchor();
            StandardMenuButton button = scrollButtonImage.gameObject.ConvertToButton<StandardMenuButton>();

            ScrollBar component = scrollBar.AddComponent<ScrollBar>();
            component.enabled = true; // The fuck you disable yourself when you have another monobehaviour
            component.backgroundImage = bgImage;
            component.scrollButtonImage = scrollButtonImage;
            component.scrollButton = button;
            component.SetSize(size, padding);
            component.SetColors(Color.black, Color.gray);
            return component;
        }

        private void Awake()
        {
            scrollButton.OnPress.AddListener(OnPressScrollButton);
            scrollButton.OnRelease.AddListener(OnReleaseScrollButton);
        }

        private void OnPressScrollButton()
        {
            trackCursor = true;
        }

        private void OnReleaseScrollButton()
        {
            trackCursor = false;
        }

        private void Update()
        {
            if (trackCursor)
            {
                if (CursorController.Instance.cursorTransform.position.y != scrollButtonImage.rectTransform.position.y)
                {
                    // Calculate the allowed range of Y coordinate
                    float scrollButtonHalfLength = scrollButtonImage.rectTransform.sizeDelta.y / 2f;
                    float startY = backgroundImage.rectTransform.sizeDelta.y / 2f - scrollButtonHalfLength - padding;
                    float endY = -startY;

                    // Set new button position
                    Vector3 oldButtonLocalPos = scrollButtonImage.rectTransform.localPosition;
                    Vector3 newButtonGlobalPos = backgroundImage.rectTransform.position;
                    newButtonGlobalPos.y = CursorController.Instance.cursorTransform.position.y;
                    scrollButtonImage.rectTransform.position = newButtonGlobalPos;

                    // Revert changes if new position is incorrect
                    // Or else update target position based on the new button position
                    if (scrollButtonImage.rectTransform.localPosition.y > startY || 
                        scrollButtonImage.rectTransform.localPosition.y < endY) 
                        scrollButtonImage.rectTransform.localPosition = oldButtonLocalPos;
                    else
                    {
                        value = minValue + (maxValue - minValue) * GetButtonPercent();
                        Vector3 pos = targetTransform.position;
                        pos.y = value;
                        targetTransform.position = pos;
                    }
                }
            }
        }

        public void SetSize(Vector2 size, float padding = 3f)
        {
            this.padding = padding;
            backgroundImage.rectTransform.sizeDelta = size;
            scrollButtonImage.rectTransform.sizeDelta = new Vector2(size.x - padding * 2f, size.y * buttonHeightPercent);
            UpdateButtonPosition((value - minValue) / (maxValue - minValue));
        }

        public void SetColors(Color backgroundColor, Color sliderColor)
        {
            backgroundImage.color = backgroundColor;
            scrollButtonImage.color = sliderColor;
        }

        private float GetButtonPercent()
        {
            // Calculate the range of button position for Y axis
            float range = backgroundImage.rectTransform.sizeDelta.y - scrollButtonImage.rectTransform.sizeDelta.y * 2f - padding * 2f;
            float startPos = (backgroundImage.rectTransform.sizeDelta.y - scrollButtonImage.rectTransform.sizeDelta.y) / 2f - padding;
            return Mathf.Abs((startPos - scrollButtonImage.rectTransform.localPosition.y) / startPos * 2f);
        }

        private void UpdateButtonPosition(float percent)
        {
            // Calculate start position on the first line
            // Calculate offset on the second line
            scrollButtonImage.rectTransform.localPosition = new Vector2(0f,
                (backgroundImage.rectTransform.sizeDelta.y - scrollButtonImage.rectTransform.sizeDelta.y) / 2f - padding -
                (backgroundImage.rectTransform.sizeDelta.y - scrollButtonImage.rectTransform.sizeDelta.y - padding * 2f) * percent);
        }

        public void Reset()
        {

        }

        public void Lock(bool value)
        {

        }
    }
}