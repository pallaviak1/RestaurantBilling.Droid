using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using RestaurantBilling.Core;
using CoreLib.Repositories;
using CoreLib.Interfaces;
using CoreLib.Models;
using MvvmCross.Plugins.Messenger;
using CoreLib.Message;
using System.Collections.ObjectModel;
using CoreLib.Extensions;
using MvvmCross.Plugins.WebBrowser;

namespace CoreLib.ViewModels
{
    public class ChangeCurrencySettingViewModel : BaseViewModel
    {
        IDialogService _dialogService = null;
        public ChangeCurrencySettingViewModel(IMvxMessenger messenger, IDialogService dialogService, IMvxWebBrowserTask webBrowser): base(messenger)
        {
            _dialogService = dialogService;
            _webBrowser = webBrowser;
        }      

        public ICommand NavBack
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }

        public void Init()
       {
            var service = Mvx.Resolve<Repository>();
            Task<List<Currency>> task = service.GetAvailableCurrencies();
            task.Wait();
            Currencies = task.Result.ToObservableCollection(); 
            ActiveCurrency = Currencies[0];
        }
              
       
        private Currency _activeCurrency;

        public Currency ActiveCurrency
        {
            get { return _activeCurrency; }
            set
            {
                _activeCurrency = value;
                RaisePropertyChanged(() => ActiveCurrency);
            }
        }

        private ObservableCollection<Currency> _currencies;

        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                RaisePropertyChanged(() => Currencies);
            }
        }

        private readonly IMvxWebBrowserTask _webBrowser;
        public MvxCommand HelpCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    Messenger.Publish(
                     new MyAlertMessage(this)
                     { MyMessage = "Don't go away from app!" });

                    _webBrowser.ShowWebPage
                        ("http://www.google.com");//what an awesome site!
                });
            }
        }

        public MvxCommand SwitchCurrencyCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                  
                    Mvx.Resolve<Repository>().SetActiveCurrency(_activeCurrency);
                    Task<Currency> currencyTask = Mvx.Resolve<Repository>().GetActiveCurrency();
                    currencyTask.Wait();
                    ActiveCurrency = currencyTask.Result;
                    Messenger.Publish(
                      new MyAlertMessage(this)
                      { MyMessage = "Currency is changed!" });

                    _dialogService.ShowAlertAsync(string.Format("Active currency {0} {1}", ActiveCurrency.CurrencyName, ActiveCurrency.CurrencySymbol), "Active Currency", "Got it!");
                
                });
            }
        }


        //public ICommand SwitchCurrencyCommand
        //{
        //    get
        //    {
        //        return new MvxCommand(() => {
        //            Mvx.Resolve<Repository>().SetActiveCurrency(ActiveCurrency);
        //           // _dialogService.ShowAlertAsync(string.Format("Active currency {0} {1}", ActiveCurrency.CurrencyName, ActiveCurrency.CurrencySymbol), "Active Currency", "Got it!");
        //            Close(this);

        //        });
        //    }
        //}

    }
}
