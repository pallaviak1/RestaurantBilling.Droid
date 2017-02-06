using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using RestaurantBilling.Core;
using CoreLib.Repositories;
using CoreLib.Interfaces;

namespace CoreLib.ViewModels
{
    public class AllBillsViewModel : MvxViewModel
    {
        IDialogService _dialogService = null;
        public AllBillsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        public List<Bill> AllBills { get; set; }

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
                    _dialogService.ShowAlertAsync(string.Format("List is clicked having details Bill Name: {0}, AmountPaid : {1}", bill.CustomerEmail, bill.AmountPaid), "_____Item____", "Got it!");
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
    }
}
