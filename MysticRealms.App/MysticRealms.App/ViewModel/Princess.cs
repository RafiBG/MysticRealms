namespace MysticRealms.App.ViewModel
{
    public class Princess : Hero
    {
        public Princess()
        {
            Name = "Princess";
            ImageSource = "princess.png";
            Health = 80;
            Attack = 10;
            Defense = 5;
            Magic = 20;
        }

        public override string GetStats()
        {
            return $"Health: {Health}\nAttack: {Attack}\nDefense: {Defense}\nMagic: {Magic}";
        }
    }
}
