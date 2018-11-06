using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Version = SemVer.Version;

namespace AbstractTools.Tools.GlobalJsonTool {
    public class SdkList : IEnumerable<Sdk> {
        private Sdk[] _sdks;

        public SdkList(bool includePreview = false) {
            GetSdks(includePreview);
        }

        public Sdk this[int index] => _sdks[index];

        public IEnumerator<Sdk> GetEnumerator() {
            return _sdks.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private void GetSdks(bool includePreview) {
            var process = new Process();
            // Redirect the output stream of the child process.
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = "dotnet";
            process.StartInfo.Arguments = "--list-sdks";
            process.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            _sdks = output.Split(
                              new[] {Environment.NewLine},
                              StringSplitOptions.None
                          ).Where(s => !string.IsNullOrWhiteSpace(s))
                          .Select(s => {
                              return new Sdk {
                                  Version = new Version(s.Substring(0, s.IndexOf(' '))),
                                  Path = s.Substring(s.IndexOf(' ') + 1)
                                          .Replace("[", string.Empty)
                                          .Replace("]", string.Empty)
                              };
                          })
                          .Where(x => x.Preview == includePreview)
                          .OrderByDescending(s => s.Version)
                          .ToArray();
        }
    }
}
