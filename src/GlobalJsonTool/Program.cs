using System;
using McMaster.Extensions.CommandLineUtils;

namespace AbstractTools.Tools.GlobalJsonTool {
    internal class Program {
        private static int Main(string[] args) {
            var app = new CommandLineApplication();

            app.HelpOption();
            var optionInit        = app.Option("-n|--new", "Create new global.json", CommandOptionType.NoValue);
            var optionPreview     = app.Option("-p|--preview", "Use preview version of sdk", CommandOptionType.NoValue);
            var optionPath        = app.Option<string>("-o|--output <PATH>", "The path to the global.json file", CommandOptionType.SingleValue);
            var optionInteractive = app.Option("-i|--interactive", "Use interactive selection", CommandOptionType.NoValue);
            app.OnExecute(() => {
                try {
                    var gjt = new GlobalJsonTool(optionPreview.HasValue(), optionInteractive.HasValue(), optionPath.HasValue() ? optionPath.Value() : "./");
                    if (optionInit.HasValue())
                    {
                        gjt.Init();
                    }
                    else
                    {
                        gjt.Update();
                    }
                }
                catch (Exception e) {
                    Console.WriteLine($"[Error] {e.Message}");
                    return -1;
                }

                return 0;
            });

            return app.Execute(args);
        }
    }
}
