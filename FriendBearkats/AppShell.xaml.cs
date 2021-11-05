using FriendBearkats.Views;

using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;


namespace FriendBearkats
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
           
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(CreateProfilePage), typeof(CreateProfilePage));
            Routing.RegisterRoute(nameof(FindPage), typeof(FindPage));
            //Routing.RegisterRoute("create", typeof(CreateProfilePage));
            //Routing.RegisterRoute("main/login", typeof(LoginPage));
            //Routing.RegisterRoute("main/profile/find", typeof(FindPage));
            BindingContext = this;

        }

        public ICommand ExecuteLogout => new Command(async () => await GoToAsync("main/login"));
    }
}
