using MysticRealms.App.ViewModel;

namespace MysticRealms.App;

public partial class SingleGamePlayPage : ContentPage
{
    private Hero _playerHero;
    private int _playerHealth;
    private bool _isPlayerDefending;

    private Enemy _enemy;

    public SingleGamePlayPage(Hero playerHero, Enemy enemy)
    {
        InitializeComponent();
        // Player stats
        _playerHero = playerHero;
        _playerHealth = playerHero.Health;
        _isPlayerDefending = false;

        // Enemy stats
        _enemy = enemy;

        HeroImage.Source = _playerHero.ImageSource;

        PlayerHealthBarUpdate();
        EnemyHealthBarUpdate();
        RandomEnemyImage();
    }

    private void btnAttack(object sender, EventArgs e)
    {
        _enemy.Health -= _playerHero.Attack;
        if (_enemy.Health <= 0)
        {
            _enemy.Health = 0;
            GameLogUpdate("\n You have defeated the enemy! \n You win!");
            EnemyHealthBarUpdate();
            ShowWinGamePage();
        }
        else
        {
            GameLogUpdate($"\n You attack {_playerHero.Attack} dmg! Enemy health: {_enemy.Health}");
            EnemyHealthBarUpdate();

            EnemyTurn();
        }
    }

    private void btnDefend(object sender, EventArgs e)
    {
        _isPlayerDefending = true;
        GameLogUpdate("\n You defend!");

        EnemyTurn();
    }

    private void btnUseItem(object sender, EventArgs e)
    {
        _playerHealth += 45;
        if (_playerHealth > _playerHero.Health)
        {
            _playerHealth = _playerHero.Health;
        }
        GameLogUpdate($"\n You use an item! Your health: {_playerHealth}");
        PlayerHealthBarUpdate();
    }

    private void EnemyTurn()
    {
        int damage = _enemy.Attack;

        if (_isPlayerDefending)
        {
            damage -= _playerHero.Defense;
            if (damage < 0) damage = 0; // Prevent negative damage
            _isPlayerDefending = false;
        }

        _playerHealth -= damage;
        if (_playerHealth <= 0)
        {
            _playerHealth = 0;
            GameLogUpdate("\n You have been defeated! \n Game Over!");
            PlayerHealthBarUpdate();
            ShowGameOverPage("You have been defeated! Game Over!");
        }
        else
        {
            GameLogUpdate($"\n Enemy attacks {damage} dmg! Your health: {_playerHealth}");
            PlayerHealthBarUpdate();
        }
    }

    private void PlayerHealthBarUpdate()
    {
        if (_playerHealth <= 35)
        {
            PlayerHealthBar.ProgressColor = Colors.Red;
        }
        else if (_playerHealth <= 65)
        {
            PlayerHealthBar.ProgressColor = Colors.Orange;
        }
        else
        {
            PlayerHealthBar.ProgressColor = Colors.Green;
        }
        PlayerHealthBar.Progress = (double)_playerHealth / _playerHero.Health;
        PlayerHealthNumber.Text = _playerHealth.ToString();
    }

    private void EnemyHealthBarUpdate()
    {
        if (_enemy.Health <= 35)
        {
            EnemyHealthBar.ProgressColor = Colors.Red;
        }
        else if (_enemy.Health <= 65)
        {
            EnemyHealthBar.ProgressColor = Colors.Orange;
        }
        else
        {
            EnemyHealthBar.ProgressColor = Colors.Green;
        }
        EnemyHealthBar.Progress = (double)_enemy.Health / 210.0;
        EnemyHealthNumber.Text = _enemy.Health.ToString();
    }

    private void GameLogUpdate(string text)
    {
        ActionGameLog.Text = ActionGameLog.Text + text;
    }

    private async void ShowGameOverPage(string message)
    {
        await Navigation.PushAsync(new GameOverPage(message, _playerHero));
    }

    private async void ShowWinGamePage()
    {
        await Navigation.PushAsync(new WinGamePage(_playerHero));
    }

    private void RandomEnemyImage()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 3);
        EnemyImage.Source = $"dragon{randomNumber}.png";
    }
}
