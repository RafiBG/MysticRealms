namespace MysticRealms.App.ViewModel
{
    public class Wizard : Hero
    {
        
        public Wizard()
        {
            Name = "Wizard";
            ImageSource = "wizard.png";
            Health = 100;
            Attack = 15;
            Defense = 10;
            Magic = 40;
        }

        public override string GetStats()
        {
            return $"Health: {Health}\nAttack: {Attack}\nDefense: {Defense}\nMagic: {Magic}";
        }
    }
}
