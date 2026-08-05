using BaldiPlusNineNineFramework.Extensions;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Utilities
{
    public class JsonAssetUtils
    {
        private static Dictionary<AssetType, Func<string, string, Assembly, object>> loadingMethods = 
            new Dictionary<AssetType, Func<string, string, Assembly, object>>()
        {
            { AssetType.Texture2D, LoadTexture2D },
            { AssetType.Sprite, LoadSprite },
            { AssetType.SoundObject, LoadSoundObject }
        };

        public static object LoadEmbeddedAssetFrom(string path, Assembly assembly) => LoadAsset(path, assembly);
        public static object LoadMyEmbeddedAsset(string path) => LoadAsset(path, Assembly.GetExecutingAssembly());
        public static object LoadAssetFromFile(string path) => LoadAsset(path, null);

        private static object LoadAsset(string path, Assembly assembly)
        {
            ExceptionUtils.ThrowIfArgumentNull(path, "path");

            string jsonPath = path.ReplaceExtensionPath("json");
            AssetData data = null;
            string serializedData = null;

            if (File.Exists(jsonPath))
            {
                serializedData = File.ReadAllText(jsonPath);
                data = JsonConvert.DeserializeObject<AssetData>(serializedData);
            }
            else
            {
                return null;
            }

            if (!loadingMethods.ContainsKey(data.type))
            {
                throw new Exception($"{data.type} is not supported.");
            }

            return loadingMethods[data.type].Invoke(path, serializedData, assembly);
        }

        private static object LoadTexture2D(string path, string serializedData, Assembly assembly)
        {
            Texture2DAssetData data = JsonConvert.DeserializeObject<Texture2DAssetData>(serializedData);
            Texture2D tex = null;
            if (assembly == null)
                tex = AssetLoader.TextureFromFile(path, data.format);
            else
                tex = AssetUtils.LoadEmbeddedTexture(assembly, path, data.format);
            tex.name = string.IsNullOrEmpty(data.name) ? Path.GetFileNameWithoutExtension(path) : data.name;

            return tex;
        }

        private static object LoadSprite(string path, string serializedData, Assembly assembly)
        {
            SpriteAssetData data = JsonConvert.DeserializeObject<SpriteAssetData>(serializedData);
            Sprite sprite = null;
            if (assembly == null)
                sprite = AssetLoader.SpriteFromFile(path, data.center, data.pixelsPerUnit);
            else
                sprite = AssetUtils.LoadEmbeddedSprite(assembly, path, data.pixelsPerUnit, data.center);
            sprite.name = string.IsNullOrEmpty(data.name) ? Path.GetFileNameWithoutExtension(path) : data.name;

            return sprite;
        }

        private static object LoadSoundObject(string path, string serializedData, Assembly assembly)
        {
            SoundObjectAssetData data = JsonConvert.DeserializeObject<SoundObjectAssetData>(serializedData);
            AudioClip audClip = AssetLoader.AudioClipFromFile(path);
            SoundObject soundObj = ObjectCreators.CreateSoundObject(audClip, data.soundKey, data.soundType, data.color, data.subDuration);
            soundObj.hasAnimation = data.hasAnimation;
            soundObj.addToMemory = data.addToMemory;
            soundObj.volumeMultiplier = data.volumeMultiplier;
            soundObj.additionalKeys = data.additionalKeys;
            soundObj.encrypted = data.encrypted;

            return soundObj;
        }
    }

    public class AssetUtils
    {
        private static int audioCounter = 0;

        public static Texture2D LoadTextureFromBytes(byte[] bytes, TextureFormat format = TextureFormat.RGBA32)
        {
            Texture2D tex = null;
            tex = new Texture2D(2, 2, format, mipChain: false);
            tex.LoadImage(bytes);
            tex.filterMode = FilterMode.Point;
            return tex;
        }

        public static AudioClip LoadEmbeddedAudioClip(Assembly assembly, string path)
        {
            Stream stream = assembly.GetManifestResourceStream(path);
            string newPath = Path.Combine(NineNineCore.AudioCachePath, $"{audioCounter + 1}{Path.GetExtension(path)}");
            using (stream)
            {
                byte[] data = stream.ToByteArray();
                File.WriteAllBytes(newPath, data);
                audioCounter++;
            }

            AudioClip audClip = AssetLoader.AudioClipFromFile(newPath);
            audClip.name = Path.GetFileNameWithoutExtension(path);
            return audClip;
        }

        public static Texture2D LoadEmbeddedTexture(Assembly assembly, string path, TextureFormat format = TextureFormat.RGBA32)
        {
            ExceptionUtils.ThrowIfArgumentNull(assembly, "assembly");
            ExceptionUtils.ThrowIfArgumentNull(path, "path");

            Stream stream = assembly.GetManifestResourceStream(path);
            Texture2D tex = null;
            using (stream)
            {
                tex = LoadTextureFromBytes(stream.ToByteArray(), format);
                tex.name = Path.GetFileNameWithoutExtension(path);
            }

            return tex;
        }

        public static Sprite LoadEmbeddedSprite(Assembly assembly, string path, float pixelsPerUnit = 1f, Vector2? center = null)
        {
            ExceptionUtils.ThrowIfArgumentNull(assembly, "assembly");
            ExceptionUtils.ThrowIfArgumentNull(path, "path");

            if (center == null) center = Vector2.one / 2f;
            Vector2 _center = center.Value;

            Texture2D tex = LoadEmbeddedTexture(assembly, path);
            Sprite spr = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), _center, pixelsPerUnit, 0u, SpriteMeshType.FullRect);
            return spr;
        }

        public static Texture2D LoadMyEmbeddedTexture(string name, TextureFormat format = TextureFormat.RGBA32)
        {
            ExceptionUtils.ThrowIfArgumentNull(name, "name");
            return LoadEmbeddedTexture(Assembly.GetExecutingAssembly(), name, format);
        }

        public static Sprite LoadMyEmbeddedSprite(string name, float pixelsPerUnit = 1f, Vector2? center = null)
        {
            ExceptionUtils.ThrowIfArgumentNull(name, "name");
            return LoadEmbeddedSprite(Assembly.GetExecutingAssembly(), name, pixelsPerUnit, center);
        }

        public static T FindNoRuntime<T>(string name = null) where T : UnityEngine.Object
        {
            return Find<T>(includeRuntimeInstantiated: false, name);
        }

        public static T Find<T>(string name = null) where T : UnityEngine.Object
        {
            return Find<T>(includeRuntimeInstantiated: true, name);
        }

        public static T[] FindAllNoRuntime<T>(string name = null) where T : UnityEngine.Object
        {
            return FindAll<T>(includeRuntimeInstantiated: false, name);
        }

        public static T[] FindAll<T>(string name = null) where T : UnityEngine.Object
        {
            return FindAll<T>(includeRuntimeInstantiated: true, name);
        }

        private static T Find<T>(bool includeRuntimeInstantiated, string name = null) where T : UnityEngine.Object
        {
            T obj = Array.Find(Resources.FindObjectsOfTypeAll<T>(),
                x => (name == null || x.name == name) && (includeRuntimeInstantiated || (!includeRuntimeInstantiated && x.GetInstanceID() > 0)));
            if (obj == null)
                throw new Exception($"Cannot find any instance of type {typeof(T).FullName}" + (name == null ? "!" : $" named as {name}!"));
            return obj;
        }

        private static T[] FindAll<T>(bool includeRuntimeInstantiated, string name = null) where T : UnityEngine.Object
        {
            T[] arr = Array.FindAll(Resources.FindObjectsOfTypeAll<T>(),
                x => (name == null || x.name == name) && (includeRuntimeInstantiated || (!includeRuntimeInstantiated && x.GetInstanceID() > 0)));
            if (arr.Length == 0)
                throw new Exception($"Cannot find any instance of type {typeof(T).FullName}" + (name == null ? "!" : $" named as {name}!"));
            return arr;
        }
    }
}