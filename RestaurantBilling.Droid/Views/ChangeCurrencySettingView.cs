
using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace RestaurantBilling.UI.Droid.Views
{
    [Activity(Label = "Change Currency", NoHistory = true)]
    public class ChangeCurrencySettingView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.View_ChangeCurrencySetting);
        }
    }
}