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
        private static Dictionary<string, int> _monsterIds = new();


        public void SetBonusStats(int level)
        {
            _bonusDefence = level;
            _bonusHealth= level;
            _bonusStrength = level;
        }

        /*
        public void IncreaseHealth()
        {
            OriginalHealth += Level;
            CurrentHealth = OriginalHealth;
        }
        */
        //public Monster() { }

        public Monster(string name, int strength, int defence, int health, int level, int bestiaryIndex) : base(name, strength, defence, health, level)
        {
            _bestiaryIndex = bestiaryIndex;
            SetBonusStats(level);

        }

        public class Goblin : Monster
        {
            public Goblin(int level) : base("Goblin", 5, 1, 10, level, 1) { }
        }
        public class Kobold : Monster
        {
            public Kobold(int level) : base("Kobold", 5, 1, 10, level, 2) { }
        }
        public class Slime : Monster
        {
            public Slime(int level) : base("Slime", 1, 5, 10, level, 3) { }
        }
        public class YuanTi : Monster
        {
            public YuanTi(int level) : base("YuanTi", 6, 4, 15, level, 4) { }
        }
        public class Dragon : Monster
        {
            public Dragon(int level) : base("Dragon", 10, 10, 30, level, 5) { }
        }
    }


}
