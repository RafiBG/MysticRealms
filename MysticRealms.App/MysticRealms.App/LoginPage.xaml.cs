using static MysticRealms.App.DatabaseHelper;

namespace MysticRealms.App
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;
            if (username == null && password == null )
            {
                await DisplayAlert("Åmpty space", "Fill in the blanks", "OK");
            }
            else if (await DatabaseHelper.ValidateUser(username, password))
            {
                SessionHelper.LoggedUsername = username;
                //await DisplayAlert("Success", "Login Successful!", "OK");
               // Navigation.PushAsync(new SelectHeroGamePage());
                await Navigation.PushAsync(new SelectHeroGamePage()); // pass username

                UsernameEntry.Text = "";
                PasswordEntry.Text = "";
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }

        private async void btnRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        public static class SessionManager
        {
            public static string LoggedInUsername { get; set; }
        }
    }
}
