using System.Collections;
using System.IO;
using BaldiPlusNineNineFramework.Components;
using BaldiPlusNineNineFramework.Debugging;
using BaldiPlusNineNineFramework.Management;
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
        // Framework information
        public const string GUID = "mrsasha5.baldiplus.ninetyninthframework";
        public const string NAME = "Ninety-Ninth Framework";
        public const string VERSION = "0.1.0.0";

        private const string CORE_FOLDER = GUID;
        // Caching
        private const string CACHE_FOLDER = "Cache";
        private const string UNPACKED_EMBEDDED_ASSETS_FOLDER = "UnpackedEmbeddedAssets";
        // Data
        private const string GLOBAL_DATA_FOLDER = "GlobalData";

        internal static string CorePath { get; } = Path.Combine(Application.persistentDataPath, CORE_FOLDER);
        internal static string GlobalDataPath { get; } = Path.Combine(CorePath, GLOBAL_DATA_FOLDER);
        internal static string CachePath { get; } = Path.Combine(CorePath, CACHE_FOLDER);
        internal static string AudioCachePath { get; } = Path.Combine(CachePath, UNPACKED_EMBEDDED_ASSETS_FOLDER, "Audio");

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
            LoadingEvents.RegisterOnAssetsLoaded(Info, BaseLoader(MainLoader()), LoadingEventOrder.Pre);

            ContentFactory.InitializeMainPaths();
            ContentFactory.RegisterWelcomeButtons();
#if DEBUG
            gameObject.AddComponent<DebugMenu>();
#endif
        }

        internal static void OnSceneUnload()
        {
            LayerUtils.Initialize();
            ContentFactory.InitializeAssets();
            ContentFactory.InitializeSingletons();
            ContentFactory.InitializePrefabs();
            CoreInitialized = true;
        }

        private IEnumerator MainLoader()
        {
            yield return 1;
            yield return "Initializing achievements...";
            ContentFactory.InitializeAchievements();
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