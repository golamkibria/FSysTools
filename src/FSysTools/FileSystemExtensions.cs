using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace FSysToolsConsole
{
    public static class FileSystemExtensions
    {
        public static string RenameToFilePath(this IFileSystem fs, IFileInfo fileInfo, string newFileName)
             => fs.Path.Combine(fileInfo.DirectoryName, newFileName);

        public static bool FileNameDoesNotEndsWith(this IFileSystem fs, string fileName, string suffix)
            => !fs.FileNameEndsWith(fileName, suffix);

        public static bool FileNameEndsWith(this IFileSystem fs, string fileName, string suffix)
            => fs.Path.GetFileNameWithoutExtension(fileName)
                             .EndsWith(suffix, StringComparison.OrdinalIgnoreCase);

        public static IEnumerable<IFileInfo> GetFilesByExtension(this IFileSystem fs, string parentDirectory, FileSearchOption searchOption)
            => searchOption.FileExtensions.SelectMany(extension => fs.Directory.EnumerateFiles(parentDirectory, $"*{extension}", searchOption.SearchOption))
                                          .Select(file => fs.FileInfo.FromFileName(file));
    }
}
