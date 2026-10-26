using BaldiPlusNineNineFramework.Components;
using BaldiPlusNineNineFramework.Components.UI;
using BaldiPlusNineNineFramework.Utilities;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Management
{
    internal class ContentFactory
    {
        public static void InitializeMainPaths()
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, NineNineCore.GUID));
            Directory.CreateDirectory(NineNineCore.AudioCachePath);
        }

        public static void RegisterWelcomeButtons()
        {
            WelcomeMenu.RegisterButtonOnInitialization<WelcomePlayButton>();
            WelcomeMenu.RegisterButtonOnInitialization<WelcomeUpdateButton>();
            WelcomeMenu.RegisterButtonOnInitialization<WelcomeExitButton>();
        }

        public static void InitializeAssets()
        {
            JsonAssetUtils.LoadAssetsIntoAssetManager(NineNineCore.Assets, Assembly.GetExecutingAssembly(),
                AssetUtils.GetEmbeddedStartingWith(Assembly.GetExecutingAssembly(), "JsonAssets").ToArray());

            // Init all vanilla assets
            NineNineCore.VanillaAssets.AddFromResourcesNoClones<Texture2D>();
            NineNineCore.VanillaAssets.AddFromResourcesNoClones<Sprite>();
            NineNineCore.VanillaAssets.AddFromResourcesNoClones<SoundObject>();
            NineNineCore.VanillaAssets.AddFromResourcesNoClones<AudioClip>();
            NineNineCore.VanillaAssets.AddFromResourcesNoClones<Material>();
        }

        public static void InitializeSingletons()
        {
            ObjectUtils.CreateSingleton<AchievementsManager>();

            Events.OnFileNameLoaded += AchievementsManager.Instance.OnFileNameLoaded;
            Events.OnFileNameLoadFailed += AchievementsManager.Instance.OnFileNameLoadFailed;
        }

        public static void InitializePrefabs()
        {
            AchievementsMenu.InitializePrefab();
        }

        public static void InitializeAchievements()
        {
            AchievementInformation testAchievement = new AchievementInformation(NineNineCore.Instance.Info, id: "test_achievement")
                .SetNameKey("Cool achievement") // You should use localization keys
                .SetDescriptionKey("Isn't it?") // but for now I put here raw text since I don't want spend the time right now
                .SetIcon(NineNineCore.VanillaAssets.Get<Sprite>("AlarmClockIcon_Large"))
                .SetDefaultHiddenIcon()
                .SetCategoryIdentifiers("items")
                .SetGlobal(false)
                .SetAdditionalData(new AchievementIntProgressData(minValue: 0, maxValue: 20))
                .Register();
        }
    }
}