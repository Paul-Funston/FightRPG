using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class GameCharacter : GameObject
    {
       

        protected int _level;
        public int Level { get; set; }

        protected readonly int _baseStrength;
        public int BaseStrength { get { return _baseStrength; } }
        protected int _bonusStrength = 0;

        protected readonly int _baseDefence;
        public int BaseDefence { get { return _baseDefence; } }
        protected int _bonusDefence = 0;

        protected readonly int _baseHealth;
        public int BaseHealth { get { return _baseHealth; } }

        protected int _bonusHealth = 0;
        
        protected int _currentHealth;
        public int CurrentHealth { get { return _currentHealth; } }

        private bool _canAct = true;
        public bool CanAct 
        {
            get { return _canAct; } 
            set { _canAct = value; }
        }

        private Dictionary<int, bool> _abilitiesAvailable = new();

        public virtual int GetEffectiveStrength()
        {
            return _baseStrength + _bonusStrength;
        }

        public virtual int GetEffectiveDefence()
        {
            return _baseDefence + _bonusDefence;
        }
        public int GetMaxHealth()
        {
            return _baseHealth + _bonusHealth;
        }

        public int SetCurrentHealthToMax()
        {
            _currentHealth = GetMaxHealth();
            return _currentHealth;
        }

        public int ChangeHealth(int n)
        {
            int expectedHealth = _currentHealth + n;

            if (expectedHealth > GetMaxHealth())
            {
                expectedHealth = GetMaxHealth();
            } else if (expectedHealth < 0)
            {
                expectedHealth = 0;
            }
            _currentHealth = expectedHealth;


            return _currentHealth;
        }

        public int DealDamage(int n)
        {
            int newHealth = ChangeHealth(n * -1);
            if (newHealth == 0) { CanAct = false; }
            return newHealth;
        }

        
        public virtual string Examine()
        {
            return $" is level {_level}, has {CurrentHealth}/{GetMaxHealth()} Health, {GetEffectiveStrength()} Strength, and {GetEffectiveDefence()} Defence.";
        }

        protected virtual void AttackEnemy()
        {
            int targetId = GetTarget();

            Game.CharacterUseAbility(this, Assets._basicAttackId, targetId);
        }

        protected virtual int GetTarget()
        {
            int targetId = -1;
            while (targetId < 0)
            {
                targetId = Game.GetMonsterTarget();
            }

            return targetId;
        }


        public GameCharacter(string name, int level, int health, int strength, int defence ) : base(name)
        {


            if (level < 0 || strength < 0 || defence < 0 || health < 0)
            {
                throw new Exception("None of a characters starting stats can be negative.");
            } else
            {
                _level = level;
                _baseStrength = strength;
                _baseDefence = defence;
                _baseHealth = health;
            }

            SetCurrentHealthToMax();
            Assets.AddCharacter(Id, this);
            _actionsAvailable.Add("Attack", AttackEnemy);
        }
        
    }
}
