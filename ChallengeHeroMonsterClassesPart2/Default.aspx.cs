using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClassesPart2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Dice dice = new Dice();
            Character hero = new Character();
            hero.Name = "Hero";
            hero.Health = 100;
            hero.DamageMaximum = 30;
            hero.AttackBonus = true;

            Character monster = new Character();
            monster.Name = "Monster";
            monster.Health = 100;
            monster.DamageMaximum = 25;
            monster.AttackBonus = false;

            if (hero.AttackBonus)
                monster.Defend(hero.Attack(dice));
            if (monster.AttackBonus)
                hero.Defend(monster.Attack(dice));

            while(hero.Health > 0 && monster.Health > 0)
            {
                monster.Defend(hero.Attack(dice));
                hero.Defend(monster.Attack(dice));

                DisplayStats(hero);
                DisplayStats(monster);

                DisplayResult(hero, monster);
            }
        }

        private void DisplayStats(Character character)
        {
            resultLabel.Text += String.Format("Name: {0}, Health: {1}, Damage Maximum: {2}, Attack Bonus: {3}</br>",
                character.Name, character.Health, character.DamageMaximum, character.AttackBonus);
        }

        private void DisplayResult(Character opponent1, Character opponent2)
        {
            if(opponent1.Health <= 0 && opponent2.Health <= 0)
            {
                resultLabel.Text += String.Format("{0} and {1} both died.", opponent1.Name, opponent2.Name);
            }
            else if(opponent1.Health <= 0)
            {
                resultLabel.Text += String.Format("{0} defeats {1}", opponent2.Name, opponent1.Name);
            }
            else if(opponent2.Health <= 0)
            {
                resultLabel.Text += String.Format("{0} defeats {1}", opponent1.Name, opponent2.Name);
            }
            
        }
    }

    class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }

        public int Attack(Dice dice)
        {
            dice.Sides = DamageMaximum + 1;
            return dice.Roll();
        }

        public void Defend(int damage)
        {
            Health -= damage;
        }
    }

    class Dice
    {
        public int Sides { get; set; }
        Random random = new Random();

        public int Roll()
        {
            return random.Next(Sides);
        }
    }
}