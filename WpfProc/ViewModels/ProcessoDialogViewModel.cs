using PROC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProc.ViewModels
{
    public class ProcessoDialogViewModel : INotifyPropertyChanged
    {
        public Andamento SelectedAndamento { get; set; }

        private ObservableCollection<Andamento> _andamentos;
        public ObservableCollection<Andamento> Andamentos
        {
            get { return _andamentos; }
            set
            {
                if (value != _andamentos)
                {
                    _andamentos = value;
                    NotifyPropertyChanged(nameof(_andamentos));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
