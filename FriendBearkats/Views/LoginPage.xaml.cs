using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FriendBearkats.ViewModels;

namespace FriendBearkats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        

        public LoginPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            var vm = new LoginPageViewModel();
            
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            InitializeComponent();


            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };

           


        }


    }
}