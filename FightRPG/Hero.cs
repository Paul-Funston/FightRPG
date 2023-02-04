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

 
        public new string Examine()
        {
            return $"{Name} " + base.Examine();
        }

        

        public Hero(string name, int level, int health, int strength, int defence, Armor startingArmor, Weapon startingWeapon) : base(name, level, health, strength, defence)
        {
            EquipItem(startingArmor);
            EquipItem(startingWeapon);

        }
    }
}
