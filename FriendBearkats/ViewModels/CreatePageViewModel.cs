using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using FriendBearkats.Views;
using FriendBearkats;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FriendBearkats.Services;
using Splat;
using Firebase.Auth;

namespace FriendBearkats.ViewModels
{
    class CreatePageViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayInvalidNamePrompt;
        public Action DisplayInvalidPasswordPrompt;
        public Action DisplayInvalidEmailPrompt;
        public Action DisplayInvalidGenderPrompt;
        public Action DisplayInvalidNumberPrompt;
        public Action DisplayInvalidAddressPrompt;
        public Action DisplayInvalidConfirmPrompt;

        
        private IRoutingService _navigationService;
        

        const string emailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@shsu.edu";
        const string passPattern = @"^(?=.*[A - Z])(?=.*\d)(?!.* (.)\1\1)[a - zA - Z0 - 9@]{6,12}$";
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public IList<string> GenderList { get; set; }
        public IList<string> SpList { get; set; }
        public string WebAPIkey = "AIzaSyAVdr8V7916YDbQimGv3rxWLaLpnG6TpMs";

        private string emailentry;
        private string passwordentry;
        private string passwordconfirm ;
        private string numberentry;
        private string nameentry;
        private string addressentry;
       


        //---------------------------------------------------------------------------
        
        private bool emailwarningvisibility = false;
        private bool passwordwarningvisibility = false;
        private bool confirmwarningvisibility = false;
        private bool numberwarningvisibility = false;
        
        private Regex emailRegex = new Regex(emailPattern, RegexOptions.IgnoreCase);
        private Regex passRegex = new Regex(passPattern);
        private Regex hasNumber = new Regex(@"[0-9]+");
        private Regex hasUpperChar = new Regex(@"[A-Z]+");
        private Regex hasLowerChar = new Regex(@"[a-z]+");
        private Regex hasMinimum8Chars = new Regex(@".{8,}");

        public string EmailEntry
        {

            get { return emailentry; }
            
            set
            {
                emailentry = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EmailEntry"));
                if (!emailRegex.IsMatch(EmailEntry))
                    EmailWarningVisibility = true;
                else
                    EmailWarningVisibility = false;

               
            }
        }
        
        public bool EmailWarningVisibility
        {
            get
            {
                return emailwarningvisibility;
            }
            set
            {
                emailwarningvisibility = !emailRegex.IsMatch(EmailEntry);
                PropertyChanged(this, new PropertyChangedEventArgs("EmailWarningVisibility"));
            }
        }

        
      
        
        //----------------------------------
        
        
        public string PasswordEntry
        {
            get { return passwordentry; }
            set
            {
                passwordentry = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PasswordEntry"));
                //if (!passRegex.IsMatch(passwordentry))
                
                if (!hasNumber.IsMatch(PasswordEntry) || !hasLowerChar.IsMatch(PasswordEntry) ||
                    !hasMinimum8Chars.IsMatch(PasswordEntry) || !hasUpperChar.IsMatch(PasswordEntry))
                   PasswordWarningVisibility = true;
                else
                    PasswordWarningVisibility = false;
            }
        }

        public bool PasswordWarningVisibility
        {
            get { return passwordwarningvisibility; }
            set
            {
                if (!hasNumber.IsMatch(PasswordEntry) || !hasLowerChar.IsMatch(PasswordEntry) ||
                   !hasMinimum8Chars.IsMatch(PasswordEntry) || !hasUpperChar.IsMatch(PasswordEntry))
                    passwordwarningvisibility =true;
                else
                    passwordwarningvisibility = false;

                PropertyChanged(this, new PropertyChangedEventArgs("PasswordWarningVisibility"));
            }
        }

        public string PasswordConfirm
        {
            get { return passwordconfirm; }
            set
            {
                passwordconfirm = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PasswordConfirm"));

                if (PasswordConfirm != PasswordEntry)
                    ConfirmWarningVisibility = true;
                else
                    ConfirmWarningVisibility = false;
            }
        }

        public bool ConfirmWarningVisibility
        {
            get { return confirmwarningvisibility; }
            set
            {
                if (PasswordConfirm != PasswordEntry)
                    ConfirmWarningVisibility = true;
                else
                    ConfirmWarningVisibility = false;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmWarningVisbility"));
            }
        }
       
        //--------------------------------------------------------------------------------------

        /*
        public string NumberEntry
        {
            get { return numberentry; }
            set
            {
                numberentry = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NumberEntry"));

                if (numberentry.Length != 10)
                    NumberWarningVisibility = true;
                else
                    NumberWarningVisibility = false;
            }
        }
        
        public bool NumberWarningVisibility
        {
            get { return numberwarningvisibility; }
            set
            {
                if (NumberEntry.Length != 10)
                   numberwarningvisibility = true;
                else
                   numberwarningvisibility = false;
                PropertyChanged(this, new PropertyChangedEventArgs("NumberWarningVisiblity"));
            }
        }


        //---------------------------------------------------------------------------------------------
      
        private bool namewarningvisibility = false;
        public string NameEntry
        {
            get { return nameentry; }
            set
            {
                nameentry = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NameEntry"));
                if (string.IsNullOrEmpty(nameentry))
                    NameWarningVisibility = true;
                else
                    NameWarningVisibility = false;
            }
        }
        
        public bool NameWarningVisibility
        {
            get { return namewarningvisibility; }
            set
            {
                namewarningvisibility = string.IsNullOrEmpty(NameEntry);
                PropertyChanged(this, new PropertyChangedEventArgs("NameWarningVisiblity"));
            }
        }
        //----------------------------------------------------------------------------------------------
        
        private bool addresswarningvisibility = false;
        public string AddressEntry
        {
            get { return addressentry; }
            set
            {
                addressentry = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AddressEntry"));
                AddressWarningVisibility = string.IsNullOrEmpty(addressentry);
            }
        }
        
        public bool AddressWarningVisibility
        {
            get { return addresswarningvisibility; }
            set
            {
                addresswarningvisibility = string.IsNullOrEmpty(AddressEntry);
                PropertyChanged(this, new PropertyChangedEventArgs("AddressWarningVisiblity"));
            }
        }

        */
        //-----------------------------------------------------------------------------------------------------
        public ICommand CreateCommand { protected set; get; }

