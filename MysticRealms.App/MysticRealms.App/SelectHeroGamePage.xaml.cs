using MysticRealms.App.ViewModel;
using static MysticRealms.App.DatabaseHelper;
namespace MysticRealms.App;

public partial class SelectHeroGamePage : ContentPage
{
    public Hero SelectedHero { get; private set; }
    public Enemy DefaultEnemy { get; private set; }
    //private string _username;

    public SelectHeroGamePage()
    {
        InitializeComponent();
        //_username = username;
        HeroPicker.SelectedIndexChanged += HeroSelected;
        DefaultEnemy = new Enemy(210, 20); // Health and Attack
        string username = SessionHelper.LoggedUsername;
        LoggedUser.Text = "Welcome, " + username ;
    }

    private void HeroSelected(object sender, EventArgs e)
    {
        Picker picker = sender as Picker;
        if (picker != null && picker.SelectedIndex != -1)
        {
            string selectedHeroName = picker.Items[picker.SelectedIndex];
            SelectedHero = HeroFactory.CreateHero(selectedHeroName);
            if (SelectedHero != null)
            {
                HeroImage.IsVisible = true;
                HeroImage.Source = SelectedHero.ImageSource;
                StatsLog.Text = $"Hero: {SelectedHero.Name}\n \n{SelectedHero.GetStats()}";
            }
        }
    }



    private async void btnSinglePlay(object sender, EventArgs e)
    {
        if (SelectedHero == null)
        {
            await DisplayAlert("Hero is not selected", "You must select a hero before you can play singleplay", "OK");
        }
        else
        {
            var defaultEnemy = new Enemy(DefaultEnemy.Health, DefaultEnemy.Attack);
            var singleGamePlayPage = new SingleGamePlayPage(SelectedHero, defaultEnemy);
            NavigationPage.SetHasNavigationBar(singleGamePlayPage, false);
            App.Current.MainPage = new NavigationPage(singleGamePlayPage);
        }
    }

    private async void btnUserStats(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShowUserStatsPage());
    }
    private void btnMultiplayer(object sender, EventArgs e)
    {
        
    }
}
