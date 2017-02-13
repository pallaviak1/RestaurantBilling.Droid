using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Plugins.Messenger;
using System;
using System.Threading.Tasks;


namespace CoreLib.ViewModels
{
    public class BaseViewModel : MvxViewModel, IDisposable
    {
        protected IMvxMessenger Messenger;
        public BaseViewModel(IMvxMessenger messenger)
        {
            Messenger = messenger;
        }
      
        public virtual IMvxLanguageBinder TextSource
        {
            get { return new MvxLanguageBinder("", GetType().Name); }
        }

        protected async Task ReloadDataAsync()
        {
            try
            {
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        protected virtual Task InitializeAsync()
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            Messenger = null;
        }

    }
}
