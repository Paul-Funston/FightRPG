using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Hero : GameCharacter
    {
        private Armor _equippedArmor;
        private Weapon _equippedWeapon;
        public Armor EquippedArmor { get; set; }
        public Weapon EquippedWeapon { get; set; }


        //public Hero() { }

        public Hero(string name, int strength, int defence, int health, int level, Armor startingArmor, Weapon startingWeapon) : base(name, strength, defence, health, level)
        {
            EquippedArmor = startingArmor;
            EquippedWeapon = startingWeapon;

        }
    }
}
