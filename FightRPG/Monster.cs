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
        public new string Name { get { return _nickname + " " + _name; } }
        public string FullName { get { return _nickname + " " + _name; } }
        protected int _xpPrize = 1;
        public int XpPrize { get { return _xpPrize; } }
        protected int _goldPrize = 1;
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

        

        public new string Examine()
        {
            return $"The {FullName} " + base.Examine();
        }

        public Monster(string name, int level, int health, int strength, int defence, int bestiaryIndex) : base(name, level, health, strength, defence)
        {
            _bestiaryIndex = bestiaryIndex;
            SetBonusStats(level);
            SetCurrentHealthToMax();
            _nickname = Assets.GetRandomAdjective();
            Assets.AddMonster(Id, this);
              
        }

 
    }


}
