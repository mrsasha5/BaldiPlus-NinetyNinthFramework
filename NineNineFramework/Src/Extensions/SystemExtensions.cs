using System.IO;
using System.Text;

namespace BaldiPlusNineNineFramework.Extensions
{
    public static class SystemExtensions
    {
        public static string ReplaceExtensionPath(this string path, string newExtension)
        {
            // This approach more friendly since it's not modifying the path much
            // Unlke what Path.Combine can do
            return path.Replace(Path.GetFileName(path), Path.GetFileNameWithoutExtension(path) + $".{newExtension}");
            //return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + $".{newExtension}");
        }

        public static string Replace(this string value, char oldChar, char newChar, int count)
        {
            StringBuilder builder = new StringBuilder(value);
            for (int i = 0; i < builder.Length; i++)
            {
                if (builder[i] == oldChar)
                {
                    builder[i] = newChar;
                    count--;

                    if (count <= 0) break;
                }
            }
            return builder.ToString();
        }

        public static string Replace(this string value, string oldString, string newString, int count)
        {
            StringBuilder builder = new StringBuilder(value);
            for (int i = 0; i < builder.Length; i++)
            {
                if (builder[i] == oldString[0])
                {
                    bool flag = true;
                    for (int j = 0; j < oldString.Length; j++)
                    {
                        if (builder[i + j] != oldString[j])
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        builder.Remove(i, oldString.Length);
                        builder.Insert(i, newString);
                        i += newString.Length - 1;
                        count--;
                    }

                    if (count <= 0) break;
                }
            }
            return builder.ToString();
        }
    }
}