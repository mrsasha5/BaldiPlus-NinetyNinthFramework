using BaldiPlusNineNineFramework.Utilities;
using BepInEx;
using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace BaldiPlusNineNineFramework.Components
{
    public class AchievementsManager : Singleton<AchievementsManager>
    {
        internal Dictionary<string, List<AchievementInformation>> information = new Dictionary<string, List<AchievementInformation>>();

        private string FilePath => Path.Combine(Application.persistentDataPath);

        internal void OnFileNameLoaded()
        {
            AchievementDataManager.InitDirectories();
            AchievementDataManager.LoadGlobalAchievements();
            AchievementDataManager.LoadLocalAchievements();
        }

        internal void OnFileNameLoadFailed()
        {
            AchievementDataManager.InitDirectories();
            AchievementDataManager.LoadGlobalAchievements();
        }

        public void RegisterAchievement(AchievementInformation info)
        {
            ThrowExceptionUtils.ArgumentNull(info, "info");

            if (!information.ContainsKey(info.pluginInfo.Metadata.GUID)) 
                information.Add(info.pluginInfo.Metadata.GUID, new List<AchievementInformation>());

            information[info.pluginInfo.Metadata.GUID].Add(info);
            info.registered = true;
        }

        public Utilities.AchievementData AddAchievement(PluginInfo pluginInfo, string id)
        {
            if (HasAchievement(pluginInfo, id)) return null;

            Utilities.AchievementData data = new Utilities.AchievementData()
            {
                ModGuid = pluginInfo.Metadata.GUID,
                Id = id,
                GainedUtcTime = DateTime.UtcNow
            };
            AchievementDataManager.Data.Add(data);
            return data;
        }

        public T GetAchievementAdditionalData<T>(PluginInfo pluginInfo, string id, bool doNotCreate = false) where T : AchievementAdditionalData
        {
            Utilities.AchievementData data = GetAchievement(pluginInfo, id);
            if (data == null) data = AddAchievement(pluginInfo, id);

            return data != null ? (T)data.Data : null;
        }

        public bool RemoveAchievement(PluginInfo pluginInfo, string id)
        {
            if (!HasAchievement(pluginInfo, id)) return false;

            for (int i = 0; i < AchievementDataManager.Data.Count; i++)
            {
                if (AchievementDataManager.Data[i].ModGuid == pluginInfo.Metadata.GUID && AchievementDataManager.Data[i].Id == id)
                {
                    AchievementDataManager.Data.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public Utilities.AchievementData GetAchievement(PluginInfo pluginInfo, string id)
        {
            for (int i = 0; i < AchievementDataManager.Data.Count; i++)
            {
                if (AchievementDataManager.Data[i].ModGuid == pluginInfo.Metadata.GUID && AchievementDataManager.Data[i].Id == id)
                {
                    return AchievementDataManager.Data[i];
                }
            }
            return null;
        }

        public bool HasAchievement(PluginInfo pluginInfo, string id)
        {
            Utilities.AchievementData achievement = GetAchievement(pluginInfo, id);
            return achievement == null ? false : achievement.Data != null && achievement.Data.IsAchievementCompleted();
        }
    }

    public class AchievementInformation
    {
        private const string ACCESS_DENIED_EXCEPTION = "this value cannot be changed after achievement registration " +
            "since it handles serialization process.";
        private const string REGISTRATION_DENIED_EXCEPTION = "you cannot register a same achievement twice.";

        // Main data
        internal PluginInfo pluginInfo;
        internal bool globalSaveSystem;
        internal string id;
        internal AchievementAdditionalData clonableAdditionalData;
        // Cosmetic variables
        internal string nameKey, descriptionKey;
        internal Sprite icon, hiddenIcon;
        internal string[] categories;
        // Cache data
        internal bool registered;

        public AchievementInformation(PluginInfo pluginInfo, string id)
        {
            ThrowExceptionUtils.ArgumentNull(pluginInfo, "pluginInfo");
            this.pluginInfo = pluginInfo;
            this.id = id;
        }

        public AchievementInformation SetGlobal(bool value)
        {
            if (registered) throw new AccessViolationException(ACCESS_DENIED_EXCEPTION);
            globalSaveSystem = value;
            return this;
        }

        public AchievementInformation SetNameKey(string value)
        {
            nameKey = value;
            return this;
        }

        public AchievementInformation SetDescriptionKey(string value)
        {
            descriptionKey = value;
            return this;
        }

        public AchievementInformation SetIcon(Sprite value)
        {
            icon = value;
            return this;
        }

        public AchievementInformation SetHiddenIcon(Sprite value)
        {
            hiddenIcon = value;
            return this;
        }

        public AchievementInformation SetDefaultHiddenIcon() => SetHiddenIcon(NineNineCore.Assets.Get<Sprite>("HiddenAchievementIcon"));

        public AchievementInformation SetAdditionalData(AchievementAdditionalData value)
        {
            if (registered) throw new AccessViolationException(ACCESS_DENIED_EXCEPTION);
            clonableAdditionalData = value;
            return this;
        }

        public AchievementInformation SetCategoryIdentifiers(params string[] values)
        {
            categories = values;
            return this;
        }

        public AchievementInformation Register()
        {
            if (registered) throw new AccessViolationException(REGISTRATION_DENIED_EXCEPTION);
            AchievementsManager.Instance.RegisterAchievement(this);
            return this;
        }
    }
}