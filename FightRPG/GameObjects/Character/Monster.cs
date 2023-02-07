using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;


namespace FightRPG
{

    public class Monster : GameCharacter
    {
        protected int _bestiaryIndex;
        protected string _nickname;
        public string Nickname { get { return _nickname; } }
        public override string Name { get { return _nickname + " " + _name; } }
        public string FullName { get { return _nickname + " " + _name; } }
        protected readonly int _xpPrize = 1;
        public int XpPrize { get { return _xpPrize; } }
        protected readonly int _goldPrize = 1;
        public int GoldPrize { get { return _goldPrize; } }


        public void SetBonusStats(int level)
        {
            _bonusDefence = level;
            _bonusHealth= level;
            _bonusStrength = level;
        }

        public HashSet<Hero> FindTargets(HashSet<Hero> team)
        {
            foreach(Hero hero in team)
            {
                if (hero.CurrentHealth > 0)
                {
                    return new HashSet<Hero>() { hero};
                }
            }

            return team;
        }

        

        public override string Examine()
        {
            return $"The {Name} " + base.Examine();
        }

        public Monster(string name, int level, int health, int strength, int defence, int bestiaryIndex, int xp = 1, int gold = 2) : base(name, level, health, strength, defence)
        {
            _bestiaryIndex = bestiaryIndex;
            if (xp < 0 || gold < 0)
            {
                throw new Exception("Monster rewards cannot be less than 0");
            }

            SetBonusStats(level);
            SetCurrentHealthToMax();
            _nickname = Assets.GetRandomAdjective();
            Assets.AddMonster(Id, this);
            
              
        }

 
    }


}
