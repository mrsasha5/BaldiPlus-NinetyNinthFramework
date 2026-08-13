using BaldiPlusNineNineFramework.Extensions;
using BaldiPlusNineNineFramework.Utilities;
using MTM101BaldAPI;
using MTM101BaldAPI.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Components.UI
{
    public class WelcomeScreenButton : MonoBehaviour
    {
        // References to everything
        public WelcomeScreen screen;
        public Image mainImage;
        public Image maskImage;

        internal Vector2 size;

        public int priority;

        public virtual void Initialize()
        {

        }

        public virtual void OnPress()
        {

        }

        public virtual void PlayAnimation()
        {

        }

        public virtual void Select()
        {

        }

        public virtual void Unselect()
        {

        }

        public void PlaySingleSound(SoundObject sound, float volumeScale = 1f)
        {
            screen.audMan.PlaySingle(sound, volumeScale);
        }

        public void InitializeBackground(Sprite sprite)
        {
            Image bg = UIHelpers.CreateImage(sprite, maskImage.transform, Vector3.zero, false);
            bg.rectTransform.SetCenterAnchor();
        }

        internal void PlayAppearAnimation(float offsetTime)
        {
            mainImage.rectTransform.localScale = Vector3.zero;
            StartCoroutine(Animation(offsetTime));
        }

        private IEnumerator Animation(float offsetTime)
        {
            while (offsetTime > 0f)
            {
                offsetTime -= Time.unscaledDeltaTime;
                yield return null;
            }

            PlaySingleSound(NineNineCore.VanillaAssets.Get<SoundObject>("CampfireToss"), 2f);

            float speed = 3f;
            float value = 0f;
            while (value < 1.1f)
            {
                value += Time.unscaledDeltaTime * speed * 1.5f;
                if (value > 1.1f) value = 1.1f;
                mainImage.rectTransform.localScale = Vector3.one * value;
                yield return null;
            }

            while (value > 0.8f)
            {
                value -= Time.unscaledDeltaTime * speed;
                if (value < 0.8f) value = 0.8f;
                mainImage.rectTransform.localScale = Vector3.one * value;
                yield return null;
            }

            while (value < 1f)
            {
                value += Time.unscaledDeltaTime * speed;
                if (value > 1f) value = 1f;
                mainImage.rectTransform.localScale = Vector3.one * value;
                yield return null;
            }
        }
    }

    public class WelcomeScreen : MonoBehaviour
    {
        private Vector3 startPosition = new Vector3(-150f, -25f, 0f);
        private Vector3 offset = new Vector3(150f, 0, 0f);

        private ModLoadingScreenManager manager;

        private Canvas canvas;

        private float oldRefPixelsPerUnit;

        private static List<Type> registeredButtons = new List<Type>();

        private List<WelcomeScreenButton> buttons = new List<WelcomeScreenButton>();

        private RectTransform animationPoint;

        internal AudioManager audMan;

        public static void RegisterButton<T>() where T : WelcomeScreenButton 
            => RegisterButton(typeof(T));
        public static void RegisterButton(Type type)
        {
            if (type.BaseType != typeof(WelcomeScreenButton)) 
                throw new Exception($"{type.FullName} is not inheriting WelcomeScreenButton! It cannot be registered.");
            registeredButtons.Add(type);
        }

        internal void Initialize(ModLoadingScreenManager manager)
        {
            this.manager = manager;
            canvas = manager.transform.parent.GetComponent<Canvas>();
            oldRefPixelsPerUnit = canvas.referencePixelsPerUnit;
            canvas.referencePixelsPerUnit = 1f; // Instead of 100f

            TextMeshProUGUI text = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans36, "Welcome back!",
                manager.transform, Vector2.zero, false);
            text.rectTransform.SetCenterAnchor();
            text.transform.localPosition = Vector3.up * 150f;
            text.rectTransform.sizeDelta = new Vector2(400f, 50f);
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.black;

            GameObject animationPointObj = new GameObject("AnimationPoint", typeof(RectTransform));
            animationPointObj.transform.SetParent(manager.transform, false);
            animationPoint = animationPointObj.GetComponent<RectTransform>();
            animationPoint.SetCenterAnchor();
            animationPoint.localPosition = Vector3.right * 400f;

            audMan = ObjectUtils.InitAudioManager(gameObject, distanceIndependent: true);
        }

        internal void InitializeButtons(bool animated)
        {
            float offsetTime = 0f;
            for (int i = 0; i < registeredButtons.Count; i++)
            {
                WelcomeScreenButton button = InitializeButton(registeredButtons[i]);
                if (i <= 3)
                {
                    button.PlayAppearAnimation(offsetTime);
                    offsetTime += 0.5f;
                }
            }
            buttons.Sort((a, b) => b.priority.CompareTo(a.priority)); // Higher priority => higher order

            Vector2 pos = startPosition;
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].mainImage.rectTransform.localPosition = startPosition;
                startPosition += offset;
            }

            StartCoroutine(AnimatePoint());
        }

        internal void Close()
        {
            canvas.referencePixelsPerUnit = oldRefPixelsPerUnit;
        }

        private WelcomeScreenButton InitializeButton(Type type)
        {
            Image bordersImage = UIHelpers.CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("TooltipBG"), animationPoint,
                Vector3.zero, false);
            bordersImage.name = $"Button_{type.FullName}";
            bordersImage.type = Image.Type.Sliced;
            bordersImage.rectTransform.sizeDelta = new Vector2(128f, 250f);
            bordersImage.rectTransform.SetCenterAnchor();

            Image maskImage = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_ButtonMask"),
                bordersImage.transform, Vector3.zero, false);
            maskImage.rectTransform.SetCenterAnchor();
            maskImage.gameObject.AddComponent<Mask>();

            WelcomeScreenButton button = (WelcomeScreenButton)bordersImage.gameObject.AddComponent(type);
            button.size = bordersImage.rectTransform.sizeDelta;
            button.screen = this;
            button.mainImage = bordersImage;
            button.maskImage = maskImage;
            button.Initialize();

            bordersImage.gameObject.ConvertToButton<StandardMenuButton>();

            buttons.Add(button);
            return button;
        }

        private IEnumerator AnimatePoint()
        {
            float speed = 400f; //400f
            float reducingSpeed = 200f; //200f
            float value = 400f;

            while (value > 0f)
            {
                if (speed > 150f) speed -= Time.unscaledDeltaTime * reducingSpeed;
                else if (speed > 10f) speed -= Time.unscaledDeltaTime * reducingSpeed * (speed / 100f);

                if (speed < 10f) speed = 10f;
                
                value -= Time.unscaledDeltaTime * speed;
                if (value < 0f) value = 0f;
                animationPoint.localPosition = Vector3.right * value;
                yield return null;
            }
        }
    }
}
