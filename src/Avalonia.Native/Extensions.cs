namespace Avalonia.Native
{
    internal static class Extensions
    {
        public static int32 AsComBool(this bool b) => b ? 1 : 0;
        public static bool FromComBool(this int32 b) => b != 0;
    }
}
