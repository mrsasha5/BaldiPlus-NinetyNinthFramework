using BaldiPlusNineNineFramework.Components;
using MTM101BaldAPI.SaveSystem;
using System;
using System.Collections.Generic;
using System.IO;

namespace BaldiPlusNineNineFramework.Utilities
{
    internal class AchievementDataManager
    {
        // Ninety-Ninth Framework Achievement Data
        private const string saveFileName = "Achievements.nnfad";
        private const int formatVersion = 1;
        private static Dictionary<Type, object> initializedTypes = new Dictionary<Type, object>();
        
        private static string GlobalAchievementsPath { get; } = Path.Combine(NineNineCore.GlobalDataPath, saveFileName);
        // Initially wanted to init property through { get; } = ...;
        // but decided to leave it as simple getter because some mods can force game to reload the save file.
        private static string LocalAchievementsPath => Path.Combine(ModdedSaveSystem.GetSaveFolder(NineNineCore.Instance, 
            PlayerFileManager.Instance.fileName), saveFileName);

        public static List<AchievementData> Data { get; } = new List<AchievementData>();

        public static void InitDirectories()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(GlobalAchievementsPath));
            Directory.CreateDirectory(Path.GetDirectoryName(LocalAchievementsPath));
        }

        public static void LoadGlobalAchievements() => LoadAchievements(GlobalAchievementsPath);
        public static void LoadLocalAchievements() => LoadAchievements(LocalAchievementsPath);

        public static void CreateAdditionalDataType(Type type) => initializedTypes.Add(type, Activator.CreateInstance(type));

        private static bool LoadAchievements(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        int version = reader.ReadInt32();
                        int count = reader.ReadInt32();
                        while (count > 0)
                        {
                            Data.Add(AchievementData.Read(version, reader));
                            count--;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        private static void SaveAchievements(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(formatVersion);
                    writer.Write(Data.Count);
                    for (int i = 0; i < Data.Count; i++)
                    {
                        Data[i].Write(formatVersion, writer);
                    }
                }
            }
        }
    }

    public class AchievementData
    {
        public string ModGuid { get; internal set; }
        public string Id { get; internal set; }
        public DateTime GainedUtcTime { get; internal set; }
        public AchievementAdditionalData Data { get; internal set; }

        internal void Write(int version, BinaryWriter writer)
        {
            writer.Write(ModGuid);
            writer.Write(Id);
            writer.Write(GainedUtcTime.Ticks);
            if (Data != null)
            {
                // Custom data gets a full control
                writer.Write(true);
                Data.Write(version, writer);
            }
            else
            {
                writer.Write(false); // No additional data
            }
        }

        internal static AchievementData Read(int version, BinaryReader reader)
        {
            string modGuid = reader.ReadString();
            string id = reader.ReadString();
            long ticks = reader.ReadInt64();
            bool hasData = reader.ReadBoolean();

            List<AchievementInformation> infos = AchievementsManager.Instance.information[modGuid];
            AchievementInformation info = null;
            for (int i = 0; i < infos.Count; i++)
            {
                // Not checking GUID since gained list by GUID already
                if (infos[i].id == id)
                {
                    info = infos[i];
                    break;
                }
            }

            AchievementAdditionalData additionalData = null;
            if (hasData)
            {
                // Read customable achievement data
                additionalData = info.clonableAdditionalData.Constructor();
                additionalData.Read(version, reader);
            }

            AchievementData data = new AchievementData();
            data.ModGuid = modGuid;
            data.Id = id;
            data.GainedUtcTime = new DateTime(ticks, DateTimeKind.Utc);
            data.Data = additionalData;
            return data;
        }
    }

    public abstract class AchievementAdditionalData
    {
        public AchievementData data;

        /// <summary>
        /// Works as constructor, used instead of standard C# constructor to achieve a bit more performance.
        /// Must return a new instance of your class with default values.
        /// </summary>
        /// <returns></returns>
        public abstract AchievementAdditionalData Constructor();
        public abstract bool IsAchievementCompleted();
        public abstract void Read(int version, BinaryReader reader);
        public abstract void Write(int version, BinaryWriter writer);
    }

    public class AchievementIntProgressData : AchievementAdditionalData
    {
        private const int FORMAT_VERSION = 1;

        // Allows to get constant values from main source.
        private AchievementIntProgressData reference;

        // Non-serializable values.
        // They represent an information.
        private int minValue;
        private int maxValue;

        // Always have own version per own custom data
        // if you want backwards compatibility in your mods.
        // Nobody wants their data become lost in the future.
        public int Version { get; private set; }
        public int Value { get; set; }

        private AchievementIntProgressData() { }

        public AchievementIntProgressData(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public override AchievementAdditionalData Constructor()
        {
            AchievementIntProgressData data = new AchievementIntProgressData();
            data.Version = 1;
            data.reference = this;
            data.Value = minValue;
            return data;
        }

        public override bool IsAchievementCompleted()
        {
            return Value >= reference.minValue && Value <= reference.maxValue;
        }

        public override void Read(int version, BinaryReader reader)
        {
            this.Version = reader.ReadInt32();
            Value = reader.ReadInt32();
        }

        public override void Write(int version, BinaryWriter writer)
        {
            writer.Write(FORMAT_VERSION);
            writer.Write(Value);
        }
    }
}