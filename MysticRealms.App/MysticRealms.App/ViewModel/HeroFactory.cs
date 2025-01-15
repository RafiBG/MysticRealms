namespace MysticRealms.App.ViewModel
{
    public static class HeroFactory
    {
        private static readonly Dictionary<string, Func<Hero>> HeroCreators = new Dictionary<string, Func<Hero>>
            {
                { "Knight", () => new Knight() },
                { "Wizard", () => new Wizard() },
                { "Bandit", () => new Bandit() },
                { "Princess", () => new Princess() },
                { "Archer", () => new Archer() }
            };

        public static Hero CreateHero(string name)
        {
            return HeroCreators.TryGetValue(name, out var creator) ? creator() : null;
        }
    }
}
