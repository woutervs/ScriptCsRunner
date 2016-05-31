using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NuGet;

namespace ScriptCSRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _model = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();

            var di = new DirectoryInfo("Scripts");
            if (!di.Exists) di.Create();
            else _model.Scripts.AddRange(di.EnumerateFiles("*.csx").Select(CreateScript));

            var watcher = new FileSystemWatcher
            {
                Path = di.FullName,
                Filter = "*.csx",
                EnableRaisingEvents = true
            };

            watcher.Created += (sender, args) => HandleFile(null, args.FullPath);
            watcher.Deleted += (sender, args) => HandleFile(args.FullPath, null);
            watcher.Renamed += (sender, args) => HandleFile(args.OldFullPath, args.FullPath);

            DataContext = _model;
            Console.SetOut(new ControlWriter(_model));
        }

        private void HandleFile(string oldFile, string newFile)
        {
            Dispatcher.Invoke(() =>
            {
                if (!string.IsNullOrEmpty(oldFile))
                {
                    _model.Scripts.RemoveAll(x => x.File.FullName.Equals(oldFile));
                }
                if (!string.IsNullOrEmpty(newFile))
                {
                    _model.Scripts.Add(CreateScript(newFile));
                }
            });
        }

        private Script CreateScript(string fileName)
        {
            var fi = new FileInfo(fileName);
            return CreateScript(fi);
        }

        private Script CreateScript(FileInfo fi)
        {
            if (fi.Exists) return new Script(System.IO.Path.GetFileNameWithoutExtension(fi.Name), fi, ()=>_model.TextBoxOutput = "");
            return null;
        }
    }
}
