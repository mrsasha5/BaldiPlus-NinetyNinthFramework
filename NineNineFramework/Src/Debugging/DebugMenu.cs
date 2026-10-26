#if DEBUG
using BaldiPlusNineNineFramework.Components;
using BaldiPlusNineNineFramework.Utilities;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Debugging
{
    internal class DebugMenu : MonoBehaviour
    {
        private bool active;

        private void Update()
        {
            if (NineNineCore.CoreInitialized)
            {
                if (Input.GetKeyDown(KeyCode.F9))
                {
                    active = !active;
                    
                    if (active)
                    {
                        MusicManager.Instance.PlaySoundEffect(NineNineCore.Assets.Get<SoundObject>("Glitched_99"));
                    }
                    else
                    {
                        CursorManager.Instance.LockCursor();
                    }
                }
            }
        }

        private void OnGUI()
        {
            if (active)
            {
                CursorManager.Instance.UnlockCursor();
                if (GUI.Button(new Rect(10, 70, 200, 40), "Add test achievement progress"))
                {
                    AchievementsManager.Instance.GetAchievementAdditionalData<AchievementIntProgressData>(
                        NineNineCore.Instance.Info, "test_achievement").Value++;
                }
            }
        }
    }
}
#endif