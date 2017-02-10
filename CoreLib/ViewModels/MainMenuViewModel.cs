using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using MvvmCross.Plugins.Messenger;

namespace CoreLib.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel(IMvxMessenger messenger) : base(messenger)
        {
        }

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

