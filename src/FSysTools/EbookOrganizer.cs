using System;
using System.IO.Abstractions;

namespace FSysToolsConsole
{
    public class EbookOrganizer
    {
        private readonly IFileSystem _fs;

        public EbookOrganizer(IFileSystem fileSystem) => this._fs = fileSystem;

        public EbookOrganizer RenameFiles(Func<FileName, FileName> fileNameTransformer,
                                          string directory,
                                          FileSearchOption searchOption)
        {
            foreach (var fileInfo in this._fs.GetFilesByExtension(directory, searchOption))
            {
                var newFileName = fileNameTransformer(new(this._fs.Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.Extension));

                this._fs.File.Move(fileInfo.FullName, this._fs.Path.Combine(fileInfo.Directory.FullName, newFileName.ToString()));
            }

            return this;
        }

        public EbookOrganizer MoveIntoParentDirectory(string directory, FileSearchOption searchOption)
        {
            foreach (var fileInfo in this._fs.GetFilesByExtension(directory, searchOption))
            {
                this._fs.File.Move(fileInfo.FullName, this._fs.Path.Combine(fileInfo.Directory.Parent.FullName, fileInfo.Name));
            }

            return this;
        }
    }
}
