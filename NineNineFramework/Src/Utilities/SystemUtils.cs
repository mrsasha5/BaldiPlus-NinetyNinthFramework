using System;

namespace BaldiPlusNineNineFramework.Utilities
{
    public class SystemUtils
    {
        public static string ReturnFirstNotNullableString(params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null)
                    return values[i];
            }
            throw new Exception("All values are nullable!");
        }
    }
}
