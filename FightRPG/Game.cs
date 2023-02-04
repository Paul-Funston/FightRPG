using System;
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
        private static Dictionary<Item, int> _inventory = new Dictionary<Item, int>();
        private static HashSet<Item> _availableItems = new();

        private static HashSet<Hero> _party = new();

        private static int _currentGold = 0;
        private static int _currentXp = 0;
        private static Location _currentLocation;
        private static Location? _previousLocation = null;


        public static void GameStart()
        {
            InitializeAssets();
            Hero player = CreatePlayer();
            _party.Add(player);
            OpenTownMenu();
        }
        private static void InitializeAssets()
        {
            _currentGold = 0;
            _currentXp = 0;
            _gameDay = 1;
            _inventory = new();
        }

        private static Hero CreatePlayer()
        {
            Armor startingArmor = new Armor("T-Shirt", 1, 0, 1);
            Weapon startingWeapon = new Weapon("KeyBlade", 0, 1, 1);
            Hero? playerHero = null;
            string heroName = "";
            
            // skip name for testing Remove this line to name player
            playerHero = new Hero("Test", 1, 20, 5, 5, startingArmor, startingWeapon);

            Console.WriteLine("Welcome to the game!");
            Console.WriteLine("Please Name your Hero");

            while (playerHero == null)
            {
                Console.Write("> ");
                string inputName = Console.ReadLine();
                inputName = inputName.Trim();

                if (string.IsNullOrEmpty(inputName)) 
                { 
                    continue; 
                }

                heroName = inputName.Substring(0, 1).ToUpper() + inputName.Substring(1);

                try
                {
                    playerHero = new Hero(heroName, 1, 20, 5, 5, startingArmor, startingWeapon);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine($"Good Luck on your adventure {playerHero.Name}!");
            return playerHero;
        }

    

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

        private static Fight CreateFight(int NumberOfEnemies = 1)
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
                // display the options (8 max or to end of list)
                for (int i = 0; i + rollingIndex < arg.Length && i < 8; i++)
                {
                    Console.WriteLine($"{i + 1}: {arg[i + rollingIndex].Name}");
                }

                Console.WriteLine("9: Previous");
                Console.WriteLine("0: Next");

                int input = -1;
                while (input < 0 || input > 10)
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
                // display the options (8 max or to end of list)
                for (int i = 0; i + rollingIndex < arg.Length && i < 8; i++)
                {
                    Console.WriteLine($"{i + 1}: {arg[i + rollingIndex]}");
                }

                Console.WriteLine("9: Previous");
                Console.WriteLine("0: Next");

                int input = -1;
                while (input < 0 || input > 10)
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

        public static void MoveLocation(Location newLocation, Location prevLocation)
        {
            newLocation.TravelTo(prevLocation);
            _currentLocation = newLocation;
            _currentLocation.ChooseAction();
        }

        
    }
}
