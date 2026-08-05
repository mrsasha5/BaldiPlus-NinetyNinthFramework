// Probably comes back to this someday

/*using Microsoft.Win32;
using System.Text;

namespace NineNineFrameworkTool.Utils
{
    internal class RegistryManager
    {
#if true // My failed attempt
        public static bool CheckIfGuidExists(string guid)
        {
            guid = "{" + guid + "}";
            RegistryKey? key = Registry.ClassesRoot.OpenSubKey(@$"CLSID\{guid}\LocalServer32", true);
            key?.Close();
            return key != null;
        }

        public static void RegisterGuid(string guid)
        {
            guid = "{" + guid + "}";
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(
                    @$"CLSID\{guid}\LocalServer32", true))
            {
                key.SetValue(null, Application.ExecutablePath);
            }
        }

        public static void RegisterDropTarget(string winId, string name, string guid, string[] extensions)
        {
            guid = "{" + guid + "}";
            for (int i = 0; i < extensions.Length; i++)
            {
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@$".{extensions[i]}\shell\{winId}", true))
                {
                    key.SetValue("MUIVerb", name);
                    using (RegistryKey subKey = Registry.ClassesRoot.CreateSubKey(@$".{extensions[i]}\shell\{winId}\DropTarget", true))
                    {
                        subKey.SetValue("Clsid", guid);
                    }
                }
            }
        }
#endif

        // My actually succesful and at the same time failed attempt

        public static bool CheckIfContextMenuRegistered(string winId, string[] extensions)
        {
            for (int i = 0; i < extensions.Length; i++)
            {
                if (Registry.ClassesRoot.OpenSubKey(@$"SystemFileAssociations\.{extensions[i]}\shell\{winId}") != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RegisterContextMenu(string winId, string name, string[] extensions, string[] commands)
        {
            for (int i = 0; i < extensions.Length; i++)
            {
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(
                    @$"SystemFileAssociations\.{extensions[i]}\shell\{winId}", true))
                {
                    key.SetValue("MUIVerb", name);
                    StringBuilder commandsValue = new StringBuilder();
                    for (int j = 0; j < commands.Length; j++)
                    {
                        commandsValue.Append(commands[j]);
                        commandsValue.Append(';');
                    }
                    key.SetValue("SubCommands", commandsValue.ToString());
                }
            }
        }

        public static void RegisterContextCommand(string winId, string name, string cmdCommand)
        {
            using (RegistryKey mainKey = Registry.LocalMachine.CreateSubKey(
                @$"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{winId}", true))
            {
                mainKey.SetValue("MUIVerb", name);
                using (RegistryKey subKey = mainKey.CreateSubKey("command"))
                {
                    subKey.SetValue(null, cmdCommand);
                }
            }
        }

        public static void UnregisterContextMenu(string winId, string[] extensions)
        {
            for (int i = 0; i < extensions.Length; i++)
            {
                Registry.ClassesRoot.DeleteSubKeyTree(@$"SystemFileAssociations\.{extensions[i]}\shell\{winId}");
            }
        }

        public static void UnregisterContextCommand(string winId)
        {
            Registry.LocalMachine.DeleteSubKeyTree(@$"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{winId}");
        }
    }
}*/