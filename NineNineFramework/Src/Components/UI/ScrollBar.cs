using BaldiPlusNineNineFramework.Extensions;
using BaldiPlusNineNineFramework.Utilities;
using MTM101BaldAPI.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Components.UI
{
    public class ScrollBar : MonoBehaviour, IStandardUIElement
    {
        [SerializeField]
        private Image backgroundImage, scrollButtonImage;

        [SerializeField]
        private StandardMenuButton scrollButton;

        [SerializeField]
        private float padding, minValue, maxValue, buttonHeightPercent, mouseScrollSpeed, magnetThreshold;

        [SerializeField]
        private bool mouseWheelSupport, locked;

        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        private AnalogInputData itemAnalogData;

        private Vector2 _absoluteVector;
        private Vector2 _deltaVector;

        private float value;

        private bool trackCursor, initialized;

        public Image BackgroundImage => backgroundImage;

        public Image ButtonImage => scrollButtonImage;

        public static ScrollBar Create(Vector2 size, float padding = 3f)
        {
            GameObject scrollBar = new GameObject("ScrollBar");
            scrollBar.gameObject.layer = LayerUtils.UI;

            Image bgImage = scrollBar.AddComponent<Image>();

            Image scrollButtonImage = new GameObject("ScrollButton").AddComponent<Image>();
            scrollButtonImage.gameObject.layer = scrollBar.gameObject.layer;
            scrollButtonImage.rectTransform.SetParent(scrollBar.transform, false);
            scrollButtonImage.rectTransform.SetCenterAnchor();
            StandardMenuButton button = scrollButtonImage.gameObject.ConvertToButton<StandardMenuButton>();

            ScrollBar component = scrollBar.AddComponent<ScrollBar>();
            component.enabled = true; // The fuck you disable yourself when you have another monobehaviour
            component.backgroundImage = bgImage;
            component.scrollButtonImage = scrollButtonImage;
            component.scrollButton = button;
            component.itemAnalogData = new AnalogInputData();
            component.itemAnalogData.xDeltaId = "ItemSelect";
            component.itemAnalogData.yDeltaId = "ItemSelect";
            component.itemAnalogData.steamDeltaId = "ItemAxis";
            component.SetMagnetThreshold(0.01f);
            component.SetMouseScrollSpeed(2000f);
            component.SetButtonLengthPercent(0.16f);
            component.SetMouseWheelSupport(true);
            component.SetBackgroundSize(size, padding);
            component.SetBackgroundColor(Color.black);
            component.SetButtonColor(Color.gray);
            return component;
        }

        private void Start()
        {
            if (!initialized)
            {
                Reset();
                initialized = true;
                scrollButton.OnPress.AddListener(OnPressScrollButton);
                scrollButton.OnRelease.AddListener(OnReleaseScrollButton);
            }   
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
            if (locked) return;

            // Handle the button
            if (trackCursor)
            {
                if (CursorController.Instance.cursorTransform.position.y != scrollButtonImage.rectTransform.position.y)
                {
                    SetButtonPosition(CursorController.Instance.cursorTransform.position.y, false);
                }
            }
            // Or check if player uses mouse wheel
            else if (mouseWheelSupport)
            {
                InputManager.Instance.GetAnalogInput(itemAnalogData, out _absoluteVector, out _deltaVector, 0.05f);
                SetButtonPosition(scrollButton.transform.position.y - _deltaVector.x * mouseScrollSpeed * Time.unscaledDeltaTime, true);
            }
        }

        private void SetButtonPosition(float yValue, bool pushToEnd)
        {
            // Calculate the allowed range of Y-axis
            float scrollButtonHalfLength = scrollButtonImage.rectTransform.sizeDelta.y / 2f;
            float startY = backgroundImage.rectTransform.sizeDelta.y / 2f - scrollButtonHalfLength - padding;
            float endY = -startY;

            // Set new button position
            Vector3 oldButtonLocalPos = scrollButtonImage.rectTransform.localPosition;
            Vector3 newButtonGlobalPos = backgroundImage.rectTransform.position;
            newButtonGlobalPos.y = yValue;
            scrollButtonImage.rectTransform.position = newButtonGlobalPos;

            // Revert position
            // Or set start/end pos if parameter pushToEnd is true
            if (scrollButtonImage.rectTransform.localPosition.y > startY)
            {
                if (pushToEnd) scrollButtonImage.rectTransform.localPosition = Vector3.up * startY;
                else scrollButtonImage.rectTransform.localPosition = oldButtonLocalPos;
            }
            else if (scrollButtonImage.rectTransform.localPosition.y < endY)
            {
                if (pushToEnd) scrollButtonImage.rectTransform.localPosition = Vector3.up * endY;
                else scrollButtonImage.rectTransform.localPosition = oldButtonLocalPos;
            }
            else
            {
                // Set new position if everything is fine
                UpdateScrollableElement();
            }
        }

        private void UpdateScrollableElement()
        {
            value = minValue + (maxValue - minValue) * GetButtonPercent();
            Vector3 pos = targetTransform.position;
            pos.y = value;
            targetTransform.position = pos;
        }

        public ScrollBar SetMagnetThreshold(float value)
        {
            magnetThreshold = value;
            return this;
        }

        public ScrollBar SetMouseScrollSpeed(float value)
        {
            mouseScrollSpeed = value;
            return this;
        }

        public ScrollBar SetMouseWheelSupport(bool value)
        {
            mouseWheelSupport = value;
            return this;
        }

        /// <summary>
        /// Defines which transform to move. The value is serializable in Unity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ScrollBar SetTargetTransform(Transform value)
        {
            targetTransform = value;
            UpdateScrollableElement();
            return this;
        }

        /// <summary>
        /// Defines how the button's Y-axis of size is calculated by the component. The value is serializable in Unity.
        /// Allowed range of the value is [0f; 1f). This method throws an exception if argument is improperly defined.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ScrollBar SetButtonLengthPercent(float value)
        {
            if (value < 0f || value >= 1f) new ArgumentOutOfRangeException("value", "allowed range is [0f; 1f)");
            buttonHeightPercent = value;
            SetBackgroundSize(backgroundImage.rectTransform.sizeDelta, padding);
            return this;
        }

        /// <summary>
        /// Defines min and max values of global Y-axis for scrollable transform.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public ScrollBar SetMinMaxValues(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            if (value < minValue) value = minValue;
            else if (value > maxValue) value = maxValue;
            UpdateButtonPosition(GetButtonPercent());
            UpdateScrollableElement();
            return this;
        }

        /// <summary>
        /// Defines size of background image.
        /// This method overload has a default padding value.
        /// Updates button size as well.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public ScrollBar SetBackgroundSize(Vector2 size)
        {
            return SetBackgroundSize(size, 3f);
        }

        /// <summary>
        /// Defines button padding and size of background image.
        /// Updates button size as well.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public ScrollBar SetBackgroundSize(Vector2 size, float padding)
        {
            this.padding = padding;
            float percent = GetButtonPercent();
            backgroundImage.rectTransform.sizeDelta = size;
            scrollButtonImage.rectTransform.sizeDelta = new Vector2(size.x - padding * 2f, size.y * buttonHeightPercent);
            UpdateButtonPosition(percent);
            return this;
        }

        public ScrollBar SetBackgroundColor(Color color)
        {
            backgroundImage.color = color;
            return this;
        }

        public ScrollBar SetButtonColor(Color color)
        {
            scrollButtonImage.color = color;
            return this;
        }

        public ScrollBar SetBackgroundSprite(Sprite sprite)
        {
            backgroundImage.sprite = sprite;
            return this;
        }

        public ScrollBar SetButtonSprite(Sprite sprite)
        {
            scrollButtonImage.sprite = sprite;
            return this;
        }

        public void Reset()
        {
            UpdateButtonPosition(0f);
            UpdateScrollableElement();
        }

        public void Lock(bool value)
        {
            locked = value;
        }

        private float GetButtonPercent()
        {
            float startPosition = (backgroundImage.rectTransform.sizeDelta.y - scrollButtonImage.rectTransform.sizeDelta.y) / 2f - padding;
            float range = startPosition * 2f;
            if (range == 0f) return 0f;
            float percent = Mathf.Clamp((startPosition - scrollButtonImage.rectTransform.localPosition.y) / range, 0f, 1f);
            if (percent < 0.5f && percent < magnetThreshold) return 0f;
            else if (percent >= 0.5f && percent > 1f - magnetThreshold) return 1f;
            return percent;
        }

        private void UpdateButtonPosition(float percent)
        {
            scrollButtonImage.rectTransform.localPosition = new Vector2(0f,
                // Calculate start position
                (backgroundImage.rectTransform.sizeDelta.y - scrollButtonImage.rectTransform.sizeDelta.y) / 2f - padding -
                // Calculate offset
                (backgroundImage.rectTransform.sizeDelta.y - scrollButtonImage.rectTransform.sizeDelta.y - padding * 2f) * percent);
        }
    }
}