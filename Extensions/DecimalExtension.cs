using System;

namespace Extensions
{
    public static class DecimalExtension
    {
        /// <summary>
        /// Truncate to length of digits
        /// </summary>
        /// <param name="value"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static decimal TruncateTo(this decimal value, uint digits)
        {
            int factor = (int)Math.Pow(10, digits);
            decimal d = Math.Truncate(value * factor) / factor;
            return d;
        }
    }
}
