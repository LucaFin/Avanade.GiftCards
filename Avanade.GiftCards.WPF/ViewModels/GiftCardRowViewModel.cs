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
using System.Windows;
using System.Windows.Input;

namespace Avanade.GiftCards.WPF.ViewModels
{
    public class GiftCardRowViewModel : ViewModelBase
    {
        private GiftCard giftCard;

        private string _Mittente;
        public string Mittente
        {
            get { return _Mittente; }
            set { _Mittente = value;RaisePropertyChanged(); }
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

        private double _Importo;
        public double Importo
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

        private bool _Dettagli;
        public bool Dettagli
        {
            get { return _Dettagli; }
            set { _Dettagli = value; RaisePropertyChanged(); }
        }

        private bool _Attivo;
        public bool Attivo
        {
            get { return _Attivo; }
            set { _Attivo = value;RaisePropertyChanged(); }
        }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public GiftCardRowViewModel()
        {
            //Inizializzazione dei comandi
            UpdateCommand = new RelayCommand(() => ExecuteUpdate());
            DeleteCommand = new RelayCommand(() => ExecuteDelete());

        }

        public GiftCardRowViewModel(GiftCard giftCard):this()
        {
            this.giftCard = giftCard;
            Mittente = giftCard.Mittente;
            Destinatario = giftCard.Destinatario;
            Messaggio=giftCard.Messaggio;
            Importo = giftCard.Importo;
            DataDiScadenza = giftCard.DataDiScadenza;
            Attivo = giftCard.DataDiScadenza > DateTime.Today;
        }

        private void ExecuteDelete()
        {
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Confirm delete",
                Content = "Are you sure?",
                Icon = MessageBoxImage.Question,
                Buttons = MessageBoxButton.YesNo,
                Callback = OnMessageBoxResultReceived
            });
        }


        private void OnMessageBoxResultReceived(MessageBoxResult result)
        {
            if (result == MessageBoxResult.Yes)
            {
                var layer = new MainBusinessLayer(new GiftCardRepositoryMock());

                var response = layer.DeleteGiftCard(giftCard);

                if (!response.Success)
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Errore",
                        Content = response.Message,
                        Icon = MessageBoxImage.Error,
                        Buttons = MessageBoxButton.OK
                    });
                    return;
                }
                else
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Deletion Confirmed",
                        Content = response.Message,
                        Icon = MessageBoxImage.Information
                    });
                }
            }
        }


        private void ExecuteUpdate()
        {
            Messenger.Default.Send(new ShowUpdateGiftCardMessage { Entity = giftCard });
        }
    }
}
