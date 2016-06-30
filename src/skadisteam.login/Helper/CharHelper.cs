using System;

namespace skadi_steam_login.Helper
{
    /// <summary>
    /// Class to help with chars.
    /// </summary>
    public static class CharHelper
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
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }
            return arr;
        }

        /// <summary>
        /// Get the Hex Value out of char.
        /// </summary>
        /// <param name="hex">
        /// Char to input.
        /// </param>
        /// <returns>
        /// An int.
        /// </returns>
        private static int GetHexVal(char hex)
        {
            var val = (int)hex;
            return val - (val < 58 ? 48 : 55);
        }
    }
}
