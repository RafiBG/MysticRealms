using MysticRealms.App.ViewModel;
using static MysticRealms.App.DatabaseHelper;

namespace MysticRealms.App;

public partial class WinGamePage : ContentPage
{
    private Hero SelectedHero { get; set; }
    //private string _username;

    public WinGamePage(Hero selectedHero)
    {
        InitializeComponent();
        SelectedHero = selectedHero;
        WinMessage.Text = "Congratulations! You have won the game!";
        SelectedHero.Score += 1;
        UserScoreUpdate();
        WinCoins(SelectedHero.Score);
    }

    private async void btnContinue(object sender, EventArgs e)
    {
        //SelectedHero.Score += 1;
        // stronger hero based on score
        SelectedHero.MakeStrongerHero(SelectedHero.Score);
        var enemy = new Enemy(210, 20);
        // stronger enemy based on score
        enemy.MakeStrongerEnemy(SelectedHero.Score);

        var coins = new Coins(30);

        var singleGamePlayPage = new SingleGamePlayPage(SelectedHero, enemy);
        await Navigation.PushAsync(singleGamePlayPage);
    }

    private async void btnExitMenu(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
        App.Current.MainPage = new NavigationPage(new SelectHeroGamePage());
    }

    private async void UserScoreUpdate()
    {
        //int score = 0;
        UserScore.Text = $"Score: {SelectedHero.Score}";
        //UserScore.Text = $"Score: {SelectedHero.Score}";
        await DatabaseHelper.UpdateUserScore(SelectedHero.Score);
        if (SelectedHero.Score > SelectedHero.BestScore)
        {
            SelectedHero.BestScore = SelectedHero.Score;
            await DatabaseHelper.UpdateUserBestScore(SelectedHero.BestScore);
        }
    }

    private async Task WinCoins(int score)
    {
        int baseCoins = 10; // Base reward for beating the monster
        int additionalCoins = score / 10; // Extra coins based on score
        int totalCoinsEarned = baseCoins + additionalCoins;

        await DatabaseHelper.UpdateUserCoins(SessionHelper.LoggedUsername, totalCoinsEarned);
        UserCoins.Text = $"Coins: {totalCoinsEarned}";
    }

}
