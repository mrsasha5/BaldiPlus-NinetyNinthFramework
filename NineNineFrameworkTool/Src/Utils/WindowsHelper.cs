/*namespace NineNineFrameworkTool.Utils
{
    internal class WindowsHelper
    {
        public static void CreateShortcut(string originalPath, string targetPath, string arguments = "")
        {
            // Use the built-in Windows Script Host COM object dynamically
            Type shellType = Type.GetTypeFromProgID("WScript.Shell")!;
            dynamic shell = Activator.CreateInstance(shellType!)!;

            dynamic shortcut = shell.CreateShortcut(targetPath);
            shortcut.TargetPath = originalPath;
            shortcut.Arguments = arguments;
            shortcut.WorkingDirectory = Path.GetDirectoryName(originalPath);
            shortcut.Save();
        }
    }
}*/