using System;
using System.IO;
using System.IO.Abstractions;

namespace FSysToolsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void Run()
        {
            const string directory = @"D:\_G_Download\Books (gkibria.mobin)\CSE (SW Eng)\SW Eng\2020";
            const string suffix = " (2020)";

            var fileExtensions = new string[] { ".pdf", ".epub", ".zip" };

            var transformer = FileNameTransformations.Start()
                                                     .RemoveSuffix(suffix.Trim())
                                                     .TrimFileName()
                                                     .AddSuffix(suffix)
                                                     .GetTranformer();

            FileSearchOption searchOption = new(fileExtensions, SearchOption.AllDirectories);

            //new EbookOrganizerV1(new FileSystem())
            //    //.RemoveSuffixFromName("(2019)", directory, searchOption)
            //    //.TrimFileName(directory, searchOption)
            //    .AddSuffixWithName(suffix, directory, searchOption)
            //    .MoveIntoParentDirectory(directory, searchOption);

            new EbookOrganizer(new FileSystem()).RenameFiles(transformer, directory, searchOption)
                                                .MoveIntoParentDirectory(directory, searchOption);
        }
    }
}
