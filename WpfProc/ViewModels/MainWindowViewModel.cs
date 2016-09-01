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
using System.Windows.Data;
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

        private ICollectionView _collectionView;
        public ICollectionView CollectionView
        {
            get
            {
                return _collectionView;
            }
            set
            {
                _collectionView = value;
                NotifyPropertyChanged(nameof(CollectionView));
            }
        }

        public ICommand Adicionar => new Command<MainWindowViewModel>(a =>
        {
            AdicionarProcesso();
        });

        public ICommand ItemDoubleClickCommand => new Command<MainWindowViewModel>(a =>
        {
            AlterarProcesso((Processo)CollectionView.CurrentItem);
        });

        public ICommand Alterar => new Command<Processo>(a =>
        {
            AlterarProcesso((Processo)CollectionView.CurrentItem);
        });

        public ICommand Concluir => new Command<Processo>(a =>
        {
            ConcluirProcesso(a);
        });

        public ICommand EditarAndamentos => new Command<MainWindowViewModel>(a =>
        {
            AbrirDialogAndamentos();
        });

        public ICommand MostrarCreditos => new Command<MainWindowViewModel>(a =>
        {
            AbrirCreditosDialog();
        });

        private async void AbrirCreditosDialog()
        {
            await DialogHost.Show(new NovoProcessoDialog(), "CreditoDialog", ClosingEventHandlerProcesso);
        }

        private async void AdicionarProcesso()
        {
            await DialogHost.Show(new NovoProcessoDialog(), "NovoProcesso", ClosingEventHandlerProcesso);
        }

        private async void AlterarProcesso(Processo processo)
        {
            await DialogHost.Show(new NovoProcessoDialog(processo), "NovoProcesso", ClosingEventHandlerProcesso);
        }

        private void ConcluirProcesso(Processo processo)
        {
            processo.DtConclusao = DateTime.Now;
            _context.SaveChanges();
            GetProcessos();
        }

        private async void AbrirDialogAndamentos()
        {
            await DialogHost.Show(new AndamentoDialog(), "AndamentoDialog", ClosingEventHandlerProcesso);
        }

        private void ClosingEventHandlerProcesso(object sender, DialogClosingEventArgs eventArgs)
        {
            GetProcessos();
        }

        public MainWindowViewModel()
        {
            _context = new Context();
            GetProcessos();

        }

        private void GetProcessos()
        {
            Processos = new ObservableCollection<Processo>(
                            _context.Processos
                            .Where(a =>!a.DtConclusao.HasValue)
                            .OrderByDescending(a => a.Lembrete)
                            .ThenByDescending(a => a.Data));

            var collectionView = CollectionViewSource.GetDefaultView(Processos);
            collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Data"));
            collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Numero"));
            collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Descricao"));
            collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Andamento"));
            collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Lembrete"));
            CollectionView = collectionView;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
