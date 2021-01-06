using Xamarin.Forms;
using AmplitudeService;
using System.Collections.Generic;

namespace NathanFirebaseAuth
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            Amplitude.Initialize(Constants.api_key);
            Amplitude.InstanceFor(Constants.userId, Constants.userProperties).StartSession();


            Amplitude.InstanceFor(Constants.userId, Constants.userProperties).Track("start");

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
