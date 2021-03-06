using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Text;

using System.Linq;
using Xamarin.Forms;
using SQLite;
using FriendBearkats.Views;
using FriendBearkats.Services;

using FriendBearkats;
using Splat;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace FriendBearkats.ViewModels
{
    class LoginPageViewModel: INotifyPropertyChanged
    {
        private IRoutingService _navigationService;
        public Action DisplayInvalidLoginPrompt;
        public string WebAPIkey = "AIzaSyAVdr8V7916YDbQimGv3rxWLaLpnG6TpMs";
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        public string getEmail()
        {
            return email;
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public ICommand SignupCommand { protected set; get; }
        public LoginPageViewModel(IRoutingService navigationService = null)
        {
            _navigationService = navigationService ?? Locator.Current.GetService<IRoutingService>();
            SubmitCommand = new Command(OnSubmit);
            SignupCommand = new Command(OnSignup);
        }

        public async void OnSignup()
        {
            await Shell.Current.GoToAsync(nameof(CreateProfilePage));
            //await Shell.Current.GoToAsync("//login/create");
        }
        public async  void OnSubmit()
        {

            var authProvider = new FirebaseAuthprovider(new Firebase.Auth.FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                await Shell.Current.GoToAsync(nameof(ProfilePage));
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }
            /* var details = await App.Database.GetPeopleAsync();

             var a1 = from x in details where x.Email == email && x.Password == password select x;
             var n = a1.ToArray();

             //if (email != "rex@shsu.edu" || password != "secret")
             if(n.Length == 0)
             {
                 DisplayInvalidLoginPrompt();
             }
             else
             {
                 //new AppShell();
                 //await Shell.Current.GoToAsync(nameof(ProfilePage));
                 await Shell.Current.GoToAsync(nameof(ProfilePage));
                 //await _navigationService.NavigateTo("//main/profile");
                 //new AppShell();
             }*/
        }
    }
}
