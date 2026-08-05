namespace NineNineFrameworkTool.Utils
{
    public static class CSharpExtensions
    {
        public static string ReplaceExtensionPath(this string path, string newExtension)
        {
            return Path.Combine(Path.GetDirectoryName(path)!, Path.GetFileNameWithoutExtension(path) + $".{newExtension}");
        }

        public static string? GetNullIfEmpty(this string value)
        {
            return value == string.Empty ? null : value;
        }
    }
}
