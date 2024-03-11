using System.ComponentModel;

namespace CererePermisiuneAccesMAP.Models
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string InitialName { get; set; } = "Hello world;";

        public bool Loading { get; set; } = false;
    }
}
