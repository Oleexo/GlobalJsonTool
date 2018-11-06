using System;

namespace AbstractTools.Tools.GlobalJsonTool.Exceptions {
    public class FileNotFoundException : Exception {
        public FileNotFoundException(string path)
            : base($"global.json file not found from directory [{path}] to hard drive root") { }
    }
}