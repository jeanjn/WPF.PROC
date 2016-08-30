using MaterialDesignThemes.Wpf;
using PROC.DataAccess;
using PROC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfProc.Helpers;
using WpfProc.Views;

namespace WpfProc.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Context _context;

        private ObservableCollection<Processo> _processos;
        public ObservableCollection<Processo> Processos
        {
            get { return _processos; }
            set
            {
                if (value != _processos)
                {
                    _processos = value;
                    NotifyPropertyChanged(nameof(Processos));
                }
            }
        }

        public ICommand Adicionar => new Command<MainWindowViewModel>(a =>
        {
            DialogHost.Show(new NovoProcessoDialog(), "NovoProcesso");
        });

        public MainWindowViewModel()
        {
            _context = new Context();
            Processos = new ObservableCollection<Processo>(
                _context.Processos
                .OrderByDescending(a => a.Lembrete)
                .ThenByDescending(a => a.Data));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
