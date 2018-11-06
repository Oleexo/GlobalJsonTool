using System;

namespace AbstractTools.Tools.GlobalJsonTool.Exceptions {
    public class InvalidFileException : Exception {
        public InvalidFileException(string filePath)
            : base($"The file [{filePath}] is invalid global.json") { }
    }
}