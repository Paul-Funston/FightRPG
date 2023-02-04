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

        public Monster(string name, int strength, int defence, int health, int level, int bestiaryIndex) : base(name, strength, defence, health, level)
        {
            _bestiaryIndex = bestiaryIndex;
            SetBonusStats(level);
            SetCurrentHealthToMax();
            _nickname = Assets.GetRandomAdjective();
              
        }

 
    }


}
