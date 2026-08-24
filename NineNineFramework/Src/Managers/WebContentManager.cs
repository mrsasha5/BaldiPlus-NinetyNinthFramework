using BaldiPlusNineNineFramework.Components.UI;
using System.Collections;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Managers
{
    internal class WebContentManager
    {
        public static void LoadWelcomeMenuPollButton()
        {
            WelcomeMenu.Instance.StartCoroutine(WelcomePollButtonLoader());
        }

        private static IEnumerator WelcomePollButtonLoader()
        {
            // TODO: add here actual web request
            float time = 7f;
            while (time > 0f)
            {
                time -= Time.unscaledDeltaTime;
                yield return null;
            }
            //WelcomeScreen.CreateButtonDynamically<WelcomePollButton>();
        }
    }
}
