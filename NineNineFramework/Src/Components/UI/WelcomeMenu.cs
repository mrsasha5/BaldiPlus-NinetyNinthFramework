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
    public class WelcomeMenuButton : MonoBehaviour
    {
        private IEnumerator selectAnimation;
        private bool pressAnimation;

        internal Vector2 size;

        public WelcomeMenu screen;
        public Image mainImage;
        public Image maskImage;

        public int priority;

        public virtual void Initialize(object data)
        {

        }

        protected virtual void OnPress()
        {

        }

        /// <summary>
        /// This method is called when animations are turned off
        /// </summary>
        public virtual void InitializeStaticVersion()
        {

        }

        public virtual void PlayAnimation()
        {

        }

        public virtual void PlayBackgroundAnimation()
        {

        }

        public virtual void Select(bool animation)
        {
            if (selectAnimation != null) StopCoroutine(selectAnimation);
            if (animation)
            {
                selectAnimation = SelectAnimation();
                StartCoroutine(selectAnimation);
            }
            else
            {
                mainImage.rectTransform.sizeDelta = size + Vector2.one * 20f;
                maskImage.rectTransform.sizeDelta = mainImage.rectTransform.sizeDelta;
            }
        }

        public virtual void Unselect(bool animation)
        {
            if (selectAnimation != null) StopCoroutine(selectAnimation);
            if (animation)
            {
                selectAnimation = UnselectAnimation();
                StartCoroutine(selectAnimation);
            }
            else
            {
                mainImage.rectTransform.sizeDelta = size;
                maskImage.rectTransform.sizeDelta = mainImage.rectTransform.sizeDelta;
            }
        }

        public void Press()
        {
            if (!pressAnimation)
            {
                pressAnimation = true;
                StartCoroutine(PressAnimation());
            }
        }

        public Image CreateImage(Sprite sprite, Transform parent = null)
        {
            if (parent == null) parent = maskImage.transform;
            Image image = UIHelpers.CreateImage(sprite, parent, Vector3.zero, false);
            image.name = "Image";
            image.rectTransform.SetCenterAnchor();
            return image;
        }

        internal void PlayAppearAnimation(float offsetTime)
        {
            mainImage.rectTransform.localScale = Vector3.zero;
            StartCoroutine(AppearAnimation(offsetTime));
        }

        private IEnumerator SelectAnimation()
        {
            float value = 0f;
            while (value < 20f)
            {
                value += Time.unscaledDeltaTime * 100f;
                if (value > 20f) value = 20f;
                mainImage.rectTransform.sizeDelta = size + Vector2.one * value;
                maskImage.rectTransform.sizeDelta = mainImage.rectTransform.sizeDelta;
                yield return null;
            }
        }

        private IEnumerator UnselectAnimation()
        {
            float value = 20f;
            while (value > 0f)
            {
                value -= Time.unscaledDeltaTime * 100f;
                if (value < 0f) value = 0f;
                mainImage.rectTransform.sizeDelta = size + Vector2.one * value;
                maskImage.rectTransform.sizeDelta = mainImage.rectTransform.sizeDelta;
                yield return null;
            }
        }

        private IEnumerator PressAnimation()
        {
            MusicManager.Instance.PlaySoundEffect(NineNineCore.VanillaAssets.Get<SoundObject>("NotebookCollect"));
            float speed = 2f;
            float value = 1f;
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

            pressAnimation = false;
            OnPress();
        }

        private IEnumerator AppearAnimation(float offsetTime)
        {
            while (offsetTime > 0f)
            {
                offsetTime -= Time.unscaledDeltaTime;
                yield return null;
            }

            MusicManager.Instance.PlaySoundEffect(NineNineCore.VanillaAssets.Get<SoundObject>("CampfireToss"), 2f);
            PlayAnimation();
            PlayBackgroundAnimation();

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

            while (value < 1.05f)
            {
                value += Time.unscaledDeltaTime * speed * 0.5f;
                if (value > 1.05f) value = 1.05f;
                mainImage.rectTransform.localScale = Vector3.one * value;
                yield return null;
            }

            while (value > 1f)
            {
                value -= Time.unscaledDeltaTime * speed * 0.25f;
                if (value < 1f) value = 1f;
                mainImage.rectTransform.localScale = Vector3.one * value;
                yield return null;
            }
        }
    }

    public class WelcomeMenu : MonoBehaviour
    {
        private Vector3 startPosition = new Vector3(-150f, -25f, 0f);
        private Vector3 offset = new Vector3(150f, 0, 0f);

        private ModLoadingScreenManager modLoadingManager;

        private Image parentImage;

        private Canvas canvas;

        private float oldRefPixelsPerUnit;

        private List<WelcomeMenuButton> buttons = new List<WelcomeMenuButton>();

        private RectTransform animationPoint;

        private int selectedButton = -1;

        private bool animationEnded;

        private float currentPointOffset;

        private float targetPointOffset;

        internal AudioManager audMan;

        internal bool SwitchingButton => currentPointOffset != targetPointOffset;

        public event Action onScreenDestroy;

        private static List<Type> registeredButtons = new List<Type>();
        private static List<object> registeredButtonData = new List<object>();
        public static WelcomeMenu Instance { get; private set; }

        public static void RegisterButtonOnInitialization<T>(object data = null) where T : WelcomeMenuButton 
            => RegisterButtonOnInitialization(typeof(T), data);
        public static void RegisterButtonOnInitialization(Type type, object data = null)
        {
            Type baseType = type.BaseType;
            while (baseType != typeof(WelcomeMenuButton))
            {
                baseType = baseType.BaseType;
                if (baseType == null) 
                    throw new Exception($"{type.FullName} is not inheriting WelcomeScreenButton. It cannot be registered.");
            }
            registeredButtons.Add(type);
            registeredButtonData.Add(data);
        }

        public static void CreateButtonDynamically<T>(object data = null) where T : WelcomeMenuButton
        {
            if (Instance == null) throw new NullReferenceException("The Welcome Screen is not instantiated.");
            Instance.StartCoroutine(ButtonCreator<T>(data));
        }

        private static IEnumerator ButtonCreator<T>(object data = null) where T : WelcomeMenuButton
        {
            while (Instance.SwitchingButton || !Instance.animationEnded) yield return null;
            WelcomeMenuButton selectedButton = Instance.buttons[Instance.selectedButton];

            WelcomeMenuButton button = Instance.InitializeButton(typeof(T), data);
            Instance.UpdateButtonPositions();

            if (ConfigManager.welcomeMenuAnimations.Value) button.PlayAppearAnimation(0f);
            else button.InitializeStaticVersion();

            if (Instance.buttons[Instance.selectedButton] != selectedButton)
            {
                Instance.SwitchButton(Instance.buttons.IndexOf(selectedButton), skipAnimation: true);
            }
        }

        internal void Initialize(ModLoadingScreenManager manager, Image parentImage)
        {
            Instance = this;
            this.modLoadingManager = manager;
            this.parentImage = parentImage;
            canvas = manager.transform.parent.GetComponent<Canvas>();
            oldRefPixelsPerUnit = canvas.referencePixelsPerUnit;
            canvas.referencePixelsPerUnit = 1f; // Instead of 100f

            TextMeshProUGUI text = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans36, "Welcome back!",
                parentImage.transform, Vector2.zero, false);
            text.rectTransform.SetCenterAnchor();
            text.transform.localPosition = Vector3.up * 150f;
            text.rectTransform.sizeDelta = new Vector2(400f, 50f);
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.black;

            GameObject animationPointObj = new GameObject("AnimationPoint", typeof(RectTransform));
            animationPointObj.transform.SetParent(parentImage.transform, false);
            animationPoint = animationPointObj.GetComponent<RectTransform>();
            animationPoint.SetCenterAnchor();
            animationPoint.localPosition = Vector3.right * 400f;

            audMan = ObjectUtils.AddAudioManager(gameObject, distanceIndependent: true, subtitles: false);
        }

        internal void InitializeButtons(bool animated)
        {
            for (int i = 0; i < registeredButtons.Count; i++)
            {
                WelcomeMenuButton button = InitializeButton(registeredButtons[i], registeredButtonData[i]);
                if (i <= 2) button.transform.localScale = Vector3.zero;
            }
            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            buttons.Sort((a, b) => b.priority.CompareTo(a.priority)); // Higher priority => higher order
            
            Vector3 pos = startPosition;
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].mainImage.rectTransform.localPosition = pos;
                pos += offset;
            }
        }

        internal void RevealButtonsNoAnimation()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].InitializeStaticVersion();
                if (i < 3) buttons[i].transform.localScale = Vector3.one;
            }
            animationPoint.localPosition = Vector3.zero;
            animationEnded = true;
        }

        internal void RevealButtons(float animationDelayTime) => StartCoroutine(StartAnimations(animationDelayTime));

        private void Update()
        {
            if (SwitchingButton)
            {
                currentPointOffset = Mathf.MoveTowards(currentPointOffset, targetPointOffset, Time.unscaledDeltaTime * 400f);
                animationPoint.localPosition = Vector3.right * currentPointOffset;
            }

            if (animationEnded && !SwitchingButton)
            {
                if (selectedButton < 0)
                {
                    selectedButton = 0;
                    buttons[selectedButton].Select(true);
                }

                if (InputManager.Instance.GetDigitalInput("MouseSubmit", onDown: true) ||
                    Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    buttons[selectedButton].Press();
                }

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D) || 
                    Input.GetKeyDown(KeyCode.S))
                {
                    if (selectedButton + 1 < buttons.Count)
                        SwitchButton(selectedButton + 1);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.A) || 
                    Input.GetKeyDown(KeyCode.W))
                {
                    if (selectedButton - 1 >= 0)
                        SwitchButton(selectedButton - 1);
                }
            }
        }

        internal void SwitchButton(int index, bool skipAnimation = false)
        {
            buttons[selectedButton].Unselect(!skipAnimation);
            selectedButton = index;
            buttons[selectedButton].Select(!skipAnimation);
            targetPointOffset = selectedButton * -150;
            if (skipAnimation)
            {
                currentPointOffset = targetPointOffset;
                animationPoint.localPosition = Vector3.right * currentPointOffset;
            }
        }

        public void Close(bool immediately)
        {
            enabled = false; // No control anymore

            if (!immediately)
            {
                GlobalCam.Instance.StartCoroutine(CloseAnimation());
            }
            else
            {
                canvas.referencePixelsPerUnit = oldRefPixelsPerUnit;
                Destroy(gameObject);
                modLoadingManager.Start();
                onScreenDestroy?.Invoke();
            }
        }

        private IEnumerator CloseAnimation()
        {
            GlobalCam.Instance.Transition(UiTransition.Dither, 0.01666667f);
            parentImage.gameObject.SetActive(false);

            while (GlobalCam.Instance.TransitionActive)
            {
                yield return null;
            }

            Close(immediately: true);
        }

        private WelcomeMenuButton InitializeButton(Type type, object data)
        {
            Image bordersImage = UIHelpers.CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("TooltipBG"), animationPoint,
                Vector3.zero, false);
            bordersImage.name = $"Button_{type.Name}";
            bordersImage.type = Image.Type.Sliced;
            bordersImage.rectTransform.sizeDelta = new Vector2(128f, 250f);
            bordersImage.rectTransform.SetCenterAnchor();

            Image maskImage = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_ButtonMask"),
                bordersImage.transform, Vector3.zero, false);
            maskImage.name = "MaskImage";
            maskImage.rectTransform.SetCenterAnchor();
            maskImage.gameObject.AddComponent<Mask>();

            WelcomeMenuButton button = (WelcomeMenuButton)bordersImage.gameObject.AddComponent(type);
            button.size = bordersImage.rectTransform.sizeDelta;
            button.screen = this;
            button.mainImage = bordersImage;
            button.maskImage = maskImage;
            button.Initialize(data);

            buttons.Add(button);
            return button;
        }

        private IEnumerator StartAnimations(float delay)
        {
            while (delay > 0f)
            {
                delay -= Time.unscaledDeltaTime;
                yield return null;
            }

            float offsetTime = 0f;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i <= 2)
                {
                    buttons[i].PlayAppearAnimation(offsetTime);
                    offsetTime += 0.5f;
                }
                else break;
            }
            StartCoroutine(AnimatePoint());
        }

        private IEnumerator AnimatePoint()
        {
            float speed = 400f;
            float reducingSpeed = 200f;
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

            animationEnded = true;
        }
    }
}