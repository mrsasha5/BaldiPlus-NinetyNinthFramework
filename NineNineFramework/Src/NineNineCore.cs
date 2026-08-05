using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BaldiPlusNineNineFramework.Utilities;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
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

        internal static Dictionary<Assembly, PluginInfo> PluginAssemblies { get; private set; } = new Dictionary<Assembly, PluginInfo>();

        private void Awake()
        {
            Harmony harmony = new Harmony(GUID);
            harmony.PatchAllConditionals();
            LoadingEvents.RegisterOnAssetsLoaded(Info, BaseLoader(AssetLoader()), LoadingEventOrder.Start);

            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, GUID));
            Directory.CreateDirectory(CachePath);
            Directory.CreateDirectory(AudioCachePath);
            RegisterMyAssembly(Info);
        }

        /// <summary>
        /// Allows this plugin to understand who owns assembly without using heavy methods. Used for optimizations.
        /// Recommended to be called on the Awake().
        /// </summary>
        /// <param name="info"></param>
        public static void RegisterMyAssembly(PluginInfo info)
        {
            PluginAssemblies.Add(Assembly.GetCallingAssembly(), info);
        }

        private IEnumerator AssetLoader()
        {
            yield return 1;
            yield return "LOADING TEST.mp3!";
            AssetUtils.LoadEmbeddedAudioClip(Assembly.GetCallingAssembly(), "TEST.mp3");
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
