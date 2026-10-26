using System;

namespace BaldiPlusNineNineFramework.Utilities
{
    internal class ThrowExceptionUtils
    {
        public static void ArgumentNull(object obj, string name)
        {
            if (obj == null) throw new ArgumentNullException(name);
        }

        public static void StringArgumentNullOrEmpty(string value, string name)
        {
            if (value == null) throw new ArgumentNullException(name);
            if (value == string.Empty) throw new ArgumentException($"{name} is empty.");
        }
    }
}
