namespace FSysToolsConsole
{
    public static class StringExtensions
    {
        public static string RemoveLast(this string srcValue, string value)
            => srcValue.Substring(0, srcValue.Length - value.Length);
    }
}
