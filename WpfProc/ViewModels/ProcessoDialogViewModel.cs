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
using System.Windows.Input;
using WpfProc.Helpers;

namespace WpfProc.ViewModels
{
    public class ProcessoDialogViewModel : INotifyPropertyChanged
    {
        private Context _context;

        private Processo _processo;

        #region Processo
        public Processo Processo { get; set; }
        public DateTime Data
        {
            get
            {
                return Processo.Data;
            }
            set
            {
                Processo.Data = value;
                NotifyPropertyChanged(nameof(Data));
            }
        }
        public string Numero
        {
            get
            {
                return Processo.Numero;
            }
            set
            {
                Processo.Numero = value;
                NotifyPropertyChanged(nameof(Data));
            }
        }
        public string Descricao
        {
            get
            {
                return Processo.Descricao;
            }
            set
            {
                Processo.Descricao = value;
                NotifyPropertyChanged(nameof(Descricao));
            }
        }
        public string Observacao
        {
            get
            {
                return Processo.Observacao;
            }
            set
            {
                Processo.Observacao = value;
                NotifyPropertyChanged(nameof(Observacao));
            }
        }
        public DateTime? Lembrete
        {
            get
            {
                return Processo.Lembrete ?? DateTime.Now;
            }
            set
            {
                Processo.Lembrete = value;
                SetLembrete();
                NotifyPropertyChanged(nameof(Lembrete));
            }
        }
        public string _tempoLembrete { get; set; }
        public string TempoLembrete
        {
            get
            {
                return _tempoLembrete;
            }
            set
            {
                _tempoLembrete = value;
                SetLembrete();
                NotifyPropertyChanged(nameof(TempoLembrete));
            }
        }
        public Andamento Andamento
        {
            get
            {
                return Processo.Andamento;
            }
            set
            {
                Processo.Andamento = value;
                NotifyPropertyChanged(nameof(Andamento));
            }
        }

        private void SetLembrete()
        {
            if (Lembrete == null)
            {
                return;
            }

            int hora = 0, minuto = 0;
            try
            {
                

                var result = string.IsNullOrWhiteSpace(TempoLembrete) ? new string[] { } : TempoLembrete.Split(':');
                if (result.Length > 1)
                {
                    hora = Convert.ToInt32(result[0]);
                    minuto = Convert.ToInt32(result[1]);
                }

                var lembrete = Lembrete.Value;
                Processo.Lembrete = new DateTime(lembrete.Year, lembrete.Month, lembrete.Day, hora, minuto, 0, 0);
            }
            catch (Exception)
            {
                Lembrete = null;
                TempoLembrete = null;
            }

        }
        #endregion



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

        public ICommand Salvar => new Command<ProcessoDialogViewModel>(a =>
        {
            SalvarProcesso();
        });


        public ProcessoDialogViewModel()
        {
            _context = new Context();

            Andamentos = new ObservableCollection<Andamento>(_context.Andamentos);

            Processo = new Processo();
        }

        private void SalvarProcesso()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
