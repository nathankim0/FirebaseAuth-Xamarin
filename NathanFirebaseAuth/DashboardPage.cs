using System;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NathanFirebaseAuth
{
    public partial class DashboardPage : ContentPage
    {
        private readonly Label userName;

        public DashboardPage()
        {
            Title = "Main Page";

            var toolbarItem = new ToolbarItem
            {
                Text = "Logout"
            };
            toolbarItem.Clicked += OnLogoutButtonClicked;
            ToolbarItems.Add(toolbarItem);

            userName = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            Content = new StackLayout
            {
                VerticalOptions=LayoutOptions.CenterAndExpand,
                Children = {
                    userName,
                }
            };
            GetProfileInformationAndRefreshToken();
        }

        private async void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.WebAPIkey));
            try
            {
                // 로그인할 때 저장했던 토큰
                var savedfirebaseauth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));

                //Here we are Refreshing the token
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));

                //Now lets grab user information
                userName.Text = savedfirebaseauth.User.Email;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Alert", "Token expired", "OK");
            }
        }

        private void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
