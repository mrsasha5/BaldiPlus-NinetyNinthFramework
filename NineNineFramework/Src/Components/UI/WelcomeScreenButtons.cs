using BaldiPlusNineNineFramework.Extensions;
using MTM101BaldAPI.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Components.UI
{
    public class WelcomePlayButton : WelcomeScreenButton
    {
        private Image[] images = new Image[5];

        public override void Initialize()
        {
            base.Initialize();
            priority = 128;
            CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Play"));

            // Slap_Sheet_0 (5)
            images[0] = CreateImage(NineNineCore.VanillaAssets.Get<Sprite>("Slap_Sheet_4"));
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
    }

    public class WelcomeUpdateButton : WelcomeScreenButton
    {
        private Image arrowsImage;

        private IEnumerator animator;

        public override void Initialize()
        {
            base.Initialize();
            priority = 64;
            CreateImage(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Updates"));

            arrowsImage = UIHelpers.CreateImage(NineNineCore.Assets.Get<Sprite>("SpinningArrows"), maskImage.rectTransform,
                Vector3.zero, false);
            arrowsImage.name = "Arrows";
            arrowsImage.rectTransform.SetCenterAnchor();

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
            if (animator == null) StartCoroutine(ThumbnailAnimation());
        }

        private IEnumerator ThumbnailAnimation()
        {
            PlaySingleSound(NineNineCore.Assets.Get<SoundObject>("Mechanism"));
            float maxAngle = 720f;
            float speed = 360f;
            float value = 0f;
            while (value < maxAngle)
            {
                value += Time.unscaledDeltaTime * speed;
                if (value > maxAngle) value = maxAngle;
                arrowsImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, -value);
                yield return null;
            }

            animator = null;
        }
    }
}
