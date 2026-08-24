using BaldiPlusNineNineFramework.Extensions;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Utilities
{
    public class JsonAssetUtils
    {
        private static Dictionary<AssetType, Func<string, string, Assembly, AssetData>> loadingMethods = 
            new Dictionary<AssetType, Func<string, string, Assembly, AssetData>>()
        {
            { AssetType.Texture2D, LoadTexture2D },
            { AssetType.Sprite, LoadSprite },
            { AssetType.AudioClip, LoadAudioClip },
            { AssetType.SoundObject, LoadSoundObject }
        };

        public static AssetData LoadEmbeddedAsset(Assembly assembly, string path) => LoadAsset(assembly, path);
        public static AssetData LoadMyEmbeddedAsset(string path) => LoadAsset(Assembly.GetExecutingAssembly(), path);
        public static AssetData LoadAssetFromFile(string path) => LoadAsset(null, path);

        public static void LoadAssetsIntoAssetManager(AssetManager assetManager, Assembly assembly, string[] paths)
        {
            ExceptionUtils.ThrowIfArgumentNull(paths, "paths");
            ExceptionUtils.ThrowIfArgumentNull(assetManager, "assetManager");

            for (int i = 0; i < paths.Length; i++)
            {
                if (Path.GetExtension(paths[i]) == ".json") continue;

                AssetData asset;
                if (assembly == null) asset = LoadAssetFromFile(paths[i]);
                else asset = LoadEmbeddedAsset(assembly, paths[i]);
                asset.AddToAssetManager(assetManager);
            }
        }

        public static IEnumerator LoadAssetsIntoAssetManagerEnumerator(AssetManager assetManager, Assembly assembly, string[] paths)
        {
            ExceptionUtils.ThrowIfArgumentNull(paths, "paths");
            ExceptionUtils.ThrowIfArgumentNull(assetManager, "assetManager");

            for (int i = 0; i < paths.Length; i++)
            {
                if (Path.GetExtension(paths[i]) == ".json") continue;

                AssetData asset;
                if (assembly == null) asset = LoadAssetFromFile(paths[i]);
                else asset = LoadEmbeddedAsset(assembly, paths[i]);
                yield return asset.AddToAssetManager(assetManager);
            }
        }

        private static AssetData LoadAsset(Assembly assembly, string path)
        {
            ExceptionUtils.ThrowIfArgumentNull(path, "path");

            string jsonPath = path.ReplaceExtensionPath("json");
            string serializedData = null;

            if (assembly == null)
            {
                serializedData = File.ReadAllText(jsonPath);
            }
            else
            {
                using (Stream stream = assembly.GetManifestResourceStream(jsonPath))
                    using (StreamReader reader = new StreamReader(stream))
                        serializedData = reader.ReadToEnd();
            }

            AssetType assetType = ReadAssetType(serializedData);
            if (!loadingMethods.ContainsKey(assetType))
            {
                throw new Exception($"{assetType} is not supported.");
            }

            return loadingMethods[assetType].Invoke(path, serializedData, assembly);
        }

        private static AssetData LoadTexture2D(string path, string serializedData, Assembly assembly)
        {
            Texture2DAssetData data = JsonConvert.DeserializeObject<Texture2DAssetData>(serializedData);
            Texture2D tex = null;

            if (assembly == null)
                tex = AssetLoader.TextureFromFile(path, data.format);
            else
                tex = AssetUtils.LoadEmbeddedTexture(assembly, path, data.format);

            tex.name = SystemUtils.ReturnFirstNotNullableString(data.name, Path.GetFileNameWithoutExtension(path));
            data.path = path;
            data.texture = tex;
            return data;
        }

        private static AssetData LoadSprite(string path, string serializedData, Assembly assembly)
        {
            SpriteAssetData data = JsonConvert.DeserializeObject<SpriteAssetData>(serializedData);
            Sprite sprite = null;

            if (assembly == null)
                sprite = AssetLoader.SpriteFromFile(path, data.center, data.pixelsPerUnit);
            else
                sprite = AssetUtils.LoadEmbeddedSprite(assembly, path, data.pixelsPerUnit, data.center);

            sprite.name = SystemUtils.ReturnFirstNotNullableString(data.name, Path.GetFileNameWithoutExtension(path));
            data.path = path;
            data.texture = sprite.texture;
            data.sprite = sprite;
            return data;
        }

        private static AssetData LoadAudioClip(string path, string serializedData, Assembly assembly)
        {
            AudioClipAssetData data = JsonConvert.DeserializeObject<AudioClipAssetData>(serializedData);
            AudioClip audClip = null;

            if (assembly == null)
            {
                audClip = AssetLoader.AudioClipFromFile(path);
            }
            else
            {
                audClip = AssetUtils.LoadEmbeddedAudioClip(assembly, path);
            }
            audClip.name = SystemUtils.ReturnFirstNotNullableString(data.name, Path.GetFileNameWithoutExtension(path));
            data.path = path;
            data.clip = audClip;
            return data;
        }

        private static AssetData LoadSoundObject(string path, string serializedData, Assembly assembly)
        {
            SoundObjectAssetData data = JsonConvert.DeserializeObject<SoundObjectAssetData>(serializedData);
            AudioClip audClip = null;

            if (assembly == null)
            {
                audClip = AssetLoader.AudioClipFromFile(path);
            }
            else
            {
                audClip = AssetUtils.LoadEmbeddedAudioClip(assembly, path);
            }

            audClip.name = SystemUtils.ReturnFirstNotNullableString(data.name, Path.GetFileNameWithoutExtension(path));

            SoundObject soundObj = ObjectCreators.CreateSoundObject(audClip, data.soundKey, data.soundType, data.color,
                string.IsNullOrEmpty(data.soundKey) ? 0f : data.subtitleDuration);
            soundObj.name = audClip.name;
            soundObj.addToMemory = data.addToMemory;
            soundObj.volumeMultiplier = data.volumeMultiplier;
            soundObj.additionalKeys = data.additionalKeys;
            soundObj.encrypted = data.encrypted;
            data.path = path;
            data.clip = audClip;
            data.sound = soundObj;
            return data;
        }

        // Try to read it first instead of completely reading JSON data.
        private static AssetType ReadAssetType(string serializedData)
        {
            try
            {
                using (StringReader reader = new StringReader(serializedData))
                {
                    using (JsonTextReader jsonReader = new JsonTextReader(reader))
                    {
                        while (jsonReader.Read())
                        {
                            if (jsonReader.TokenType == JsonToken.PropertyName && jsonReader.Value is string val && val == "assetType")
                            {
                                jsonReader.Read();
                                return (AssetType)Enum.Parse(typeof(AssetType), (string)jsonReader.Value);
                            }
                        }
                    }
                }
                throw new Exception("Unable to read the assetType parameter from the JSON data!");
            }
            catch { throw new Exception("Unable to read the assetType parameter from the JSON data!"); }
        }
    }

    public class AssetUtils
    {
        private static int audioCounter = 0;

        public static List<string> GetEmbeddedStartingWith(Assembly assembly, string path)
        {
            List<string> paths = new List<string>();
            string[] resourceNames = assembly.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; i++)
            {
                if (resourceNames[i].StartsWith(path)) paths.Add(resourceNames[i]);
            }
            return paths;
        }

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
            string newPath = Path.Combine(NineNineCore.AudioCachePath, $"{audioCounter + 1}{Path.GetExtension(path)}");
            using (Stream stream = assembly.GetManifestResourceStream(path))
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
            
            Texture2D tex = null;
            using (Stream stream = assembly.GetManifestResourceStream(path))
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