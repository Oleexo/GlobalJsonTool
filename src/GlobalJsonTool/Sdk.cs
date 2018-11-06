using System.Diagnostics;
using SemVer;

namespace AbstractTools.Tools.GlobalJsonTool {
    [DebuggerDisplay("{Version} => {Path}")]
    public class Sdk {
        public Version Version { get; set; }
        public string  Path    { get; set; }

        public bool Preview => Version.PreRelease != null;
    }
}
