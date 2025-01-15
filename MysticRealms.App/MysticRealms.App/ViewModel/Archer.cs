namespace MysticRealms.App.ViewModel
{
    public class Archer : Hero
    {
   
        public Archer()
        {
            Name = "Archer";
            ImageSource = "archer.png";
            Health = 95;
            Attack = 15;
            Defense = 10;
            Magic = 10;
        }

        public override string GetStats()
        {
            return $"Health: {Health}\nAttack: {Attack}\nDefense: {Defense}\nMagic: {Magic}";
        }
    }
}