        public CreatePageViewModel(IRoutingService navigationService = null)
        {
            CreateCommand = new Command(OnCreate);
            
            _navigationService = navigationService ?? Locator.Current.GetService<IRoutingService>();
          

        }
        /*
        public void EmailChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            var emailRegex = new Regex(emailPattern, RegexOptions.IgnoreCase);
            var isEmailValid = emailRegex.IsMatch(args.ToString());

            var entry = args.ToString();

            if (entry.Length > 0 && isEmailValid)
            {
                EmailEntry = args.NewTextValue;
                
            }
            else
            {
                EmailWarningVisibility = true;
            }


        }


        */

       
        public async void OnCreate()
        {
            if ( /*!string.IsNullOrEmpty(NameEntry) &&
                !string.IsNullOrEmpty(AddressEntry) &&
                NumberEntry.Length == 10 && !string.IsNullOrEmpty(NumberEntry)
                && */emailRegex.IsMatch(EmailEntry)
                && !string.IsNullOrEmpty(EmailEntry)
                && !string.IsNullOrEmpty(PasswordEntry)
                && !string.IsNullOrEmpty(PasswordConfirm)
                && hasNumber.IsMatch(PasswordEntry)
                && hasLowerChar.IsMatch(PasswordEntry)
                && hasMinimum8Chars.IsMatch(PasswordEntry)
                && hasUpperChar.IsMatch(PasswordEntry)
                && PasswordConfirm== PasswordEntry)

            {
                try
                {/*
                    await App.Database.SavePersonAsync(new Person
                    {
                        //Name = NameEntry,


                        //Email = EmailEntry,
                        Password = PasswordEntry,

                        Address = AddressEntry,
                        Number = NumberEntry,

                        //Major = majorEntry.Text, 
                        //Bio = bioEntry.Text, 
                        //Hobbies = hobbyEntry.Text
                    });*/
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailEntry, PasswordEntry);
                    string gettoken = auth.FirebaseToken;
                    await authProvider.UpdateProfileAsync(auth.FirebaseToken, EmailEntry, "");
                    await App.Current.MainPage.DisplayAlert("Alert", "Your acoount is succesfully created. Please verify your account using OTP(One Time Password)", "Ok");


                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    
                }
                catch(Exception e)
                {
                    if (string.IsNullOrEmpty(emailentry))
                        DisplayInvalidEmailPrompt();

                    else if (!emailRegex.IsMatch(EmailEntry))
                        DisplayInvalidEmailPrompt();


                    /*else if (string.IsNullOrWhiteSpace(NameEntry))
                        DisplayInvalidNamePrompt();
                    */
                    else if (string.IsNullOrWhiteSpace(PasswordEntry)
                        || !emailRegex.IsMatch(EmailEntry)
                    || !hasNumber.IsMatch(PasswordEntry)
                    || !hasLowerChar.IsMatch(PasswordEntry)
                    || !hasMinimum8Chars.IsMatch(PasswordEntry)
                    || !hasUpperChar.IsMatch(PasswordEntry))
                        DisplayInvalidPasswordPrompt();
                    else if (string.IsNullOrWhiteSpace(PasswordConfirm)
                        || PasswordConfirm != PasswordEntry)
                        DisplayInvalidNumberPrompt();
                        
                    /*
                    else if (string.IsNullOrWhiteSpace(AddressEntry))
                        DisplayInvalidAddressPrompt();
                    else if (NumberEntry.Length != 10)
                        DisplayInvalidNumberPrompt();
                    */
                }
            }
            else
            {
                if (string.IsNullOrEmpty(emailentry))
                    DisplayInvalidEmailPrompt();
                else if (!emailRegex.IsMatch(EmailEntry))
                    DisplayInvalidEmailPrompt();
                /*
                else if (string.IsNullOrWhiteSpace(NameEntry))
                    DisplayInvalidNamePrompt();
                */
                else if (string.IsNullOrWhiteSpace(PasswordEntry)
                    || !emailRegex.IsMatch(EmailEntry)
                || !hasNumber.IsMatch(PasswordEntry)
                || !hasLowerChar.IsMatch(PasswordEntry)
                || !hasMinimum8Chars.IsMatch(PasswordEntry)
                || !hasUpperChar.IsMatch(PasswordEntry))
                    DisplayInvalidPasswordPrompt();
                /*
                else if (string.IsNullOrWhiteSpace(AddressEntry))
                    DisplayInvalidAddressPrompt();
                else if (NumberEntry.Length != 10)
                    DisplayInvalidNumberPrompt();
                */
            }
        }
    }
}
