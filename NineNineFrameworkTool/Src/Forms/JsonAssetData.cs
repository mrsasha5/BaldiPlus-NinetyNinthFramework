using NineNineFrameworkTool.Unity;
using NineNineFrameworkTool.Utils;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace NineNineFrameworkTool.Forms
{
    internal enum AssetType
    {
        None,
        Texture2D,
        Sprite,
        SoundObject,
        AudioClip,
        SpriteSheet
    }

    // Editor-only enum
    internal enum JsonLoadStatus
    {
        NotFound,
        Successful,
        Failed
    }

    internal class JsonAssetData
    {
        // Fields for editor only
        [JsonIgnore]
        public string Path { get; set; } = null!;

        [JsonIgnore]
        public JsonLoadStatus LoadStatus { get; set; }

        [JsonIgnore]
        public ListViewItem UIItem { get; set; } = null!;

        // Serializable fields
        [JsonPropertyName("name")]
        [JsonPropertyOrder(2)]
        public string Name { get; set; } = null!;

        [JsonPropertyName("assetType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyOrder(0)]
        public AssetType AssetType { get; set; }

        [JsonPropertyName("formatVersion")]
        [JsonPropertyOrder(1)]
        public Version Version { get; set; } = null!;

        // Controls when editor can show properties of mass selected files.
        public virtual bool IsSame(object obj)
        {
            JsonAssetData generalData = (JsonAssetData)obj;
            if (AssetType != generalData.AssetType) return false;
            if (Name != generalData.Name) return false;
            if (LoadStatus == JsonLoadStatus.Failed && generalData.LoadStatus != JsonLoadStatus.Failed) return false;
            if (LoadStatus != JsonLoadStatus.Failed && generalData.LoadStatus == JsonLoadStatus.Failed) return false;

            return true;
        }

        // Used for updating subitems.
        // For some WEIRDEST reason Microsoft suggests us write two letters of abbreviations completely UPPERCASE.
        // Probably because one-character words simply don't exist, but for me, it's still a strange decision.
        public virtual void UpdateUI()
        {
            UIItem.SubItems.Clear();
            UIItem.Text = System.IO.Path.GetFileName(Path);
            switch (LoadStatus)
            {
                case JsonLoadStatus.Successful:
                    UIItem.SubItems.Add("Successful");
                    break;
                case JsonLoadStatus.NotFound:
                    UIItem.SubItems.Add("Not Found");
                    break;
                case JsonLoadStatus.Failed:
                    UIItem.SubItems.Add("Error");
                    break;
            }
            UIItem.SubItems.Add(AssetType.ToString());
        }

        public virtual void LoadProperties(JsonAssetEditorForm form)
        {
            form._assetNameTextBox.Text = Name;
            form._assetTypeComboBox.Text = AssetType.ToString();
        }
        public virtual void ApplyProperties(JsonAssetEditorForm form)
        {
            Name = form._assetNameTextBox.Text.GetNullIfEmpty()!;
        }

        public static JsonAssetData InitAssetOfType(AssetType type, string path, ListViewItem uiItem, JsonLoadStatus status)
        {
            switch (type)
            {
                case AssetType.Texture2D:
                    return new TextureJsonAssetData() { AssetType = AssetType.Texture2D, Path = path, UIItem = uiItem, LoadStatus = status };
                case AssetType.Sprite:
                    return new TextureJsonAssetData() { AssetType = AssetType.Sprite, Path = path, UIItem = uiItem, LoadStatus = status };
                case AssetType.SpriteSheet:
                    return null!; // new TextureJsonAssetData() { AssetType = AssetType.Sprite, Path = path, UIItem = uiItem, LoadStatus = status };
                case AssetType.SoundObject:
                    return new SoundObjectJsonAssetData() { AssetType = AssetType.SoundObject, Path = path, UIItem = uiItem, LoadStatus = status };
                case AssetType.AudioClip:
                    return new JsonAssetData() { AssetType = type, Path = path, UIItem = uiItem, LoadStatus = status };
                default:
                    return new JsonAssetData() { AssetType = type, Path = path, UIItem = uiItem, LoadStatus = status };
            }
        }
        public static JsonAssetData Load(string path)
        {
            JsonAssetData baseData = Load<JsonAssetData>(path);
            JsonAssetData resultData = baseData;
            switch (baseData.AssetType)
            {
                case AssetType.Texture2D:
                    resultData = Load<TextureJsonAssetData>(path);
                    break;
                case AssetType.Sprite:
                    resultData = Load<TextureJsonAssetData>(path);
                    break;
                case AssetType.SoundObject:
                    resultData = Load<SoundObjectJsonAssetData>(path);
                    break;
            }

            return resultData;
        }

        public static T Load<T>(string path) where T : JsonAssetData, new()
        {
            string jsonPath = path.ReplaceExtensionPath("json");

            if (File.Exists(jsonPath))
            {
                T data = null!;
                using (FileStream stream = new FileStream(jsonPath, FileMode.Open))
                {
                    try
                    {
                        data = JsonSerializer.Deserialize<T>(stream)!;
                        data.Path = path;
                        data.LoadStatus = JsonLoadStatus.Successful;
                    }
                    catch { }
                }

                if (data == null)
                {
                    data = new T()
                    {
                        Path = path,
                        LoadStatus = JsonLoadStatus.Failed
                    };
                }

                return data;
            }
            else
            {
                T data = new T()
                {
                    Path = path,
                    LoadStatus = JsonLoadStatus.NotFound
                };

                return data;
            }
        }

        public void DeleteJsonIfExists()
        {
            string jsonPath = Path.ReplaceExtensionPath("json");
            if (File.Exists(jsonPath))
            {
                File.Delete(jsonPath);
                LoadStatus = JsonLoadStatus.NotFound;
            }
        }
    }

    internal class TextureJsonAssetData : JsonAssetData
    {
        public const string LastVersion = "1.0.0.0";

        [JsonPropertyName("format")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyOrder(3)]
        public TextureFormat Format { get; set; } = TextureFormat.RGBA32;

        [JsonPropertyName("pixelsPerUnit")]
        [JsonPropertyOrder(4)]
        public float PixelsPerUnit { get; set; } = 1f;

        [JsonPropertyName("center")]
        [JsonPropertyOrder(5)]
        public Vector2 Center { get; set; } = new Vector2(0.5f, 0.5f);

        public TextureJsonAssetData()
        {
            Version = new Version(LastVersion);
        }

        public override bool IsSame(object obj)
        {
            if (!base.IsSame(obj)) return false;

            TextureJsonAssetData data = (TextureJsonAssetData)obj;
            if (Format != data.Format) return false;
            if (PixelsPerUnit != data.PixelsPerUnit) return false;
            if (Center != data.Center) return false;

            return true;
        }

        public static void RemoveUnusedProperties(JsonTypeInfo typeInfo)
        {
            if (typeInfo.Type != typeof(TextureJsonAssetData))
                return;

            typeInfo.Properties.First(x => x.Name == "pixelsPerUnit").ShouldSerialize = static (obj, value) =>
                ((TextureJsonAssetData)obj).AssetType == AssetType.Sprite;
            typeInfo.Properties.First(x => x.Name == "center").ShouldSerialize = static (obj, value) =>
                ((TextureJsonAssetData)obj).AssetType == AssetType.Sprite;
        }

        public override void ApplyProperties(JsonAssetEditorForm form)
        {
            base.ApplyProperties(form);
            // Texture properties
            if (Enum.TryParse(form._formatComboBox.Text, out TextureFormat format)) Format = format;
            // Sprite properties
            if (float.TryParse(form._pixelsPerUnitTextBox.Text, CultureInfo.InvariantCulture.NumberFormat, out float result)) 
                PixelsPerUnit = result;
            Vector2? center = Vector2.GetFromString(form._centerTextBox.Text.Replace(',', '.'));
            if (center != null!) Center = center.Value;
        }

        public override void LoadProperties(JsonAssetEditorForm form)
        {
            base.LoadProperties(form);
            form._textureSpritePanel.Visible = true;
            form._formatComboBox.Text = Format.ToString();

            if (AssetType == AssetType.Sprite)
            {
                form._sprTitleLabel.Enabled = form._pixelsPerUnitLabel.Enabled = form._centerLabel.Enabled = true;
                form._pixelsPerUnitTextBox.Enabled = form._centerTextBox.Enabled = true;
                form._pixelsPerUnitTextBox.Text = PixelsPerUnit.ToString(CultureInfo.InvariantCulture.NumberFormat);
                form._centerTextBox.Text = Center.GetAsString();
            }
            else
            {
                form._sprTitleLabel.Enabled = form._pixelsPerUnitLabel.Enabled = form._centerLabel.Enabled = false;
                form._pixelsPerUnitTextBox.Enabled = form._centerTextBox.Enabled = false;
                form._pixelsPerUnitTextBox.Clear();
                form._centerTextBox.Clear();
            }
        }
    }

    internal class SpriteSheetJsonAssetData : JsonAssetData
    {
        public const string LastVersion = "1.0.0.0";

        public SpriteSheetJsonAssetData()
        {
            Version = new Version(LastVersion);
        }
    }

    internal class SoundObjectJsonAssetData : JsonAssetData
    {
        public const string LastVersion = "1.0.0.0";

        [JsonPropertyName("soundKey")]
        [JsonPropertyOrder(5)]
        public string SoundKey { get; set; } = null!;

        [JsonPropertyName("additionalKeys")]
        [JsonPropertyOrder(7)]
        public SubtitleTimedKey[] AdditionalKeys { get; set; } = new SubtitleTimedKey[0];

        [JsonPropertyName("soundType")]
        [JsonPropertyOrder(3)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SoundType AudioType { get; set; } = SoundType.Effect;

        // 0f = no subtitles
        // -1f = auto-duration
        [JsonPropertyName("subDuration")]
        [JsonPropertyOrder(8)]
        public float SubtitleDuration { get; set; } = 0f;

        [JsonPropertyName("volumeMultiplier")]
        [JsonPropertyOrder(4)]
        public float VolumeMultiplier { get; set; } = 1f;

        [JsonPropertyName("color")]
        [JsonPropertyOrder(9)]
        public Unity.Color Color { get; set; } = new Unity.Color(1f, 1f, 1f, 1f);

        [JsonPropertyName("encrypted")]
        [JsonPropertyOrder(6)]
        public bool Encrypted { get; set; }

        [JsonPropertyName("addToMemory")]
        [JsonPropertyOrder(10)]
        public bool AddToMemory { get; set; } = true;

        public SoundObjectJsonAssetData()
        {
            Version = new Version(LastVersion);
        }

        public override bool IsSame(object obj)
        {
            if (!base.IsSame(obj)) return false;

            SoundObjectJsonAssetData data = (SoundObjectJsonAssetData)obj;
            if (AudioType != data.AudioType) return false;
            if (VolumeMultiplier != data.VolumeMultiplier) return false;
            if (SoundKey != data.SoundKey) return false;
            if (!CompareKeyArrays(AdditionalKeys, data.AdditionalKeys)) return false;
            if (SubtitleDuration != data.SubtitleDuration) return false;
            if (Color != data.Color) return false;
            if (Encrypted != data.Encrypted) return false;
            if (AddToMemory != data.AddToMemory) return false;

            return true;
        }

        private bool CompareKeyArrays(SubtitleTimedKey[] left, SubtitleTimedKey[] right)
        {
            if (left.Length != right.Length) return false;

            for (int i = 0; i < left.Length; i++)
            {
                if (!CompareKeys(left[i], right[i])) return false;
            }

            return true;
        }

        private bool CompareKeys(SubtitleTimedKey left, SubtitleTimedKey right)
        {
            if (left.Key != right.Key) return false;
            if (left.Time != right.Time) return false;
            if (left.Encrypted != right.Encrypted) return false;

            return true;
        }

        public override void ApplyProperties(JsonAssetEditorForm form)
        {
            base.ApplyProperties(form);
            // Main properties
            if (Enum.TryParse(form._soundTypeComboBox.Text, out SoundType type)) AudioType = type;
            if (float.TryParse(form._volumeTextBox.Text, CultureInfo.InvariantCulture.NumberFormat, out float volume))
                VolumeMultiplier = volume;
            // Subtitle properties
            SoundKey = form._soundKeyTextBox.Text.GetNullIfEmpty()!;
            Encrypted = form._encryptedCheckBox.Checked;
            if (float.TryParse(form._subtitleDurationTextBox.Text, CultureInfo.InvariantCulture.NumberFormat, out float subtitleDuration))
                SubtitleDuration = subtitleDuration;
            Unity.Color? color = Unity.Color.GetFromStringNoPercents(form._subtitleColorTextBox.Text);
            if (color != null) Color = color.Value;
        }

        public override void LoadProperties(JsonAssetEditorForm form)
        {
            base.LoadProperties(form);
            form._soundPanel.Visible = true;
            form._soundTypeComboBox.Text = AudioType.ToString();
            form._volumeTextBox.Text = VolumeMultiplier.ToString(CultureInfo.InvariantCulture.NumberFormat);
            form._soundKeyTextBox.Text = SoundKey;
            form._encryptedCheckBox.Checked = Encrypted;
            form._subtitleDurationTextBox.Text = SubtitleDuration.ToString(CultureInfo.InvariantCulture.NumberFormat);
            form._subtitleColorTextBox.Text = Color.GetAsStringNoPercents();
            form._addToMemoryCheckBox.Checked = AddToMemory;
        }

        public class SubtitleTimedKey
        {
            [JsonPropertyName("key")]
            public string Key { get; set; } = null!;

            [JsonPropertyName("time")]
            public float Time { get; set; }

            [JsonPropertyName("encrypted")]
            public bool Encrypted { get; set; }
        }

        public enum SoundType
        {
            Voice,
            Effect,
            Music
        }
    }

    internal class AudioClipJsonAssetData : JsonAssetData
    {
        public const string LastVersion = "1.0.0.0";

        public AudioClipJsonAssetData()
        {
            Version = new Version(LastVersion);
        }
    }
}