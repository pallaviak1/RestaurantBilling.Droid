using CoreLib.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace RestaurantBilling.Core.ViewModels
{
    public class MainMenuViewModel : MvxViewModel
    {
        public ICommand NavigateCreateBill
        {
            get
            {
                // Navigation Note:
                // Must add following value to Assembly.cs for any Windows projects to see the lambda.
                // [assembly: InternalsVisibleTo("Cirrious.MvvmCross")]
                return new MvxCommand(() => ShowViewModel<BillViewModel>());
            }
        }

        public ICommand NavigateAllBills
        {
            get
            {
                // Navigation Note:
                // Must add following value to Assembly.cs for any Windows projects to see the lambda.
                // [assembly: InternalsVisibleTo("Cirrious.MvvmCross")]
                return new MvxCommand(() => ShowViewModel<AllBillsViewModel>());
            }
        }

        public ICommand NavigateChangeCurrencySetting
        {
            get
            {
                // Navigation Note:
                // Must add following value to Assembly.cs for any Windows projects to see the lambda.
                // [assembly: InternalsVisibleTo("Cirrious.MvvmCross")]
                return new MvxCommand(() => ShowViewModel<ChangeCurrencySettingViewModel>());
            }
        }
    }
}

