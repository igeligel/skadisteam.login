using System;

namespace skadisteam.login.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Hex to Byte method.
        /// </summary>
        /// <param name="hex">
        /// Hex value.
        /// </param>
        /// <returns>
        /// Hex Value as Byte[].
        /// </returns>
        public static byte[] HexToByte(this string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            var arr = new byte[hex.Length >> 1];
            var l = hex.Length;

            for (var i = 0; i < (l >> 1); ++i)
            {
                arr[i] = (byte)(((hex[i << 1].GetHexVal()) << 4) + ((hex[(i << 1) + 1]).GetHexVal()));
            }
            return arr;
        }
    }
}
