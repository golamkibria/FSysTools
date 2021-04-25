using System.IO;

namespace FSysToolsConsole
{
    public record FileSearchOption(string[] FileExtensions, SearchOption SearchOption = SearchOption.TopDirectoryOnly);
}
