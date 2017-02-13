using Android.Content;
using CoreLib.Interfaces;
using CoreLib.Repositories;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using RestaurantBilling.Core;
using RestaurantBilling.UI.Droid.Services;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Linq;

namespace RestaurantBilling.UI.Droid
{
    /// <summary>
    /// Every MvvmCross UI project needs a setup class.
    /// For Android, inherit from MvxAndroidSetup
    /// 
    /// Initializes:
    /// - IoC system
    /// - MvvmCross data binding
    /// - App class and collection of ViewModels
    /// - UI project and collection of Views
    /// </summary>
    public class Setup : MvxAndroidSetup
    {

        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            var dbConn = FileAccessHelper.GetLocalFilePath("restaurant.db3");
            Mvx.RegisterSingleton(new Repository(dbConn));
            return new App(Thread.CurrentThread.CurrentUICulture);
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            Mvx.RegisterSingleton<IDialogService>(() => new DialogService());
        }
        //protected virtual IEnumerable<Assembly> ValueConverterAssemblies { get; }

        protected override IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                return base.ValueConverterAssemblies.Concat(new[] { typeof(MvxLanguageConverter).Assembly });
            }
        }

        //protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        //{
        //    base.FillValueConverters(registry);
        //    registry.AddOrOverwrite("Language", new MvxLanguageConverter() );
        //}
    }
}