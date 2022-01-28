using Avanade.GiftCards.Core.BusinessLayer;
using Avanade.GiftCards.Core.Entities;
using Avanade.GiftCards.Mock.Repository;
using Avanade.GiftCards.WPF.Messaging.GiftCardMsg;
using Avanade.GiftCards.WPF.Messaging.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avanade.GiftCards.WPF.ViewModels
{
    public class GiftCardCreateViewModel : ViewModelBase
    {
        public ICommand CreateCommand { get; set; }
        private string _Mittente;
        public string Mittente
        {
            get { return _Mittente; }
            set { _Mittente = value; RaisePropertyChanged(); }
        }
        private string _Destinatario;
        public string Destinatario
        {
            get { return _Destinatario; }
            set { _Destinatario = value; RaisePropertyChanged(); }
        }
        private string _Messaggio;
        public string Messaggio
        {
            get { return _Messaggio; }
            set { _Messaggio = value; RaisePropertyChanged(); }
        }
        private int _Importo;
        public int Importo
        {
            get { return _Importo; }
            set { _Importo = value; RaisePropertyChanged(); }
        }
        private DateTime _DataDiScadenza;
        public DateTime DataDiScadenza
        {
            get { return _DataDiScadenza; }
            set { _DataDiScadenza = value; RaisePropertyChanged(); }
        }
        public GiftCardCreateViewModel()
        {
            CreateCommand = new RelayCommand(()=>ExecuteCreate());
            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (CreateCommand as RelayCommand).RaiseCanExecuteChanged();
                };
            }
        }

        private void ExecuteCreate()
        {
            var entity = new GiftCard
            {
                Mittente = Mittente,
                Destinatario = Destinatario,
                Messaggio = Messaggio,
                Importo = Importo,
                DataDiScadenza = DataDiScadenza
            };
            var layer = new MainBusinessLayer(new GiftCardRepositoryMock());
            var response = layer.CreateGiftCard(entity);

            if (!response.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Something wrong",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Warning
                });
                return;
            }
            else
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Creazione completata",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Information
                });
            }
            Messenger.Default.Send(new CloseCreateGiftCardMessage());
        }

        private bool CanExecuteCreate()
        {
            return !string.IsNullOrEmpty(Mittente) &&
               !string.IsNullOrEmpty(Destinatario) &&
               !string.IsNullOrEmpty(Messaggio) &&
               !string.IsNullOrEmpty(Importo.ToString()) &&
               !string.IsNullOrEmpty(DataDiScadenza.ToString());
        }
    }
}
