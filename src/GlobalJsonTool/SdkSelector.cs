using System;
using System.Linq;

namespace AbstractTools.Tools.GlobalJsonTool {
    public class SdkSelector {
        private readonly SdkList _sdks;

        public SdkSelector(SdkList sdks) {
            _sdks = sdks;
        }

        public Sdk ManualSelect() {
            for (var i = 0; i < _sdks.Count(); i++) {
                Console.WriteLine($"[{i + 1}]: {_sdks[i].Version}");
            }

            var haveValue = false;
            var value     = 0;
            while (!haveValue) {
                Console.WriteLine($"Select Sdk [1-{_sdks.Count()}]");
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) {
                    haveValue = int.TryParse(input.Trim(), out value);
                    if (haveValue == false) {
                        Console.WriteLine($"Need to enter a value between [1-{_sdks.Count()}]");
                    }
                    else {
                        if (value <= 0 || value > _sdks.Count()) {
                            haveValue = false;
                            Console.WriteLine("Invalid selection");
                        }
                    }
                }
            }

            return _sdks[value - 1];
        }

        public Sdk AutoSelect() {
            return _sdks[0];
        }
    }
}
