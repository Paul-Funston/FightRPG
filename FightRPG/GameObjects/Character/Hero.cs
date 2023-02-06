using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightRPG;

namespace FightRPG
{
    public class Hero : GameCharacter
    {
        private Armor _equippedArmor;
        private Weapon _equippedWeapon;
        public Armor EquippedArmor { get { return _equippedArmor; } }

        
        public Weapon EquippedWeapon { get { return _equippedWeapon; } }

        public override int GetEffectiveStrength()
        {
            return base.GetEffectiveStrength() + _equippedArmor.Strength + _equippedWeapon.Strength;
        }

        public override int GetEffectiveDefence()
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
        private void ExamineEnemy()
        {

        }

        protected override void AttackEnemy()
        {
            int targetId = GetTarget();

            Game.CharacterUseAbility(this, new Ability("Testing Ability").Id, targetId);
        }

        protected override int GetTarget()
        {
            int targetId = -1;
            while (targetId < 0)
            {
                targetId = Game.PlayerSelectTarget();
            }
            return targetId;
        }
        private void UseItem()
        {

        }
        private void UseSkill()
        {

        }
        

        public Hero(string name, int level, int health, int strength, int defence, Armor startingArmor, Weapon startingWeapon) : base(name, level, health, strength, defence)
        {
            _equippedArmor = startingArmor;
            _equippedWeapon = startingWeapon;
            Assets.AddHero(Id, this);

            //_actionsAvailable.Add("Attack", AttackEnemy); - Moved to GameCharacter
            //_actionsAvailable.Add("Skill", UseSkill);

            _actionsAvailable.Add("Examine", ExamineEnemy);
            //_actionsAvailable.Add("Item", UseItem);

        }
    }
}
