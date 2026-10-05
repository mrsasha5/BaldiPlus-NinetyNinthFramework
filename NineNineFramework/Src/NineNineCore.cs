using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BaldiPlusNineNineFramework.Components.UI;
using BaldiPlusNineNineFramework.Managers;
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
        internal static NineNineCore Instance { get; private set; }
        internal static bool CoreInitialized { get; private set; }

        private void Awake()
        {
            Instance = this;
            Harmony harmony = new Harmony(GUID);
            harmony.PatchAllConditionals();
            ConfigManager.Initialize();
            LoadingEvents.RegisterOnAssetsLoaded(Info, BaseLoader(AssetLoader()), LoadingEventOrder.Start);
            //LoadingEvents.RegisterOnAssetsLoaded(Info, BaseLoader(MainLoader()), LoadingEventOrder.Pre);

            ContentFactory.InitializeMainPaths();
            ContentFactory.RegisterWelcomeButtons();
            Registers.OnSingletonsInstantiated += ContentFactory.InitializeSingletons;
        }

        internal static void OnSceneUnload()
        {
            LayerUtils.Initialize();
            ContentFactory.InitializeAssets();
            ContentFactory.InitializePrefabs();
            CoreInitialized = true;
        }

        private IEnumerator MainLoader()
        {
            yield return 1;
            //yield return "";
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