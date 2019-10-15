using Golden.Fish.Core;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Golden.Fish.AndroidApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            BaseBluetoothServer bluetoothServer = new BaseBluetoothServer();
            bluetoothServer.Initialize();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
