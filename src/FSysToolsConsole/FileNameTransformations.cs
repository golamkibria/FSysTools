using System;
using System.Collections.Generic;

namespace FSysToolsConsole
{
    public class FileNameTransformations
    {
        public static FileNameTransformationBuilder Start()
            => new FileNameTransformationBuilder();
    }

    public class FileNameTransformationBuilder
    {
        private readonly List<(string Name, Func<FileName, FileName> Transformation)> _fileNameTransformations = new List<(string Name, Func<FileName, FileName> Transformation)>();

        public FileNameTransformationBuilder TrimFileName()
        {
            this._fileNameTransformations.Add((nameof(TrimFileName), fileName => new(fileName.NameWithoutExtension.Trim(), fileName.Extension)));
            return this;
        }

        public FileNameTransformationBuilder RemoveSuffix(string suffix)
        {
            this._fileNameTransformations.Add((nameof(RemoveSuffix),
                                              fileName => fileName.NameWithoutExtension.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
                                                            ? (new(fileName.NameWithoutExtension.RemoveLast(suffix), fileName.Extension))
                                                            : (new(fileName.NameWithoutExtension, fileName.Extension))));
            return this;
        }

        public FileNameTransformationBuilder AddSuffix(string suffix)
        {
            this._fileNameTransformations.Add((nameof(AddSuffix), fileName => new(fileName.NameWithoutExtension + suffix, fileName.Extension)));
            return this;
        }

        public Func<FileName, FileName> GetTranformer()
        {
            var transformations = this._fileNameTransformations.ToArray();
            this._fileNameTransformations.Clear();

            return fileName => Execute(transformations, fileName);
        }

        private static FileName Execute(IEnumerable<(string Name, Func<FileName, FileName> Transformation)> transformations, FileName fileName)
        {
            foreach (var transformation in transformations)
            {
                fileName = transformation.Transformation(fileName);
            }

            return fileName;
        }
    }
}
