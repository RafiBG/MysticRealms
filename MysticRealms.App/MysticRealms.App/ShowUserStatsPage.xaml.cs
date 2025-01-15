using MysticRealms.App.ViewModel;

namespace MysticRealms.App;

public partial class ShowUserStatsPage : ContentPage
{
	public ShowUserStatsPage()
	{
		InitializeComponent();
        LoadUserStats();
    }

    private async void btnGoBack(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelectHeroGamePage());
    }

    private async void LoadUserStats()
    {
        var userStats = await DatabaseHelper.GetUserStats();
        if (userStats != null)
        {
            ShowUsername.Text = $"Username: {userStats.Username}";
            ShowScore.Text = $"Score: {userStats.Score}";
            ShowBestScore.Text = $"Best Score: {userStats.BestScore}";
            ShowCoins.Text = $"Coins: {userStats.Coins}";
            ShowHealthPotions.Text = $"Health Potions: {userStats.HealthPotions}";
        }
        else
        {
            await DisplayAlert("Error", "Failed to load user stats.", "OK");
        }
    }
}