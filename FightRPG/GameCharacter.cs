using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class GameCharacter
    {
        protected int _id;
        public int Id { get { return _id; } }

        protected string _name;
        public string Name { get { return _name; } }

        protected int _level;
        public int Level { get; set; }

        protected int _baseStrength;
        public int BaseStrength { get { return _baseStrength; } }
        protected int _bonusStrength = 0;

        protected int _baseDefence;
        public int BaseDefence { get { return _baseDefence; } }
        protected int _bonusDefence = 0;

        protected int _baseHealth;
        public int BaseHealth { get { return _baseHealth; } }

        protected int _bonusHealth = 0;
        
        private int _currentHealth;
        public int CurrentHealth { get { return _currentHealth; } }

        private bool _canAct = true;
        public bool CanAct 
        {
            get { return _canAct; } 
            set { _canAct = value; }
        }

        public int GetEffectiveStrength()
        {
            return _baseStrength + _bonusStrength;
        }

        public int GetEffectiveDefence()
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

            if (_currentHealth == 0)
            {
                CanAct = false;
            }

            return _currentHealth;
        }

        public string Examine()
        {
            return $"has {CurrentHealth} / {GetMaxHealth()} Health, {GetEffectiveStrength()} Strength, and {GetEffectiveDefence()} Defence.";
        }

        static GameCharacter() => currentID = 0;
        private int GetNextID() => ++currentID;
        private static int currentID;

        public GameCharacter(string name, int level, int health, int strength, int defence )
        {
            _id = GetNextID();

            if (name.Length < 2 || !name.All(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)))
            {
                throw new Exception("A characters name must have at least 2 characters that are all letters.");
            }
            else
            {
                _name = name;
            }

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

        }
        
    }
}
