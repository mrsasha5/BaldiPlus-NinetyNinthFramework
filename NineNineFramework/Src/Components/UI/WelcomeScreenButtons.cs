using UnityEngine;
using UnityEngine.UI;

namespace BaldiPlusNineNineFramework.Components.UI
{
    public class WelcomePlayButton : WelcomeScreenButton
    {
        public override void Initialize()
        {
            base.Initialize();
            priority = 128;
            InitializeBackground(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Play"));
        }
    }

    public class WelcomeUpdateButton : WelcomeScreenButton
    {
        public override void Initialize()
        {
            base.Initialize();
            priority = 64;
            InitializeBackground(NineNineCore.Assets.Get<Sprite>("WelcomeMenu_Updates"));
        }
    }
}
