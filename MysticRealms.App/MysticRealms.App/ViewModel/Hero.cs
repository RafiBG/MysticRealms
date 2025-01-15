namespace MysticRealms.App.ViewModel
{
    public abstract class Hero
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }

        public int Score { get; set; }
        public int BestScore { get; set; }
        public int Coins { get; set; }

        public abstract string GetStats();


        public void MakeStrongerHero(int score)
        {
            if (score % 2 == 0) // Increase strength every 2 scores
            {
                Health += 10;
                Attack += 2;
                Defense += 1;
                Magic += 1;
            }
        }

        // Give more money every score
        public void GiveMoreMoney(int score)
        {
            Coins += score * 25;
        }
    }
}
