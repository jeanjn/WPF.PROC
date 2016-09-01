using MaterialDesignThemes.Wpf;
using WpfProc.DataAccess;
using WpfProc.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfProc.Helpers;

namespace WpfProc.ViewModels
{
    public class AndamentoDialogViewModel : INotifyPropertyChanged
    {
        private Context _context;
        private string _descricao;
        public string Descricao
        {
            get
            {
                return _descricao;
            }
            set
            {
                _descricao = value;
                NotifyPropertyChanged(nameof(Descricao));
            }
        }
        private Andamento _andamento;
        public Andamento Andamento
        {
            get
            {
                return _andamento;
            }
            set
            {
                if(value.Id == -1)
                {
                    Descricao = "";
                }
                else
                {
                    Descricao = value.Descricao;
                }
                _andamento = value;
                NotifyPropertyChanged(nameof(Andamento));
            }
        }
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

        public ICommand Salvar => new Command<AndamentoDialogViewModel>(a =>
        {
            SalvarAndamento();
        });

        public AndamentoDialogViewModel()
        {
            _context = new Context();
            Andamentos = new ObservableCollection<Andamento>(_context.Andamentos.ToList());

            Andamento = new Andamento
            {
                Id = -1,
                Descricao = "-- NOVO --"
            };

            Andamentos.Insert(0, Andamento);
        }

        private void SalvarAndamento()
        {
            if(string.IsNullOrWhiteSpace(Descricao))
            {
                return;
            }


            if(Andamento.Id == -1)
            {
                _context.Andamentos.Add(new Andamento { Descricao = Descricao });
            }
            else
            {
                var andamento = _context.Andamentos.Single(a => a.Id == Andamento.Id);
                andamento.Descricao = Descricao;
            }

            _context.SaveChanges();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
