using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace BaldiPlusNineNineFramework.Utilities
{
    internal enum AssetType
    {
        Texture2D,
        Sprite,
        SoundObject,
        AudioClip,
        SpriteSheet
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class AssetData
    {
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public AssetType type;

        [JsonProperty]
        public string name;
    }

    internal class Texture2DAssetData : AssetData
    {
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public TextureFormat format = TextureFormat.RGBA32;
    }

    internal class SpriteAssetData : Texture2DAssetData
    {
        [JsonProperty]
        public float pixelsPerUnit = 1f;

        [JsonProperty]
        public Vector3 center = Vector3.one / 2f;
    }

    internal class SoundObjectAssetData : AssetData
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
        public float subDuration = 0f;

        [JsonProperty]
        public float volumeMultiplier = 1f;

        [JsonProperty]
        public Color color = Color.white;

        [JsonProperty]
        public bool encrypted;

        [JsonProperty]
        public bool hasAnimation;

        [JsonProperty]
        public bool addToMemory = true;
    }
}