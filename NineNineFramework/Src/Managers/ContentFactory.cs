using BaldiPlusNineNineFramework.Components;
using BaldiPlusNineNineFramework.Components.UI;
using BaldiPlusNineNineFramework.Utilities;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Managers
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
            NineNineCore.VanillaAssets.AddFromResourcesNoClones<GameObject>();
        }

        public static void InitializeSingletons()
        {
            
        }

        public static void InitializePrefabs()
        {
            AchievementsMenu.InitializePrefab();
        }
    }
}
