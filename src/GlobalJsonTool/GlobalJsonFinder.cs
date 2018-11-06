using System.IO;
using System.Linq;
using AbstractTools.Tools.GlobalJsonTool.Exceptions;
using FileNotFoundException = AbstractTools.Tools.GlobalJsonTool.Exceptions.FileNotFoundException;

namespace AbstractTools.Tools.GlobalJsonTool {
    public class GlobalJsonFinder {
        public GlobalJsonFile Find(string path) {
            string         filePath = null;
            GlobalJsonFile gjf;
            if (File.Exists(path)) {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Name != GlobalJsonFile.FileName) {
                    throw new InvalidFileException(path);
                }

                gjf = new GlobalJsonFile(path);
            }
            else {
                if (!Directory.Exists(path)) {
                    throw new InvalidPathException(path);
                }

                gjf = RecurseFind(new DirectoryInfo(path));
                if (gjf == null) {
                    throw new FileNotFoundException(path);
                }
            }

            return gjf;
        }

        private GlobalJsonFile RecurseFind(DirectoryInfo directory) {
            while (directory.Parent != null) {
                var file = directory.GetFiles()
                                    .FirstOrDefault(x => x.Name == GlobalJsonFile.FileName);
                if (file == null) {
                    directory = directory.Parent;
                    continue;
                }

                return new GlobalJsonFile(file.FullName);
            }

            return null;
        }
    }
}
