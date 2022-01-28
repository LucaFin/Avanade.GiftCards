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
    public class UpdateGiftCardViewModel:ViewModelBase
    {
        private GiftCard _Entity;
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; RaisePropertyChanged();}
        }
        private string _Mittente;
        public string Mittente
        {
            get { return _Mittente; }
            set { _Mittente = value; RaisePropertyChanged();}
        }
        private string _Destinatario;
        public string Destinatario
        {
            get { return _Destinatario; }
            set { _Destinatario = value; RaisePropertyChanged();}
        }

        private string _Messaggio;
        public string Messaggio
        {
            get { return _Messaggio; }
            set { _Messaggio = value; RaisePropertyChanged();}
        }
        private double _Importo;
        public double Importo
        {
            get { return _Importo; }
            set { _Importo = value; RaisePropertyChanged();}
        }

        private DateTime _DataDiScadenza;
        public DateTime DataDiScadenza
        {
            get { return _DataDiScadenza; }
            set { _DataDiScadenza = value; RaisePropertyChanged();}
        }
        public ICommand UpdateCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public UpdateGiftCardViewModel()
        {
            //() => ExecuteUpdate(), 
            UpdateCommand = new RelayCommand(() => ExecuteUpdate());
            CancelCommand = new RelayCommand(() => ExecuteCancel());
            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (UpdateCommand as RelayCommand).RaiseCanExecuteChanged();
                };
            }
        }

        public UpdateGiftCardViewModel(GiftCard entity):this()
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _Entity = entity;
            Id = entity.Id;
            Mittente = entity.Mittente;
            Destinatario = entity.Destinatario;
            Messaggio = entity.Messaggio;
            Importo = entity.Importo;
            DataDiScadenza = entity.DataDiScadenza;
        }

        private void ExecuteCancel()
        {
            Messenger.Default.Send(new CloseUpdateEmployeeMessage());
        }

        private void ExecuteUpdate()
        {
            var entity = new GiftCard
            {
                Id = Id,
                Mittente = Mittente,
                Destinatario = Destinatario,
                Messaggio = Messaggio,
                Importo = Importo,
                DataDiScadenza = DataDiScadenza
            };
            var layer = new MainBusinessLayer(new GiftCardRepositoryMock());
            var result = layer.UpdateGiftCard(entity);
            if (!result.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Attenzione! Alcuni dati non sono validi!",
                    Content = result.Message,
                    Icon = MessageBoxImage.Warning
                });
                return;
            }
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Conferma",
                Content = $"La GiftCard da {Mittente} a {Destinatario} è stata aggiornata!",
                Icon = MessageBoxImage.Information
            });
            CancelCommand.Execute(null);
        }
    }
}
