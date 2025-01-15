namespace MysticRealms.App.ViewModel
{
    public class Bandit : Hero
    {
        // Name, Health, Attack, Defense, Magic
        public Bandit()
        {
            Name = "Bandit";
            ImageSource = "bandit.png";
            Health = 120;
            Attack = 25;
            Defense = 15;
            Magic = 10;
        }

        public override string GetStats()
        {
            return $"Health: {Health}\nAttack: {Attack}\nDefense: {Defense}\nMagic: {Magic}";
        }
    }
}
