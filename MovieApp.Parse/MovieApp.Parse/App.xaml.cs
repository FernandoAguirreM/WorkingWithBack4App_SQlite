using Xamarin.Forms;

namespace MovieApp.Parse
{
    public partial class App : Application
    {
        public App()
        {
            ServiceClient.Instance.Init();
            InitializeComponent();

            MainPage = new MovieApp_ParsePage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
