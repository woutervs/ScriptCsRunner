using System.IO;
using System.Text;

namespace ScriptCSRunner
{
    public class ControlWriter : TextWriter
    {
        private readonly MainWindowViewModel _model;
        public ControlWriter(MainWindowViewModel model)
        {
            _model = model;
        }
        public override Encoding Encoding => Encoding.ASCII;
        public override void Write(char value)
        {
            _model.TextBoxOutput += value;
        }
    }
}
