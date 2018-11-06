using System;
using System.IO;
using System.Linq;
using AbstractTools.Tools.GlobalJsonTool.Exceptions;

namespace AbstractTools.Tools.GlobalJsonTool {
    public class GlobalJsonTool {
        private readonly bool    _interactive;
        private readonly string  _path;
        private readonly SdkList _sdks;

        public GlobalJsonTool(bool   preview     = false,
                              bool   interactive = false,
                              string path        = "./") {
            _interactive = interactive;
            _path = path;
            _sdks = new SdkList(preview);
        }

        public void Init() {
            var directory = new DirectoryInfo(_path);
            var files     = directory.GetFiles();
            if (files.Any(f => f.Name == "global.json")) {
                throw new FileAlreadyExistException(_path);
            }

            var sdkSelector = new SdkSelector(_sdks);
            var sdk = _interactive
                ? sdkSelector.ManualSelect()
                : sdkSelector.AutoSelect();

            GlobalJsonFile.Create(sdk, _path);
            Console.WriteLine($"global.json create with SDK version {sdk.Version}");
        }

        public void Update() {
            var gjf        = new GlobalJsonFinder();
            var globalJson = gjf.Find(_path);
            Console.WriteLine($"File: {globalJson.Path}");
            Console.WriteLine($"Current version: {globalJson.Version}");

            var sdkSelector = new SdkSelector(_sdks);

            var sdk = _interactive
                ? sdkSelector.ManualSelect()
                : sdkSelector.AutoSelect();

            if (globalJson.Version == sdk.Version) {
                Console.WriteLine("The SDK is already up to date");
                return;
            }

            globalJson.Version = sdk.Version;
            globalJson.Save();
            Console.WriteLine($"Update SDK version to {globalJson.Version}");
        }
    }
}
