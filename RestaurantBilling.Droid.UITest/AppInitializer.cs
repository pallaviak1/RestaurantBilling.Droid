using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RestaurantBilling.Droid.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .EnableLocalScreenshots()
                    .PreferIdeSettings()
                    .ApkFile(@"D:\Self Learning\Xamarin my samples\mvvmCross\RestaurantBilling.Droid\RestaurantBilling.Droid\bin\Release\RestaurantBilling.Droid.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

