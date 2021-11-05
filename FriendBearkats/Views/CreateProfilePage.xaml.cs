using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FriendBearkats.ViewModels;

namespace FriendBearkats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfilePage : ContentPage
    {

        

        public CreateProfilePage()
        {
            NavigationPage.SetHasBackButton(this, false);
            var vm = new CreatePageViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            vm.DisplayInvalidNamePrompt += () => DisplayAlert("Error", "Name not Entered, try again", "OK");
            vm.DisplayInvalidEmailPrompt += () => DisplayAlert("Error", "Email not Valid, use @shsu.edu email address", "OK");
            vm.DisplayInvalidPasswordPrompt += () => DisplayAlert("Error", "Password must be atleast 8 character, have atleast one number, one Uppercase and one lowercase", "OK");
            vm.DisplayInvalidGenderPrompt += () => DisplayAlert("Error", "Please enter gender", "OK");
            vm.DisplayInvalidAddressPrompt += () => DisplayAlert("Error", "Please enter Address", "OK");
            vm.DisplayInvalidNumberPrompt += () => DisplayAlert("Error", "Number should be 10 strings long", "OK");
            vm.DisplayInvalidConfirmPrompt += () => DisplayAlert("Error", "Password not matched", "OK");
            InitializeComponent();
            
            /*
            EmailEntry.Completed += (object sender, EventArgs e) =>
            {

                PasswordEntry.Focus();
            };

            PasswordEntry.Completed += (object sender, EventArgs e) =>
            {
                NumberEntry.Focus();
                //vm.SubmitCommand.Execute(null);
            };

            NumberEntry.Completed += (object sender, EventArgs e) =>
            {
                NameEntry.Focus();
            };

            NameEntry.Completed += (object sender, EventArgs e) =>
            {
                AddressEntry.Focus();
            };

            AddressEntry.Completed += (object sender, EventArgs e) =>
            {
                //vm.SubmitCommand.Execute(null);
                vm.CreateCommand.Execute(null);
            };
            */
        }
    }
}