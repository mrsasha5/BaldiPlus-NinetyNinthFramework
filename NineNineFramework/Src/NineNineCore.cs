using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BaldiPlusNineNineFramework.Components.UI;
using BaldiPlusNineNineFramework.Utilities;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.Registers;
using UnityEngine;

namespace BaldiPlusNineNineFramework
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class NineNineCore : BaseUnityPlugin
    {
        public const string GUID = "mrsasha5.baldiplus.ninetyninthframework";
        public const string NAME = "Ninety-Ninth Framework";
        public const string VERSION = "0.1.0.0";

        private const string CACHE_FOLDER = "Cache";

        internal static string CachePath { get; private set; } = Path.Combine(Application.persistentDataPath, GUID, CACHE_FOLDER);
        internal static string AudioCachePath { get; set; } = Path.Combine(CachePath, "Audio");

        internal static AssetManager VanillaAssets { get; private set; } = new AssetManager();
        internal static AssetManager Assets { get; private set; } = new AssetManager();

        private void Awake()
        {
            Harmony harmony = new Harmony(GUID);
            harmony.PatchAllConditionals();
            LoadingEvents.RegisterOnAssetsLoaded(Info, BaseLoader(AssetLoader()), LoadingEventOrder.Start);

            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, GUID));
            Directory.CreateDirectory(CachePath);
            Directory.CreateDirectory(AudioCachePath);

            WelcomeScreen.RegisterButton<WelcomePlayButton>();
            WelcomeScreen.RegisterButton<WelcomeUpdateButton>();
            WelcomeScreen.RegisterButton<WelcomeUpdateButton>();
            WelcomeScreen.RegisterButton<WelcomeUpdateButton>();
        }

        internal static void OnSceneUnload()
        {
            LayerUtils.Initialize();
            Assets.Add("WelcomeMenu_Play", AssetUtils.LoadMyEmbeddedSprite("Textures/UI/WelcomeMenu_Play.png"));
            Assets.Add("WelcomeMenu_Updates", AssetUtils.LoadMyEmbeddedSprite("Textures/UI/WelcomeMenu_Updates.png"));
            Assets.Add("WelcomeMenu_ButtonMask", AssetUtils.LoadMyEmbeddedSprite("Textures/UI/WelcomeMenu_ButtonMask.png"));
            VanillaAssets.AddFromResourcesNoClones<Sprite>();
            VanillaAssets.AddFromResourcesNoClones<SoundObject>();
        }

        private IEnumerator AssetLoader()
        {
            yield return 1;
            yield return "Loading game assets...";
        }

        private IEnumerator BaseLoader(IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}