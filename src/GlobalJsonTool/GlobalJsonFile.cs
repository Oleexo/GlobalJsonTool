using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SemVer;

namespace AbstractTools.Tools.GlobalJsonTool {
    public class GlobalJsonFile {
        public static readonly string  FileName = "global.json";
        private readonly       JObject _object;

        public GlobalJsonFile(string path) {
            Path = path;
            var content = File.ReadAllText(path);
            _object = JsonConvert.DeserializeObject<JObject>(content);
        }

        public string Path { get; }

        public Version Version {
            get => new Version(_object["sdk"].Value<string>("version"));
            set => _object["sdk"]["version"] = value.ToString();
        }

        public void Save() {
            var content = _object.ToString();
            File.WriteAllText(Path, content);
        }

        public static void Create(Sdk sdk, string path) {
            var process = new Process {
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = "dotnet",
                    Arguments = $"new globaljson --sdk-version {sdk.Version}",
                    WorkingDirectory = path
                }
            };

            process.Start();
        }
    }
}
