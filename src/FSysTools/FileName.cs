namespace FSysToolsConsole
{
    public record FileName(string NameWithoutExtension, string Extension)
    {
        public override string ToString() => NameWithoutExtension + Extension;
    }
}
