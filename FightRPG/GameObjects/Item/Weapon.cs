﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Weapon : Equipment
    {


        public Weapon(string name, int value, int strength, int defence) : base(name, value, strength, defence)
        {
            Assets.AddWeapon(Id, this);
        }
    }
}
