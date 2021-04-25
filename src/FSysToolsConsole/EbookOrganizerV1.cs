using System.IO.Abstractions;
using System.Linq;

namespace FSysToolsConsole
{
    public class EbookOrganizerV1
    {
        private readonly IFileSystem _fs;

        public EbookOrganizerV1(IFileSystem fileSystem)
        {
            this._fs = fileSystem;
        }

        public EbookOrganizerV1 AddSuffixWithName(string suffix, string directory, FileSearchOption searchOption)
        {
            foreach (var (fileInfo, newFileName) in
                from fileInfo in this._fs.GetFilesByExtension(directory, searchOption).Where(fi => this._fs.FileNameDoesNotEndsWith(fi.Name, suffix))
                let newFileName = $"{this._fs.Path.GetFileNameWithoutExtension(fileInfo.Name)}{suffix}{fileInfo.Extension}"
                select (fileInfo, newFileName))
            {
                this._fs.File.Move(fileInfo.FullName, this._fs.RenameToFilePath(fileInfo, newFileName));
            }

            return this;
        }

        public EbookOrganizerV1 RemoveSuffixFromName(string suffix, string directory, FileSearchOption searchOption)
        {
            foreach (var fileInfo in this._fs.GetFilesByExtension(directory, searchOption)
                                             .Where(fi => this._fs.FileNameEndsWith(fi.Name, suffix)))
            {

                this._fs.File.Move(fileInfo.FullName,
                                   this._fs.RenameToFilePath(fileInfo,
                                                             newFileName: $"{this._fs.Path.GetFileNameWithoutExtension(fileInfo.Name).RemoveLast(suffix)}{fileInfo.Extension}"));
            }

            return this;
        }

        public EbookOrganizerV1 TrimFileName(string directory, FileSearchOption searchOption)
        {
            foreach (var fileInfo in this._fs.GetFilesByExtension(directory, searchOption))
            {
                this._fs.File.Move(fileInfo.FullName, this._fs.Path.Combine(fileInfo.Directory.FullName, $"{this._fs.Path.GetFileNameWithoutExtension(fileInfo.Name).Trim()}{fileInfo.Extension}"));
            }

            return this;
        }       

        public EbookOrganizerV1 MoveIntoParentDirectory(string directory, FileSearchOption searchOption)
        {
            foreach (var fileInfo in this._fs.GetFilesByExtension(directory, searchOption))
            {
                this._fs.File.Move(fileInfo.FullName, this._fs.Path.Combine(fileInfo.Directory.Parent.FullName, fileInfo.Name));
            }

            return this;
        }
    }
}
