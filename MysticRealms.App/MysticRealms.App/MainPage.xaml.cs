namespace MysticRealms.App
{
    public partial class MainPage : ContentPage
    {
        //private readonly UserRepository _userRepository;

        public MainPage()
        {
            
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            //string username = "Not logged user";
            //App.Current.MainPage = new NavigationPage(new SelectHeroGamePage());
            await Navigation.PushAsync(new LoginPage());
        }
    }

}
