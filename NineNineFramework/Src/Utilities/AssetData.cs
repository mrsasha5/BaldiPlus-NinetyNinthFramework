using MTM101BaldAPI.AssetTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Utilities
{
    public enum AssetType
    {
        Texture2D,
        Sprite,
        SoundObject,
        AudioClip,
        SpriteSheet
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class AssetData
    {
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public AssetType type;

        [JsonProperty]
        public string name;

        [JsonIgnore]
        public string path;

        public virtual string AddToAssetManager(AssetManager assetManager) { throw new NotImplementedException(); }
    }

    public class Texture2DAssetData : AssetData
    {
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public TextureFormat format = TextureFormat.RGBA32;

        [JsonIgnore]
        public Texture2D texture;

        public override string AddToAssetManager(AssetManager assetManager)
        {
            assetManager.Add(texture.name, texture);
            return Path.GetFileName(path);
        }
    }

    public class SpriteAssetData : Texture2DAssetData
    {
        [JsonProperty]
        public float pixelsPerUnit = 1f;

        [JsonProperty]
        public Vector3 center = Vector3.one / 2f;

        [JsonProperty]
        public bool includeTextureInAssetManager;

        [JsonIgnore]
        public Sprite sprite;

        public override string AddToAssetManager(AssetManager assetManager)
        {
            if (includeTextureInAssetManager) assetManager.Add(texture.name, texture);
            assetManager.Add(sprite.name, sprite);
            return Path.GetFileName(path);
        }
    }

    public class AudioClipAssetData : AssetData
    {
        [JsonIgnore]
        public AudioClip clip;

        public override string AddToAssetManager(AssetManager assetManager)
        {
            assetManager.Add(clip.name, clip);
            return Path.GetFileName(path);
        }
    }

    public class SoundObjectAssetData : AudioClipAssetData
    {
        [JsonProperty]
        public string soundKey;

        [JsonProperty]
        public SubtitleTimedKey[] additionalKeys = new SubtitleTimedKey[0];

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public SoundType soundType;

        // 0f = no subtitles
        // -1f = auto-duration
        [JsonProperty]
        public float subtitleDuration = 0f;

        [JsonProperty]
        public float volumeMultiplier = 1f;

        [JsonProperty]
        public Color color = Color.white;

        [JsonProperty]
        public bool encrypted;

        [JsonProperty]
        public bool addToMemory = true;

        [JsonIgnore]
        public bool includeClipInAssetManager;

        [JsonIgnore]
        public SoundObject sound;

        public override string AddToAssetManager(AssetManager assetManager)
        {
            if (includeClipInAssetManager) assetManager.Add(clip.name, clip);
            assetManager.Add(sound.name, sound);
            return Path.GetFileName(path);
        }
    }
}