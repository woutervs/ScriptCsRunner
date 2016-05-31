using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ScriptCSRunner.Annotations;

namespace ScriptCSRunner
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Script> Scripts { get; private set; } = new ObservableCollection<Script>();
        private string _textBoxOutput;
        public string TextBoxOutput
        {
            get { return _textBoxOutput; }
            set
            {
                if (_textBoxOutput == value) return;
                _textBoxOutput = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
