using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using RestaurantBilling.Core;
using CoreLib.Repositories;
using CoreLib.Interfaces;
using MvvmCross.Plugins.Messenger;
using CoreLib.Message;
using CoreLib.Models;
using MvvmCross.Localization;

namespace CoreLib.ViewModels
{
    public class AllBillsViewModel : BaseViewModel
    {
        IDialogService _dialogService = null;
        public AllBillsViewModel(IMvxMessenger messenger, IDialogService dialogService) : base(messenger)
        {
            _dialogService = dialogService;
            InitializeMessenger();
        }
        public List<Bill> AllBills { get; set; }

        public string ShowAlertMessage { get; set; }

        public ICommand NavBack
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }

    

        public ICommand BillClickedCommand
        {
            get
            {
                return new MvxCommand<Bill>((bill) => {
                    // _dialogService.ShowAlertAsync(string.Format("List is clicked having details Bill Name: {0}, AmountPaid : {1}", bill.CustomerEmail, bill.AmountPaid), "_____Item____", "Got it!");
                    _dialogService.ShowAlertAsync(
                        string.Format(TextSource.GetText("InformationReceivedMessage"), bill.CustomerEmail, bill.AmountPaid), 
                        TextSource.GetText("InformationReceivedHeader"), 
                        TextSource.GetText("InformationReceivedButtonText"));
                });
            }
        }

        // This is another place that could be improved.
        // We are using the async capabilities built in to SQLite-Net,
        // but in this example, we simply wait for the thread to complete.
        public void Init()
        {
            Task<List<Bill>> result = Mvx.Resolve<Repository>().GetAllBills();
            result.Wait();
            AllBills = result.Result;
            //_dialogService.ShowAlertAsync("List is loaded", "_____Good News____", "Got it!");
        }
        private void InitializeMessenger()
        {
            //reload all bills with new currency
            Messenger.Subscribe<MyAlertMessage>((e) => { Mvx.Trace("Message received! {0}", e.MyMessage); ShowAlertMessage = e.MyMessage; });
        }
    }
}
