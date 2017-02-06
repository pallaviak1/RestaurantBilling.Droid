using Android.App;
using CoreLib.Interfaces;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Threading.Tasks;
using System;

namespace RestaurantBilling.UI.Droid.Services
{
    public class DialogService : IDialogService
    {
        //get hold of top most activity
        protected Activity currentActivity =>
            Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        public Task ShowAlertAsync(string message, string title, string buttonText)
        {
            return Task.Run(() =>
            {
                Alert(message, title, buttonText);
            });
        }

        private void Alert(string message, string title, string buttonText)
        {
            Application.SynchronizationContext.Post(ignored =>
            {

                var builder = new AlertDialog.Builder(currentActivity);
                builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
                builder.SetTitle(title);
                builder.SetMessage(message);
                builder.SetPositiveButton(buttonText, delegate { });
                builder.Create().Show();
            }, null
            );
        }
    }
}
