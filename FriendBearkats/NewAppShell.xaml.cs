using FriendBearkats.Views;
using Xamarin.Forms;

namespace FriendBearkats
{
    public partial class NewAppShell : Shell
    {
        public NewAppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            
            Routing.RegisterRoute(nameof(FindPage), typeof(FindPage));

        }

    }
}
