namespace NineNineFrameworkTool.Data
{
    internal class ConfigData
    {
        public int Version { get; private set; } = 1;

        public bool RunOnStartup { get; set; }

        public bool NotifyIcon { get; set; } = true;

        public bool SendToSupport { get; set; }

        public void Read(BinaryReader reader)
        {
            Version = reader.ReadInt32();
            RunOnStartup = reader.ReadBoolean();
            NotifyIcon = reader.ReadBoolean();
            SendToSupport = reader.ReadBoolean();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write(RunOnStartup);
            writer.Write(NotifyIcon);
            writer.Write(SendToSupport);
        }
    }
}
