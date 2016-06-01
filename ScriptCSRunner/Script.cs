using System;
using System.IO;
using System.Linq;
using ScriptCs.Contracts;
using ScriptCs.Hosting;
using ScriptCs.Engine.Roslyn;

namespace ScriptCSRunner
{
    public class Script : IEquatable<Script>
    {
        public Action ActionBeforeClick { get; set; }

        public string Name { get; }
        public FileInfo File { get; set; }

        public RelayCommand Action { get; private set; }

        public Script(string name, FileInfo file, Action actionBeforeClick)
        {
            Action = new RelayCommand(() =>
            {
                ActionBeforeClick.Invoke();
                var console = new ScriptConsole();
                var logProvider = new ColoredConsoleLogProvider(LogLevel.Info, console);
                var builder = new ScriptServicesBuilder(console, logProvider);
                builder.ScriptEngine<CSharpScriptInMemoryEngine>();
                var services = builder.Build();
                var executor = services.Executor;
                executor.Initialize(Enumerable.Empty<string>(), Enumerable.Empty<IScriptPack>());
                executor.Execute(file.FullName, Arguments);

            });
            ActionBeforeClick = actionBeforeClick;
            Name = name;
            File = file;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool Equals(Script other)
        {
            return other.File.FullName.Equals(File.FullName);
        }

        public override bool Equals(object obj)
        {
            var script = obj as Script;
            return script != null && Equals(script);
        }

        public string[] Arguments { get; set; } = {};
    }
}
