using FriendBearkats.Services;
using FriendBearkats.ViewModels;
using FriendBearkats.Views;
using Splat;
using System;
using System.IO;
using Xamarin.Forms;

namespace FriendBearkats
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            //MainPage = new FriendBearkats.Views.LoginPage();
            MainPage = new AppShell();
           
        }
        private void InitializeDi()
        {
            // Services
            Locator.CurrentMutable.RegisterLazySingleton<IRoutingService>(() => new ShellRoutingService());
            // ViewModels
            Locator.CurrentMutable.Register(() => new LoginPageViewModel());
            
            Locator.CurrentMutable.Register(() => new CreatePageViewModel());
            Locator.CurrentMutable.Register(() => new ProfilePage());
            Locator.CurrentMutable.Register(() => new FindPage());
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
