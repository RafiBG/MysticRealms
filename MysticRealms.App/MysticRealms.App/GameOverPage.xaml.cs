using MysticRealms.App.ViewModel;

namespace MysticRealms.App;

public partial class GameOverPage : ContentPage
{
    private Hero SelectedHero { get; set; }

    public GameOverPage(string message, Hero selectedHero)
    {
        InitializeComponent();
        GameOverMessage.Text = message;
        SelectedHero = selectedHero;
        UserScoreReset();
    }

    private async void btnPlayAgain(object sender, EventArgs e)
    {
        //SelectedHero.Score = 0;
        await Navigation.PopToRootAsync();
        var singleGamePlayPage = new SingleGamePlayPage(SelectedHero, new Enemy(250, 20));
        App.Current.MainPage = new NavigationPage(singleGamePlayPage);
    }

    private async void btnExitToMenu(object sender, EventArgs e)
    {
        //SelectedHero.Score = 0;
        await Navigation.PopToRootAsync();
        App.Current.MainPage = new NavigationPage(new SelectHeroGamePage());
    }

    private async void UserScoreReset()
    {
        //UserLostScore.Text = $"Score: {SelectedHero.Score}";
        await DatabaseHelper.UpdateUserScore(0);
        
    }
}
