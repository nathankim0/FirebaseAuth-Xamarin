using System;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NathanFirebaseAuth
{
    public partial class MyDashboardPage : ContentPage
    {
        private readonly string WebAPIkey = "AIzaSyDuUX2cRjUinWLuqPq7cfbePTvNeREJAqM";
        public MyDashboardPage()
        {
            InitializeComponent();

            GetProfileInformationAndRefreshToken();
        }

        private async void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                // 로그인할 때 저장했던 토큰
                var savedfirebaseauth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));

                //Here we are Refreshing the token
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));

                //Now lets grab user information
                MyUserName.Text = savedfirebaseauth.User.Email;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Alert", "Oh no !  Token expired", "OK");
            }
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            Application.Current.MainPage = new NavigationPage(new MainPage());

        }
    }
}
