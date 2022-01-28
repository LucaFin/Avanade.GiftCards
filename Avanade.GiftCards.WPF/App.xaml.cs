using Avanade.GiftCards.Mock.Storage;
using Avanade.GiftCards.WPF.Messaging.Misc;
using Avanade.GiftCards.WPF.ViewModels;
using Avanade.GiftCards.WPF.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Avanade.GiftCards.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Messenger.Default.Register<DialogMessage>(this, OnDialogMessageReceived);
            AllocationMockStorage.Initialize();
            GiftCardEditorView view = new GiftCardEditorView();
            GiftCardEditorViewModel vm = new GiftCardEditorViewModel();
            view.DataContext = vm;
            view.Show();
            base.OnStartup(e);
        }

        private void OnDialogMessageReceived(DialogMessage message)
        {
            MessageBoxResult result = MessageBox.Show(
                message.Content,
                message.Title,
                message.Buttons, message.Icon);

            if (message.Callback != null)
                message.Callback(result);
        }
    }
}
