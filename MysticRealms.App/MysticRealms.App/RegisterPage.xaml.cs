namespace MysticRealms.App
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void btnRegister(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;
            if (username == null && password == null && email == null)
            {
                await DisplayAlert("Åmpty space", "Fill in the blanks", "OK");
            }
            else if (await DatabaseHelper.UserExists(username))
            {
                await DisplayAlert("Error", "Username already exists.", "OK");
            }
            else
            {
                await DatabaseHelper.RegisterUser(username, email, password);
                //await DisplayAlert("Success", "Registration Successful!", "OK");
                Navigation.PushAsync(new LoginPage());

                UsernameEntry.Text = "";
                EmailEntry.Text = "";
                PasswordEntry.Text = "";
            }
        }
    }
}
