using BepInEx;
using MTM101BaldAPI.AssetTools;
using BaldiPlusNineNineFramework.Utilities;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Extensions
{
    public class ExtendedAssetManager : AssetManager
    {
        protected BaseUnityPlugin plugin;

        public ExtendedAssetManager(BaseUnityPlugin plugin) : base()
        {
            this.plugin = plugin;
        }

        public ExtendedAssetManager(BaseUnityPlugin plugin, Type[] ignoreTypes) : base(ignoreTypes)
        {
            this.plugin = plugin;
        }

        /// <summary>
        /// Loads all textures and sprites from provided folder.
        /// Detects the difference between sprites and textures by checking if they follow the pattern "Name__ppu=99.png"
        /// </summary>
        public void LoadTexturesAndSpritesFrom(string path,
            SearchOption searchOption = SearchOption.AllDirectories, string searchPattern = "*.png")
        {

        }

        /// <summary>
        /// Splits texture and sprite loading into steps.
        /// Works same as <see cref="LoadTexturesAndSpritesFrom"/>.
        /// </summary>
        public IEnumerator ProceduralTextureAndSpritesLoad(string path,
            SearchOption searchOption = SearchOption.AllDirectories, string searchPattern = "*.png")
        {
            yield break;
        }

        public void LoadTexturesFromFolder(string path,
            SearchOption searchOption = SearchOption.AllDirectories, string searchPattern = "*.png")
        {
            string[] paths = Directory.GetFiles(Path.Combine(AssetLoader.GetModPath(plugin), path), searchPattern, searchOption);
            Texture2D[] textures = new Texture2D[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                textures[i] = AssetLoader.TextureFromMod(plugin, paths[i]);
            }
            AddRange(textures);
        }

        public void LoadSpritesFromFolder(string path,
            SearchOption searchOption = SearchOption.AllDirectories, string searchPattern = "*.png")
        {
            
        }
    }

    public static class AssetExtensions
    {
        
    }
}
