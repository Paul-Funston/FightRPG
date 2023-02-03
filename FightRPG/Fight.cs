using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Fight
    {
        private HashSet<Hero> _party;
        private List<Monster> _enemies;
        private bool _isActive = true;

        private int _goldReward;
        public int GoldReward { get { return _goldReward; } }

        private int _xpReward;
        public int XpReward { get { return _xpReward; } }



        public Fight(HashSet<Hero> party, List<Monster> enemies)
        {

        }
    }
}
