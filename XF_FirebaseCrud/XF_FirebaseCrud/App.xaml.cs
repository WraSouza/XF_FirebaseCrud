using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF_FirebaseCrud
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new XF_FirebaseCrud.Views.LoginView());
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
