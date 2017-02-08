using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using RestaurantBilling.Core;
using CoreLib.Repositories;
using CoreLib.Interfaces;
using CoreLib.Models;

namespace CoreLib.ViewModels
{
    public class ChangeCurrencySettingViewModel : MvxViewModel
    {
        IDialogService _dialogService = null;
        public ChangeCurrencySettingViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public ChangeCurrencySettingViewModel()
        {

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
            Currencies = Mvx.Resolve<Repository>().GetAvailableCurrencies();
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

        private List<Currency> _currencies;

        public List<Currency> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                RaisePropertyChanged(() => Currencies);
            }
        }
        public MvxCommand SwitchCurrencyCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    //Messenger.Publish(
                    //    new CurrencyChangedMessage(this)
                    //    { NewCurrency = ActiveCurrency });
                    Mvx.Resolve<Repository>().SetActiveCurrency(ActiveCurrency);
                    ActiveCurrency = Mvx.Resolve<Repository>().GetActiveCurrency();
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
