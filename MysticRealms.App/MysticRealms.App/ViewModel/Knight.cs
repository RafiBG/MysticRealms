namespace MysticRealms.App.ViewModel
{
    public class Knight : Hero
    {

        public Knight()
        {
            Name = "Knight";
            ImageSource = "knight.png";
            Health = 150;
            Attack = 20;
            Defense = 30;
            Magic = 5;

        }
        public override string GetStats()
        {
            return $"Health: {Health}\nAttack: {Attack}\nDefense: {Defense}\nMagic: {Magic}";
        }
    }
}

