using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Fight : Location
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

        public void FightMenu(Hero activeHero)
        {

            Console.WriteLine("You are in: BATTLE");
            Console.WriteLine("What will you do next?");

            Console.WriteLine("1: Attack");
            Console.WriteLine("2: Examine");
            Console.WriteLine("3: Defend");
            Console.WriteLine("4: Use Item");
            Console.WriteLine("5: Check stats");
            Console.WriteLine("0: Attempt to Run");

            Console.WriteLine("Press a key to decide");
            int option = -1;
            bool keepTurn = false;

            while (option < 0 || option > 5)
            {
                option = Game.GetInput();
            }
            Console.WriteLine();

            switch(option)
            {
                case 1:
                    Monster target = SelectTarget();
                    Console.WriteLine($"{activeHero.Name} attacks {Assets.GetRandomAdverb()}");
                    CharacterAttack(activeHero, target);
                    break;

                case 2:
                    target = SelectTarget();
                    Console.WriteLine(target.Examine());
                    break;
                
                case 3:
                    Console.WriteLine($"{activeHero.Name} defends. ");
                    keepTurn = true;
                    break;

                case 4:
                    Console.WriteLine($"{activeHero.Name} looks into their bag and realizes it's not the time for that");
                    keepTurn = true;
                    break;
                
                case 5:
                    foreach(Hero hero in _activeParty)
                    {
                        Console.WriteLine(hero.Examine());
                    }
                    keepTurn = true;
                    break;
                
                default:
                    keepTurn = true;
                    break;
                    
            }

            if(keepTurn)
            {
                FightMenu(activeHero);
            }
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

                    // monster.TakeTurn(Fight);
                   HashSet<Hero> targets = monster.FindTargets(_activeParty);

                    Console.Write($"The {monster.Nickname} {monster.Name} attacks {Assets.GetRandomAdverb()}");
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
            bool isTeamAlive = IsTeamAlive(_activeParty);
            bool enemiesAlive = false;

            foreach(Monster monster in _enemies)
            {
                if (monster.CurrentHealth > 0)
                {
                    enemiesAlive = true;
                    break;
                }
            }

            // Is battle over
            if (!isTeamAlive || !enemiesAlive) { _isActive = false; }

            if(!_isActive && isTeamAlive) 
            {
                _wasFightWon = true;
            }
        }

        public void CharacterAttack(GameCharacter attacker, GameCharacter target)
        {
            int damage = attacker.GetEffectiveStrength() - target.GetEffectiveDefence();

            // always deal at least 1 damage
            if (damage < 1) { damage = 1; }

            int targetsHealth = target.ChangeHealth(damage * -1);
            int targetMaxHealth = target.GetMaxHealth();
            Console.WriteLine($" and deals {damage} to {target.Name}.");

            if (targetsHealth == 0)
            {
                Console.WriteLine($"{target.Name} is knocked out.");
            } else if (targetsHealth < targetMaxHealth / 4)
            {
                Console.WriteLine($"{target.Name} can't take much more. ");
            } else if (targetsHealth < targetMaxHealth / 2)
            {
                Console.WriteLine($"{target.Name} is slowing down after that.");
            }

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
        public Monster SelectTarget()
        {
            foreach (Monster monster in _enemies)
            {
                Console.WriteLine($"Target the {monster.Nickname} {monster.Name}?");
                Console.WriteLine("1: Yes  2: No");

                int input = -1;
                while (input < 1 || input > 2)
                {
                    input = Game.GetInput();
                }

                if (input == 1)
                {
                    return monster;
                }

                continue;
            }

            return SelectTarget();
        }

        public Fight(HashSet<Hero> party, HashSet<Monster> enemies) : base("Fight")
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
