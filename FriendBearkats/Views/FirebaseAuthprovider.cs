using Firebase.Auth;

namespace FriendBearkats.Views
{
    internal class FirebaseAuthprovider : FirebaseAuthProvider
    {
        public FirebaseAuthprovider(FirebaseConfig authConfig) : base(authConfig)
        {
        }
    }
}