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

        private Dictionary<string, int> _equippedEquipment = new();
        private Dictionary<Type, int> testDictionary= new(); // Test This!
        public void SetArmor(int id)
        {
            Armor? armor = Assets.GetObjectById<Armor>(id);
            if (armor != null)
            {
                _equippedEquipment["Armor"] = id;
            } else
            {
                throw new Exception("Only armor can be equipped to the armor slot.");
            }
        }
        public void SetWeapon(int id) 
        {
            Weapon? armor = Assets.GetObjectById<Weapon>(id);
            if (armor != null)
            {
                _equippedEquipment["Weapon"] = id;
            }
            else
            {
                throw new Exception("Only weapons can be equipped to the weapon slot.");
            }
        }


        
        public Weapon GetWeapon()
        {
            int id = _equippedEquipment["Weapon"];

            return Assets.GetObjectById<Weapon>(id);
        }
        public Weapon GetArmor()
        {
            int id = _equippedEquipment["Armor"];

            return Assets.GetObjectById<Weapon>(id);
        }

        
        public void DisplayEquipment()
        {
            foreach(KeyValuePair<string, int> pair in _equippedEquipment)
            {
                Equipment equip = Assets.GetObjectById<Equipment>(pair.Value);
                if(equip != null)
                {
                    Console.WriteLine($"{pair.Key}: Strength{equip.Strength} Defence: {equip.Defence}.");
                }
                
            }
        }
        

        public override int GetEffectiveStrength()
        {

            return base.GetEffectiveStrength() + _equippedArmor.Strength + _equippedWeapon.Strength;
        }

        public override int GetEffectiveDefence()
        {
            return base.GetEffectiveDefence() + _equippedArmor.Defence + _equippedWeapon.Defence;
        }

        
        public override string Examine()
        {
            return $"{Name} " + base.Examine();
                    
        }
        
        public string GetStatus()
        {
            return $"{Name} {CurrentHealth}/{GetMaxHealth()} HP"; 
        }

        private void ExamineEnemy()
        {
            int targetId = GetTarget();
            
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
        public int CompareEquipment(int newId)
        {
            try
            {
                Equipment? newEquip = Assets.GetObjectById<Equipment>(newId);
                foreach (int currentId in _equippedEquipment.Values)
                {
                    Equipment currentEquip = Assets.GetObjectById<Equipment>(currentId);
                    if (currentEquip.GetType() == newEquip.GetType())
                    {
                        Console.Write("Equipped: ");
                        currentEquip.Examine();
                        Console.Write("New: ");
                        newEquip.Examine();
                        return currentId;
                    }
                }
            }
            catch { }

            return -1;
        }
        public void Equip(int id)
        {

        }

        
        public string ChooseEquipType()
        {
            return Game.PlayerChoosesString(_equippedEquipment.Keys.ToArray());
        }
        

        public Hero(string name, int level, int health, int strength, int defence) : base(name, level, health, strength, defence)
        {
            
            Assets.AddHero(Id, this);

            //_actionsAvailable.Add("Attack", AttackEnemy); - Moved to GameCharacter
            //_actionsAvailable.Add("Skill", UseSkill);

            _actionsAvailable.Add("Examine", ExamineEnemy);
            //_actionsAvailable.Add("Item", UseItem);
            
            SetArmor(Assets.noArmor.Id);
            SetWeapon(Assets.noWeapon.Id);
        }
    }
}
