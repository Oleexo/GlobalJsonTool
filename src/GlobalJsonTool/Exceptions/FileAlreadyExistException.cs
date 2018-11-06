using System;

namespace AbstractTools.Tools.GlobalJsonTool.Exceptions {
    public class FileAlreadyExistException : Exception {
        public FileAlreadyExistException(string path)
            : base($"The folder [{path}] already contains a global.json file") { }
    }
}
