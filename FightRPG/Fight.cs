using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Fight
    {
        private HashSet<Hero> _activeParty;
        private HashSet<Monster> _enemies;
        public HashSet<Monster> Enemies { get { return _enemies.ToHashSet(); } }
        private bool _isActive = true;
        public bool IsActive { get { return _isActive; } }

        private bool _wasFightWon = false;
        private int _turnCount = 0;

        private int _goldReward;
        public int GoldReward { get { return _goldReward; } }

        private int _xpReward;
        public int XpReward { get { return _xpReward; } }


        
        private void CalculateRewards()
        {
            foreach (Monster enemy in _enemies)
            {
                _goldReward += enemy.GoldPrize;
                _xpReward += enemy.XpPrize;
                
            }
        }

        public bool EnterFight()
        {
            FightIntro();

            while(_isActive)
            {
                Console.WriteLine($"Round: {++_turnCount}");
                PlayerTurn();
                MonsterTurn();
                EndTurn();

            }


            return _wasFightWon;
        }

        public void FightMenu(Hero active)
        {

        }



        public void FightIntro()
        {
            Console.WriteLine($"Danger!");
            foreach (Monster enemy in _enemies)
            {
                Console.WriteLine($"A {enemy.Nickname} {enemy.Name} attacked!");
            }
        }


        public void PlayerTurn()
        {
            foreach (Hero hero in _activeParty)
            {

                if (hero.CanAct)
                {
                    FightMenu(hero);
                }
            }

        }

        public void MonsterTurn()
        {
            foreach (Monster monster in _enemies)
            {
                if(monster.CanAct && IsTeamAlive(_activeParty))
                {
                   HashSet<Hero> targets = monster.FindTargets(_activeParty);

                    int n = new Random().Next(Assets.Adjectives.Length);
                    string adverb = Assets.Adverbs[n];
                    Console.Write($"The {monster.Nickname} {monster.Name} attacks {Assets.GetRandomAdverb}");
                    CharacterAttacks(monster, targets);
                }
            }

        }

        public bool IsTeamAlive(HashSet<Hero> team)
        {
            bool isTeamAlive = false;

            foreach(Hero character in team)
            {
                if (character.CurrentHealth > 0)
                {
                    isTeamAlive = true;
                    break;
                }
            }

            return isTeamAlive;
        }

        public void EndTurn()
        {

        }

        public void CharacterAttack(GameCharacter attacker, GameCharacter target)
        {
            int damage = attacker.GetEffectiveStrength() - target.GetEffectiveDefence();

            // always deal at least 1 damage
            if (damage < 1) { damage = 1; }

            int targetsHealth = target.ChangeHealth(damage * -1);
            Console.WriteLine($"dealt {damage} to {target.Name}.");
        }
        public void CharacterAttacks(GameCharacter attacker, HashSet<Hero> targets)
        {
            foreach (Hero target in targets)
            {
                CharacterAttack(attacker, target);
            }
        }
        public void CharacterAttacks(GameCharacter attacker, HashSet<Monster> targets)
        {
            foreach (Monster target in targets)
            {
                CharacterAttack(attacker, target);
            }
        }

        public Fight(HashSet<Hero> party, HashSet<Monster> enemies)
        {
            if (party.Count > 0 && enemies.Count > 0)
            {
                _activeParty = party;
                _enemies = enemies;
            } else
            {
                throw new Exception("Cannot start a fight without characters on both sides.");
            }



        }
    }
}
