namespace MysticRealms.App
{
    public class Enemy
    {
        public int Health { get; set; }
        public int Attack { get; set; }

        public Enemy(int health, int attack)
        {
            Health = health;
            Attack = attack;
        }

        public void MakeStrongerEnemy(int score)
        {
            Health += score * 20;
            Attack += score * 4;
        }
    }
}
