using BaldiPlusNineNineFramework.Extensions;
using BaldiPlusNineNineFramework.Utilities;
using MTM101BaldAPI.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Components.UI
{
    internal class WelcomePlayButton : WelcomeMenuButton
    {
        private Image[] images = new Image[5];

        private IEnumerator animator;
        private IEnumerator[] bgAnimators;

        public override void Initialize(object data)
        {
            base.Initialize(data);
            priority = 128;
            CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Play")).name = "Background";

            images[0] = CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("Slap_Sheet_0"));
            images[0].name = "Baldi";
            images[0].rectTransform.localPosition = new Vector3(10f, -38f, 0f);

            string[] questionMarkNames = new string[] { "QMarkSheet_1", "QMarkSheet_5", "QMarkSheet_3", "QMarkSheet_4" };
            Vector3[] markPositions = new Vector3[] { new Vector3(-44f, 47f, 0f), new Vector3(44f, 64f, 0f), 
                new Vector3(-46f, -45f, 0f), new Vector3(46f, -54f, 0f) };

            for (int i = 1; i < images.Length; i++)
            {
                int index = i - 1;
                images[i] = CreateImage(NineNineCore.VanillaAssets.Get<Sprite>(questionMarkNames[index]));
                images[i].name = questionMarkNames[index];
                images[i].rectTransform.localPosition = markPositions[index];
            }

            Image playImage = CreateImage(NineNineCore.Assets.Get<Sprite>("MainMenu_Button_Play"));
            playImage.name = "Play";
            playImage.rectTransform.localPosition = Vector3.up * -88f;
        }

        protected override void OnPress()
        {
            base.OnPress();
            WelcomeMenu.Instance.Close(immediately: false);
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
            if (animator == null)
            {
                animator = MainAnimation();
                StartCoroutine(animator);
            }
        }

        public override void PlayBackgroundAnimation()
        {
            base.PlayBackgroundAnimation();
            if (bgAnimators == null)
            {
                bgAnimators = new IEnumerator[4];
                float[] time = new float[4] { 0f, 0.5f, 0.4f, 0.1f };
                for (int i = 0; i < bgAnimators.Length; i++)
                {
                    bgAnimators[i] = BackgroundAnimation(images[i + 1], time[i], i % 2 == 0);
                    StartCoroutine(bgAnimators[i]);
                }
            }
        }

        private IEnumerator BackgroundAnimation(Image questionMark, float time, bool state)
        {
            Vector3 mainPos = questionMark.rectTransform.localPosition;
            float maxValue = 5f;
            float startValue = 0f;
            float duration = 1f;

            float value = 0f;
            float targetValue = state ? maxValue : -maxValue;

            while (true)
            {
                time += Time.unscaledDeltaTime / duration;
                float smoothStep = Mathf.SmoothStep(0f, 1f, time);
                value = Mathf.Lerp(startValue, targetValue, smoothStep);
                questionMark.rectTransform.localPosition = mainPos + Vector3.up * value;

                if (time >= 1f)
                {
                    value = targetValue;
                    state = !state;

                    time = 0f;
                    startValue = value;
                    targetValue = state ? maxValue : -maxValue;
                }

                yield return null;
            }
        }

        private IEnumerator MainAnimation()
        {
            Sprite[] sprites = new Sprite[]
            {
                NineNineCore.VanillaAssets.Get<Sprite>("Slap_Sheet_4"),
                NineNineCore.VanillaAssets.Get<Sprite>("Slap_Sheet_3"),
                NineNineCore.VanillaAssets.Get<Sprite>("Slap_Sheet_2"),
                NineNineCore.VanillaAssets.Get<Sprite>("Slap_Sheet_1"),
                NineNineCore.VanillaAssets.Get<Sprite>("Slap_Sheet_0"),
            };

            int counter = 2;
            while (counter > 0)
            {
                float time = 0.25f;
                while (time > 0f)
                {
                    time -= Time.unscaledDeltaTime;
                    yield return null;
                }

                MusicManager.Instance.PlaySoundEffect(NineNineCore.VanillaAssets.Get<SoundObject>("BAL_Slap"));
                images[0].sprite = sprites[0];
                time = 0.1f;
                while (time > 0f)
                {
                    time -= Time.unscaledDeltaTime;
                    yield return null;
                }
                float animationTime = 1f;
                while (animationTime > 0f)
                {
                    animationTime -= Time.unscaledDeltaTime * 2f;
                    if (animationTime < 0f) animationTime = 0f;
                    images[0].sprite = sprites[(int)(4 * (1f - animationTime))];
                    yield return null;
                }
                counter--;
            }

            animator = null;
        }
    }

    internal class WelcomeUpdateButton : WelcomeMenuButton
    {
        private Image arrowImage;
        private Image[] fileImages;

        private IEnumerator animator;
        private IEnumerator bgAnimator;

        public override void Initialize(object data)
        {
            base.Initialize(data);
            priority = 64;
            CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Updates")).name = "Background";
            fileImages = new Image[3];

            Vector3[] filePositions = new Vector3[] { new Vector3(-120f, -25f, 0f), new Vector3(-120f, -25f, 0f), 
                new Vector3(120f, 25f, 0f) };
            for (int i = 0; i < fileImages.Length; i++)
            {
                fileImages[i] = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>($"FileIcon_{i + 1}"), maskImage.rectTransform,
                    Vector3.zero, false);
                fileImages[i].name = fileImages[i].sprite.name;
                fileImages[i].rectTransform.SetCenterAnchor();
                fileImages[i].rectTransform.localPosition = filePositions[i];
            }

            arrowImage = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("SpinningArrows"), maskImage.rectTransform,
                Vector3.zero, false);
            arrowImage.name = "Arrows";
            arrowImage.rectTransform.SetCenterAnchor();

            TextMeshProUGUI updatesCenterText = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans24, "Update\nCenter", 
                maskImage.rectTransform, Vector3.zero, false);
            updatesCenterText.rectTransform.SetCenterAnchor();
            updatesCenterText.alignment = TextAlignmentOptions.Center;
            updatesCenterText.rectTransform.localPosition = Vector3.up * 87f;

            TextMeshProUGUI descriptionText = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans12, 
                "Revert the updates\nif they break\nthe game",
                maskImage.rectTransform, Vector3.zero, false);
            descriptionText.rectTransform.SetCenterAnchor();
            descriptionText.alignment = TextAlignmentOptions.Center;
            descriptionText.rectTransform.localPosition = Vector3.up * -87f;
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
            if (animator == null)
            {
                animator = MainAnimation();
                StartCoroutine(animator);
            }
        }

        public override void PlayBackgroundAnimation()
        {
            base.PlayBackgroundAnimation();
            if (bgAnimator == null)
            {
                bgAnimator = BackgroundAnimation();
                StartCoroutine(bgAnimator);
            }
        }

        private IEnumerator BackgroundAnimation()
        {
            float speed = 300f;
            float maxAxisValue = 120f;
            Vector3[] filePositions = new Vector3[fileImages.Length];
            for (int i = 0; i < fileImages.Length; i++)
            {
                filePositions[i] = fileImages[i].rectTransform.localPosition; 
            }
            int index = 0;
            while (true)
            {
                if (filePositions[index].x < 0f) // If the file comes from the left side
                {
                    while (fileImages[index].rectTransform.localPosition.x < maxAxisValue)
                    {
                        fileImages[index].rectTransform.localPosition += Vector3.right * Time.unscaledDeltaTime * speed;
                        yield return null;
                    }
                }
                else
                {
                    while (fileImages[index].rectTransform.localPosition.x > -maxAxisValue)
                    {
                        fileImages[index].rectTransform.localPosition -= Vector3.right * Time.unscaledDeltaTime * speed;
                        yield return null;
                    }
                }
                fileImages[index].rectTransform.localPosition = filePositions[index];
                index++;
                if (index > filePositions.Length - 1) index = 0;
            }
        }

        private IEnumerator MainAnimation()
        {
            MusicManager.Instance.PlaySoundEffect(NineNineCore.Assets.Get<SoundObject>("Mechanism"));
            float maxAngle = 720f;
            float speed = 360f;
            float value = 0f;
            while (value < maxAngle)
            {
                value += Time.unscaledDeltaTime * speed;
                if (value > maxAngle) value = maxAngle;
                arrowImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, -value);
                yield return null;
            }

            animator = null;
        }
    }

    internal class WelcomeExitButton : WelcomeMenuButton
    {
        private Image firstPrizeImage;
        private Image doorImage;
        private IEnumerator animator;
        private IEnumerator bgAnimator;
        private AudioManager audMan;

        public override void Initialize(object data)
        {
            base.Initialize(data);
            CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Exit")).name = "Background";

            doorImage = CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Exit_Door_2"));
            doorImage.name = "Door";
            doorImage.rectTransform.localPosition = new Vector3(32f, -10f, 0f);

            Image arrow = CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("Arrow_Sprite_Sheet_7"));
            arrow.name = "Arrow";
            arrow.rectTransform.localPosition = new Vector3(-15f, -13f, 0f);

            Image prizeMask = new GameObject("FirstPrizeMask").AddComponent<Image>();
            prizeMask.gameObject.layer = LayerUtils.UI;
            prizeMask.rectTransform.SetParent(maskImage.transform, false);
            prizeMask.rectTransform.localPosition = new Vector3(-18f, -113f, 0f);
            prizeMask.rectTransform.sizeDelta = new Vector2(150f, 300f);
            prizeMask.gameObject.AddComponent<Mask>().showMaskGraphic = false;

            firstPrizeImage = CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("1PrizeSprites_9"), prizeMask.transform);
            firstPrizeImage.name = "FirstPrize";
            firstPrizeImage.rectTransform.localPosition = new Vector3(-60, -60f, 0f);
            firstPrizeImage.rectTransform.localScale = Vector3.one * 0.5f;

            Image exitSign = CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("ExitSignSprite"));
            exitSign.name = "ExitSign";
            exitSign.rectTransform.localPosition = Vector3.up * 89f;

            audMan = ObjectUtils.AddAudioManager(gameObject, true, AudioManagerSources.Sfx, false);
        }

        protected override void OnPress()
        {
            base.OnPress();
            Application.Quit();
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
            if (animator == null)
            {
                animator = MainAnimation();
                StartCoroutine(animator);
            }
        }

        public override void PlayBackgroundAnimation()
        {
            base.PlayBackgroundAnimation();
            if (bgAnimator == null)
            {
                bgAnimator = BackgroundAnimation();
                StartCoroutine(bgAnimator);
            }
        }

        private IEnumerator BackgroundAnimation()
        {
            yield break;
        }

        private IEnumerator MainAnimation()
        {
            doorImage.sprite = NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Exit_Door_2");

            Vector3 startPos = firstPrizeImage.rectTransform.localPosition;
            Vector3 startScale = firstPrizeImage.rectTransform.localScale;
            Vector3 endPos = new Vector3(140f, 185f, 0f);
            Vector3 endScale = Vector3.one * 0.1f;
            float totalTime = 4f;

            audMan.QueueAudio(NineNineCore.VanillaAssets.Get<SoundObject>("1PR_Motor"));
            audMan.SetLoop(true);

            float time = totalTime;
            Vector3 direction = endPos - startPos;
            Vector3 additionalScale = endScale - startScale;
            while (time > 0f)
            {
                time -= Time.unscaledDeltaTime;
                if (time < 0f) time = 0f;
                float value = (totalTime - time) / totalTime;
                firstPrizeImage.rectTransform.localPosition = startPos + direction * value;
                firstPrizeImage.rectTransform.localScale = startScale + additionalScale * value;
                audMan.pitchModifier = 1f + value * 2.5f;
                yield return null;
            }

            audMan.FlushQueue(true);
            doorImage.sprite = NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Exit_Door_1");
            MusicManager.Instance.PlaySoundEffect(NineNineCore.VanillaAssets.Get<SoundObject>("Doors_StandardShut"));

            firstPrizeImage.rectTransform.localPosition = startPos;
            firstPrizeImage.rectTransform.localScale = startScale;
            animator = null;
        }
    }

    public class WelcomePollButton : WelcomeMenuButton
    {
        private Image balloonImage;
        private Image urlImage;
        private IEnumerator animator;
        private IEnumerator bgAnimator;
        private AudioManager audMan;
        private string url;

        public override void Initialize(object data)
        {
            base.Initialize(data);
            url = (string)data;
            priority = 96;
            CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_PollButton")).name = "Background";

            balloonImage = CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("QuestionMarkBalloon"));
            balloonImage.name = "Balloon";
            balloonImage.rectTransform.localPosition = new Vector3(-6f, -23f, 0f);
            balloonImage.rectTransform.localScale = Vector3.zero;

            TextMeshProUGUI descText = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans12, 
                "Take the survey", maskImage.transform, 
                    Vector3.zero, false);
            descText.rectTransform.SetCenterAnchor();
            descText.alignment = TextAlignmentOptions.Center;
            descText.rectTransform.localPosition = Vector3.up * -87f;

            urlImage = CreateImage(NineNineCore.Assets.Get<Sprite>("PageRedirectIcon"));
            urlImage.name = "PageRedirect";
            urlImage.rectTransform.localPosition = new Vector3(45f, 105f, 0f);
            urlImage.color = Color.yellow;

            audMan = ObjectUtils.AddAudioManager(gameObject, true, AudioManagerSources.Vfx, false);
        }

        protected override void OnPress()
        {
            base.OnPress();
            Application.OpenURL(url);
        }

        public override void InitializeStaticVersion()
        {
            base.InitializeStaticVersion();
            balloonImage.rectTransform.localScale = Vector3.one;
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
            if (animator == null)
            {
                animator = MainAnimation();
                StartCoroutine(animator);
            }
        }

        public override void PlayBackgroundAnimation()
        {
            base.PlayBackgroundAnimation();
            if (bgAnimator == null)
            {
                bgAnimator = BackgroundAnimation();
                StartCoroutine(bgAnimator);
            }
        }

        private IEnumerator MainAnimation()
        {
            float duration = 0.75f;

            audMan.PlaySingle(NineNineCore.VanillaAssets.Get<SoundObject>("MatchBalloon_Revealed"));
            Vector3 scale = new Vector3(1f, 0f, 1f);
            while (scale.y < 1f)
            {
                scale.y += Time.unscaledDeltaTime / duration;
                if (scale.y > 1f) scale.y = 1f;
                balloonImage.rectTransform.localScale = scale;
                yield return null;
            }

            animator = null;
        }

        private IEnumerator BackgroundAnimation()
        {
            Vector3 mainPos = balloonImage.rectTransform.localPosition;
            float maxValue = 10f;
            float startValue = 0f;
            float duration = 2f;

            float time = 0f;
            bool state = true;
            float value = 0f;
            float targetValue = state ? maxValue : -maxValue;

            while (true)
            {
                time += Time.unscaledDeltaTime / duration;
                float smoothStep = Mathf.SmoothStep(0f, 1f, time);
                value = Mathf.Lerp(startValue, targetValue, smoothStep);
                balloonImage.rectTransform.localPosition = mainPos + Vector3.up * value;

                if (time >= 1f)
                {
                    value = targetValue;
                    state = !state;

                    time = 0f;
                    startValue = value;
                    targetValue = state ? maxValue : -maxValue;
                }

                yield return null;
            }
        }
    }
}