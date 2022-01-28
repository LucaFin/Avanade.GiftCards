using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Avanade.GiftCards.Core.BusinessLayer;
using Avanade.GiftCards.Core.Entities;
using Avanade.GiftCards.Core.Repositories;
using Avanade.GiftCards.Mock.Repository;
using Avanade.GiftCards.WPF.Messaging.GiftCardMsg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Avanade.GiftCards.WPF.ViewModels
{
    public class GiftCardEditorViewModel : ViewModelBase
    {
        public ICommand CreateGiftCard { get; set; }
        public ObservableCollection<GiftCardRowViewModel> _GiftCardSource;
        private ICollectionView _GiftCards;
        public ICollectionView GiftCards
        {
            get { return _GiftCards; }
            set { _GiftCards = value; RaisePropertyChanged(); }
        }
        public ICommand LoadGiftCardsCommand { get; set; }
        public GiftCardEditorViewModel()
        {
            CreateGiftCard = new RelayCommand(() => ExecuteShowCreateGiftCard());
            LoadGiftCardsCommand = new RelayCommand(() => ExecuteLoadGiftCards());
            _GiftCardSource = new ObservableCollection<GiftCardRowViewModel>();
            _GiftCards = new CollectionView(_GiftCardSource);
            LoadGiftCardsCommand.Execute(null);
        }

        private void ExecuteLoadGiftCards()
        {
            IGiftCardRepository repository = new GiftCardRepositoryMock();
            MainBusinessLayer layer = new MainBusinessLayer(repository);
            var giftCards = layer.FetchAllGiftCards();
            _GiftCardSource.Clear();
            foreach (GiftCard giftCard in giftCards)
            {
                var vmGiftCardRow = new GiftCardRowViewModel(giftCard);
                _GiftCardSource.Add(vmGiftCardRow);
            }
        }

        private void ExecuteShowCreateGiftCard()
        {
            Messenger.Default.Send(new ShowCreateGiftCardMessage());
        }
    }
}
