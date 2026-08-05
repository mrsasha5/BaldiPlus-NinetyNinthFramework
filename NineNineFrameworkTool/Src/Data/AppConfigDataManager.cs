using NineNineFrameworkTool.Utils;

namespace NineNineFrameworkTool.Data
{
    internal class AppConfigDataManager
    {
        public static ConfigData Data { get; private set; } = null!;

        private const string Extension = "appbindata";

        public static void Init()
        {
            Data = new ConfigData();
        }

        public static bool Load(out bool damaged)
        {
            damaged = false;
            string path = Application.ExecutablePath.ReplaceExtensionPath(Extension);
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        try
                        {
                            Data = new ConfigData();
                            Data.Read(reader);
                        }
                        catch
                        {
                            damaged = true;
                        }
                    }
                }

                return true;
            }
            
            return false;
        }

        public static void Save()
        {
            string path = Application.ExecutablePath.ReplaceExtensionPath(Extension);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    Data.Write(writer);
                }
            }
        }
    }
}
