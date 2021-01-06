using System;
using AmplitudeService;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NathanFirebaseAuth
{
	public class SignUpPage : ContentPage
	{
        private readonly Entry emailEntry;
        private readonly Entry passwordEntry;

        public SignUpPage()
		{
			Title = "Sign Up";

			passwordEntry = new Entry
			{
				IsPassword = true
			};
			emailEntry = new Entry();

			var signUpButton = new Button
			{
				Text = "Sign Up"
			};
			signUpButton.Clicked += OnSignUpButtonClicked;

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					new Label { Text = "Email address" },
					emailEntry,

					new Label { Text = "Password" },
					passwordEntry,
					
					signUpButton,
				}
			};
		}

		private async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			try
			{
				
				var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.WebAPIkey));
				var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(emailEntry.Text, passwordEntry.Text);

				string gettoken = auth.FirebaseToken;
				await Application.Current.MainPage.DisplayAlert("Alert", gettoken, "Ok");

				Amplitude.InstanceFor(Constants.userId, Constants.userProperties).Track("sign up");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
			}
		}
	}
}
