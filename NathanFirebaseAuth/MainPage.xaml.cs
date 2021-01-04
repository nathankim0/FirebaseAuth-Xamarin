using System;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NathanFirebaseAuth
{
    public partial class MainPage : ContentPage
    {
        private readonly string WebAPIkey = "AIzaSyDuUX2cRjUinWLuqPq7cfbePTvNeREJAqM";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Signupbutton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(UserNewEmail.Text, UserNewPassword.Text);

                string gettoken = auth.FirebaseToken;
                await Application.Current.MainPage.DisplayAlert("Alert", gettoken, "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

        }

        private async void Loginbutton_Clicked(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserLoginEmail.Text, UserLoginPassword.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontent);
                await Navigation.PushAsync(new MyDashboardPage());
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }
        }
    }
}
