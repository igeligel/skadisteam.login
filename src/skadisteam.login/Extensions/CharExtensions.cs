namespace skadisteam.login.Extensions
{
    internal static class CharExtensions
    {
        /// <summary>
        /// Get the Hex Value out of char.
        /// </summary>
        /// <param name="hex">
        /// Char to input.
        /// </param>
        /// <returns>
        /// An int.
        /// </returns>
        internal static int GetHexVal(this char hex)
        {
            var val = (int)hex;
            return val - (val < 58 ? 48 : 55);
        }
    }
}
