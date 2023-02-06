using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public static class Game
    {
        private static int _gameDay = 1;
        private static int _daysPrevious = 0;
        private static int _battlesWon = 0;
        private static int _totalWins = 0;
        private static int _timesDefeated = 0;
        private static Hero? _playerHero = null;
        public static GameObject? _obj = _playerHero;

        private static string? _heroName = null;
        public static int GameDay { get { return _gameDay; } }
        private static bool _firstLoad = true;
        private static Dictionary<Item, int> _inventory = new Dictionary<Item, int>();
        private static HashSet<Hero> _party = new();
        
        private static int _currentGold = 0;
        private static int _currentXp = 0;
        private static Location _currentLocation;
        private static Location? _previousLocation = null;
        private static Dictionary<string, int> _statistics = new Dictionary<string, int>()
        {
            {"Days This Life", _gameDay },
            {"Battles Won This Life", _battlesWon },
            {"Times Defeated", _timesDefeated },
            {"Total Days Played", _gameDay + _daysPrevious },
            {"Total Battles Won", _totalWins },
            
        };

        public static int[] GetActiveParty()
        {
            int[] heroIds = new int[_party.Count];
            Hero[] heroArray = _party.ToArray();
            for (int i = 0; i < heroIds.Length; i++)
            {
                heroIds[i] = heroArray[i].Id;
            }

            return heroIds;
        }


        public static void GameStart()
        {
            InitializeAssets();
            
            
            //OpenTownMenu();
            TestMethod();

            Console.WriteLine($"Good Luck on your adventure {_playerHero.Name}!");

            PlayGame();
        }
        private static void InitializeAssets()
        {
            _party = new();
            if (_firstLoad)
            {
                GetHeroName();
                try
                {
                    Assets.LoadItems();
                    _firstLoad = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            CreatePlayer();


            _currentGold = 0;
            _currentXp = 0;
            _gameDay = 1;
            _inventory = new();

            
            SetToStartingLocation();
        }
        private static void GetHeroName()
        {
            _heroName = null;
            Console.WriteLine("Welcome to the game!");
            while(_heroName == null)
            {
                Console.WriteLine("Please Name your Hero");

                Console.Write("> ");
                string? inputName = Console.ReadLine();
                inputName = inputName.Trim();

                if (string.IsNullOrEmpty(inputName))
                {
                    continue;
                }

                _heroName = inputName.Substring(0, 1).ToUpper() + inputName.Substring(1);

            }

        }
        private static void CreatePlayer()
        {
            Armor startingArmor = new Armor("TShirt", 1, 0, 1);
            Weapon startingWeapon = new Weapon("KeyBlade", 1, 2, 0);
            
            Console.WriteLine("Welcome to the game!");
            Console.WriteLine("Please Name your Hero");

            while (_playerHero == null)
            {
                try
                {
                    _playerHero = new Hero(_heroName, 1, 20, 5, 5, startingArmor, startingWeapon);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (_playerHero == null) { GetHeroName(); }
            }

            _party.Add(_playerHero);
            _obj = _playerHero;
            
        }

        private static void SetToStartingLocation()
        {
            
            Location? location = null;
            location = Assets.GetObjectById<Location>(Assets._startingTownId);
            

            if (location == null)
            {
                Console.WriteLine("Could not Find Starting Town.");
            } else
            {
                _currentLocation = location;
            }
        }

        private static void PlayGame()
        {
            try
            {
                _currentLocation.ChooseAction();

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Maybe set CurrentLocation to a default?
            }

            //ClearScreen();
            Status();
            ClearScreen(4);
            PlayGame();
        }

    
        /*
        private static void OpenMenu()
        {

            // check location / state and open appropriate menu
            // OpenTownMenu() OpenDungeonMenu() OpenFightMenu()
            OpenTownMenu();
        }
        private static void OpenTownMenu()
        {
            Console.WriteLine("You are in: TOWN");
            Console.WriteLine("What will you do next?");

            Console.Write("1: Go on a Quest");
            Console.Write("              ");
            Console.WriteLine("3: Purchase Items");
            Console.Write("2: Display Inventory");
            Console.Write("          ");
            Console.WriteLine("4: Level up");
            Console.WriteLine();
            Console.WriteLine("0: Display Stats.");

            Console.WriteLine("Press a key to decide:");
            int option = -1;

            while (option < 0 || option > 4)
            {
                option = GetInput();
            }

            Console.WriteLine(option);

            switch (option)
            {
               case 1:
                    Console.WriteLine("Quest Selected.");
                    SelectQuest();
                    break;

                case 2:
                    Console.WriteLine("Inventory Selected.");

                    SelectInventory();

                    break;

                case 3:
                    Console.WriteLine("Shop Selected.");
                    SelectShop();
                    break;

                case 4:
                    Console.WriteLine("Level up Selected.");
                    SelectLevel();
                    break;

                case 0:
                    Console.WriteLine("Stats Selected.");
                    SelectStats();
                    break;

 
            }
        }

        private static void SelectQuest()
        {
            Console.WriteLine("Coming Soon");
            StartFight(CreateFight());
            OpenMenu();
        }

        private static void SelectInventory()
        {
            Console.WriteLine("Coming Soon");
            // if currentLocation = town open town menu, if currentLocation = dungeon openDungeonMenu
            OpenMenu();
        }   

        private static void SelectShop()
        {
            Console.WriteLine("Coming Soon");
            OpenMenu();
        }

        private static void SelectLevel()
        {
            Console.WriteLine("Coming Soon");
            OpenMenu();
        }

        private static void SelectStats()
        {
            Console.WriteLine("Coming Soon");

            // if currentLocation = town open town menu, if currentLocation = dungeon openDungeonMenu
            OpenMenu();
        }

        private static Fight CreateFight(int NumberOfEnemies = 2)
        {
            HashSet<Monster> enemies = new HashSet<Monster>();
            
            for(int i = 1; i <= NumberOfEnemies; i++)
            {
                int randomMonster = new Random().Next(Assets.BestiaryCount);
                Monster newMonster = Assets.GetMonster(randomMonster, _gameDay);
                enemies.Add(newMonster);
            }

            try
            {
               Fight newFight = new Fight(_party, enemies);
                return newFight;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Assets.debugFight;

        }

        private static void StartFight(Fight fight)
        {

            bool fightWon = fight.EnterFight();



            if (fightWon)
            {
                _currentGold += fight.GoldReward;
                _currentXp +=fight.XpReward;
            } else
            {

            }
            _gameDay++;
        }
        */
        // Handling Combat

        public static void CharacterUseAbility(GameCharacter attacker, int abilityId, int defenderId)
        {
            int attackerStrength = attacker.GetEffectiveStrength();

            Ability? ability = Assets.GetObjectById<Ability>(abilityId);
            if (ability == null)
            {
                ability = new Ability("Attack");
            }

            attackerStrength += ability.Value;

            try
            {
                GameCharacter defender = Assets.GetObjectById<GameCharacter>(defenderId);
                int defenderDefence = defender.GetEffectiveDefence();
                int damage = attackerStrength - defenderDefence;
                if (damage < 1) { damage = 1; } // minimum 1 damage every attack
                defender.DealDamage(damage);
                SendDamageMessage(attacker, defender, damage) ;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void SendDamageMessage(GameCharacter attacker, GameCharacter defender, int damage)
        {
            Console.Write($"{attacker.Name} attacks {defender.Name} {Assets.GetRandomAdverb()}.");
            Console.WriteLine($" {defender.Name} takes {damage} damage.");
            int currentHealth = defender.CurrentHealth;
            int maxHealth = defender.GetMaxHealth();

            if (currentHealth == 0) 
            {
                Console.WriteLine($"{defender.Name} is knocked out.");
            } else if (currentHealth < maxHealth / 4 )
            {
                Console.WriteLine($"{defender.Name} can't take much more.");
            } else if (currentHealth < maxHealth / 2)
            {
                Console.WriteLine($"{defender.Name} is really slowing down.");
            }
            
        }

        public static int PlayerSelectTarget()
        {
            try
            {
                int[] options = ((Fight)_currentLocation).MonstersWithHealthIds;
                
                if (options.Length == 1) 
                { return options[0]; } else
                {
                    return GetPlayerChoice(options);

                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        public static int GetMonsterTarget()
        {
            int choice = -1;
            try
            {
                
                int[] options = ((Fight)_currentLocation).HeroesWithHealthIds;
                choice = options[0];
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (choice < 0)
            {
                choice = new Assets.Goblin(1).Id;
                Console.WriteLine("Where did that go?");

            }

            return choice;
        }

        // Fight Over

        public static void FightWon(Fight fight)
        {
            if (fight.PreviousLocation != null)
            {
                _currentLocation = fight.PreviousLocation;
            } else
            {
                SetToStartingLocation();
            }

            _battlesWon++;
            _totalWins++;
            GetRewards(fight);
        }

        public static void FightLost()
        {
            HealParty();
            _daysPrevious += _gameDay;
            _timesDefeated++;

            Console.WriteLine("You have been defeated...");
            Console.WriteLine("but is it the end?");
            Console.WriteLine("Will you try again? Maybe it will be easier.");
            // handle Stat Trackin and reincarnation
            GameStart();
        }

        private static void GetRewards(Fight fight)
        {
            // _currentGold += fight.GoldReward;
            // _currentXp += fight.XpReward;
        }
        // Controls

        public static int GetInput()
        {
            int input = -1;
            while (input < 0 || input > 10)
            {
                Console.Write("> ");
                char test = Console.ReadKey().KeyChar;
                Console.Write(" ");
                input = (int)Char.GetNumericValue(test);

            }

            return input;
        }

        public static Location PlayerChooseLocation(Location[] arg)
        {
            bool hasSelected = false;
            int optionIndex = -1;
            int rollingIndex = 0;
            int numOfPages = arg.Length / 8 + 1;
            while (!hasSelected)
            {
                if (numOfPages > 1)
                {
                    Console.WriteLine("9: Previous");
                    Console.WriteLine("0: Next");
                    Console.WriteLine();
                }
                // display the options (8 max or to end of list)
                for (int i = 0; i + rollingIndex < arg.Length && i < 8; i++)
                {
                    Console.WriteLine($"{i + 1}: {arg[i + rollingIndex].Name}");
                }

                int maxInput = 8;
                if (arg.Length - rollingIndex < 8) { maxInput = arg.Length - rollingIndex; }

                int input = -1;
                while (input < 0 || (input > maxInput && input != 9))
                {
                    input = GetInput();
                }
                Console.WriteLine();

                if (input == 0)
                {
                    if (rollingIndex + 8 < arg.Length)
                    {
                        rollingIndex += 8;
                    }
                }
                else if (input == 9)
                {
                    if (rollingIndex - 8 >= 0)
                    {
                        rollingIndex -= 8;
                    }
                }
                else
                {
                    hasSelected = true;
                    optionIndex = input + rollingIndex - 1;
                }
            }
            Location choice = arg[optionIndex];
            return choice;
        }

        public static string PlayerChoosesString(string[] arg)
        {
            bool hasSelected = false;
            int optionIndex = -1;
            int rollingIndex = 0;
            int numOfPages = arg.Length / 8 + 1;
            while (!hasSelected)
            {

                if (numOfPages > 1)
                {
                    Console.WriteLine("9: Previous");
                    Console.WriteLine("0: Next");
                    Console.WriteLine();
                }

                for (int i = 0; i + rollingIndex < arg.Length && i < 8; i++)
                {
                    Console.WriteLine($"{i + 1}: {arg[i + rollingIndex]}");
                }

                int maxInput = 8;
                if (arg.Length - rollingIndex < 8) { maxInput = arg.Length - rollingIndex; }

                int input = -1;
                while (input < 0 || (input > maxInput && input != 9) )
                {
                    input = GetInput();
                }
                Console.WriteLine();

                if (input == 0)
                {
                    if (rollingIndex + 8 < arg.Length)
                    {
                        rollingIndex += 8;
                    }
                }
                else if (input == 9)
                {
                    if (rollingIndex - 8 >= 0)
                    {
                        rollingIndex -= 8;
                    }
                }
                else
                {
                    hasSelected = true;
                    optionIndex = input + rollingIndex - 1;
                }
            }

            string choice = arg[optionIndex];
            return choice;

        }
        public static int GetPlayerChoice(int[] arg )
        {
            bool hasSelected = false;
            int optionIndex = -1;
            int rollingIndex = 0;
            int numOfPages = arg.Length / 8 + 1;
            
            while (!hasSelected)
            {
                if (numOfPages > 1)
                {
                    Console.WriteLine("9: Previous");
                    Console.WriteLine("0: Next");
                    Console.WriteLine();
                }
                // display the options (8 max or to end of list)
                for (int i = 0; i + rollingIndex < arg.Length && i < 8; i++)
                {
                    GameObject obj = Assets.GetObjectById(arg[i + rollingIndex]);
                    try
                    {
                       
                        Console.Write($"{i + 1}: "); //ex Template "{obj.Name}" "{obj.Name} {(cast)obj.Value}"
                        Console.WriteLine(Format(obj));
                    } catch (Exception ex)
                    {
                        Console.WriteLine($"{i + 1}: Failed to Load - {ex.Message}");
                    }
                }

                int maxInput = 8;
                if (arg.Length - rollingIndex < 8) { maxInput = arg.Length - rollingIndex; }

                int input = -1;
                while (input < 0 || (input > maxInput && input != 9))
                {
                    input = GetInput();
                }
                Console.WriteLine();

                if (input == 0)
                {
                    if (rollingIndex + 8 < arg.Length)
                    {
                        rollingIndex += 8;
                    }
                }
                else if (input == 9)
                {
                    if (rollingIndex - 8 >= 0)
                    {
                        rollingIndex -= 8;
                    }
                }
                else
                {
                    hasSelected = true;
                    optionIndex = input + rollingIndex - 1;
                }
            }
            int choice = arg[optionIndex];
            return choice;

        }

        public static string Format(GameObject obj)
        {
            return $"{obj.Name}";
        }


        public static void MoveLocation(Location newLocation, Location prevLocation)
        {
            newLocation.TravelTo(prevLocation);
            _currentLocation = newLocation;
            //_currentLocation.ChooseAction();
        }

        // Information and Utility Methods

        public static void ReturnToTown()
        {
            HealParty();
            _gameDay++;
        }

        public static void HealParty()
        {
            foreach(Hero hero in _party)
            {
                hero.SetCurrentHealthToMax();
                hero.CanAct = true;
            }
        }

        public static void ClearScreen(int n = 100)
        {
            int position = Console.GetCursorPosition().Top;



            if(n > position) { n = position; }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
            }
        }

        public static void Status()
        {
            Console.Write(_currentLocation.Name);

            Console.Write($" Day {_gameDay}");

  
        }
        public static void DisplayStats()
        {
            foreach(KeyValuePair<string, int> pair in _statistics)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }

        public static void OpenInventory()
        {

        }

        private static void TestMethod()
        {
            //Fight? obj = Assets.GetObjectById<Fight>(5);
            //Console.WriteLine($"Testing Generic Method {obj}");
            string colour = "black";
            string Format = $"{colour}";
            string output = $"{Format}";
            Console.WriteLine(output);

        }
        
    }
}
