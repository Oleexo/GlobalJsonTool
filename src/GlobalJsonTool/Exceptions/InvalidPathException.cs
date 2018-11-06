using System;

namespace AbstractTools.Tools.GlobalJsonTool.Exceptions {
    public class InvalidPathException : Exception {
        public InvalidPathException(string path)
            : base($"The folder at path [{path}] doesn't exist") { }
    }
}