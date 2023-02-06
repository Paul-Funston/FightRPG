using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Ability : Item
    {

        public Ability(string name) : base(name, 0)
        {
            Assets.AddAbility(Id, this);
        }
    }
}
