using System;

namespace BaldiPlusNineNineFramework.Utilities
{
    internal class ExceptionUtils
    {
        public static void ThrowIfArgumentNull(object obj, string name)
        {
            if (obj == null) throw new ArgumentNullException(name);
        }
    }
}
