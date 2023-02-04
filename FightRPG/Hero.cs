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
        public Armor EquippedArmor { get { return _equippedArmor; } }

        
        public Weapon EquippedWeapon { get { return _equippedWeapon; } }

        public new int GetEffectiveStrength()
        {
            return base.GetEffectiveStrength() + _equippedArmor.Strength + _equippedWeapon.Strength;
        }

        public new int GetEffectiveDefence()
        {
            return base.GetEffectiveDefence() + _equippedArmor.Defence + _equippedWeapon.Defence;
        }

        public void EquipItem(Armor armor)
        {
            _equippedArmor= armor;
        }

        public void EquipItem(Weapon weapon)
        {
            _equippedWeapon= weapon;
        }

 
        

        public Hero(string name, int strength, int defence, int health, int level, Armor startingArmor, Weapon startingWeapon) : base(name, strength, defence, health, level)
        {
            EquipItem(startingArmor);
            EquipItem(startingWeapon);

        }
    }
}
