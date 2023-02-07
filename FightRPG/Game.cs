using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using FightRPG;

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
        private static Dictionary<string, Action> _menuActions = new Dictionary<string, Action>()
        {
            {"Items", OpenInventory},
            {"Party", PartyEquipment },
            {"View Stats", DisplayStats },
            {"Close", CloseMenu },

    };


        private static string? _heroName = null;
        public static int GameDay { get { return _gameDay; } }
        private static bool _firstLoad = true;
        private static Dictionary<int, int> _inventory = new Dictionary<int, int>();
        private static HashSet<Hero> _party = new(); // Convert to Ids
        
        private static int _currentGold = 0;
        private static int _currentXp = 0;
        private static Location _currentLocation;
            
        private static Dictionary<string, int> _statistics = new Dictionary<string, int>()
        {
            {"Days This Life", _gameDay },
            {"Battles Won This Life", _battlesWon },
            {"Times Defeated", _timesDefeated },
            {"Total Days Played", _gameDay + _daysPrevious },
            {"Total Battles Won", _totalWins },
            
        };

        


        public static void GameStart()
        {
            
            if (_firstLoad)
            {
                _firstLoad = false;
                InitializeAssets();
            }

            ResetVariables();
            CreatePlayer();
            
            
            SetToStartingLocation();
            TestMethod();

            Console.WriteLine($"Good Luck on your adventure {_playerHero.Name}!");

            PlayGame();
        }
        private static void InitializeAssets()
        {
            
            try
            {
                Assets.LoadItems();
                _firstLoad = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Welcome to the game!");
            GetHeroName();
            
            
        }

        private static void ResetVariables()
        {
            _party = new();
            _currentGold = 0;
            _currentXp = 0;
            _gameDay = 1;
            _inventory = new();
            _inventory.Add(Assets.umbrella.Id, 1);
            _inventory.Add(Assets.TShirt.Id, 1);

        }
        private static void GetHeroName()
        {
            _heroName = null;
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
            

            while (_playerHero == null)
            {
                try
                {
                    _playerHero = new Hero(_heroName, 1, 20, 5, 5);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (_playerHero == null) { GetHeroName(); }
            }

            _party.Add(_playerHero);
            
            
        }

        public static void SetToStartingLocation()
        {
                        
            _currentLocation = Assets.Town;

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
            //Status();
            ClearScreen(2);
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

       /*  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *
        *                                                              *
        *   Combat                                                     *
        *                                                              *
        *  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   */
        

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
                    return PlayerChoosesObjectByName(options);
                    //return GetPlayerChoice(options);
                    // return PlayerChooseString();
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

        public static void ScanEnemy(int id)
        {
            Monster? obj = Assets.GetObjectById<Monster>(id);
            if (obj != null)
            {
                obj.Examine();
            }

        }

        // Fight Over

        public static void FightWon(Fight fight)
        {
            Location? previousLocation = Assets.GetObjectById<Location>(fight.PreviousLocationId);
            if (previousLocation != null)
            {
                _currentLocation = previousLocation;
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
            if (GetPlayerConfirmation())
            {
                GameStart();
                // handle Stat Trackin and reincarnation
            }
            else
            {
                Console.WriteLine("Game Over");

            }
           
        }

        private static void GetRewards(Fight fight)
        {
             _currentGold += fight.GoldReward;
             _currentXp += fight.XpReward;
        }


        /*  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *
         *                                                              *
         * Items and Inventory                                          *
         *                                                              *
         *  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   */
        
        public static void OpenMenu() // Open Menu? Inventory, Stats, Party
        {

            string choice = PlayerChoosesString(_menuActions.Keys.ToArray());
            _menuActions[choice].Invoke();
            
        }
        
       
        public static void ShowPartyAndEquipment()
        {
            foreach (Hero hero in _party)
            {
                hero.Examine();
                hero.DisplayEquipment();
            }
        }
        public static void PartyEquipment()
        {
            ShowPartyAndEquipment();
            Hero hero = ChoosePartyMember();
            string equipType = hero.ChooseEquipType();
            int newEquipId = SelectInventoryItemByType(equipType);
            if (newEquipId < 1) { return; }

            Equipment equip = Assets.GetObjectById<Equipment>(newEquipId);
             if (equip == null)
            {
                Console.WriteLine("Item not Found.");
                return; 
            } else
            {
                equip.Examine();
            }

            hero.CompareEquipment(newEquipId);
            Console.WriteLine("Equip?");
            if (GetPlayerConfirmation())
            {
                int currentEquipId = hero.Equip(newEquipId);
                InventoryAddItem(currentEquipId);
                InventoryTakeItem(newEquipId);
            }

            OpenMenu();
        }
        public static void OpenInventory()
        {
            int itemId = SelectInventoryItem();
            if (itemId < 0) { return; }

            Item? item = Assets.GetObjectById<Item>(itemId);
            if (item == null) { return; }

            item.Examine();
            Console.WriteLine($"Use {item.Name}?");
            if (GetPlayerConfirmation())
            {
                item.ChooseAction();
            }

            OpenMenu();

        }

        public static void TryEquipEquipment(int id)
        {
            ShowPartyAndEquipment();
            Hero hero = ChoosePartyMember();
            hero.CompareEquipment(id);
            Console.WriteLine("Equip?");
            if (GetPlayerConfirmation())
            {
                int currentEquipId = hero.Equip(id);
                InventoryAddItem(currentEquipId);
                InventoryTakeItem(id);
            }
        }

        private static void InventoryAddItem(int id)
        {
            if (_inventory.Keys.Contains(id))
            {
                _inventory[id]++;
            }
            else
            {
                _inventory[id] = 1;
            }

        }
        private static void InventoryTakeItem(int id)
        {
            if (_inventory[id] > 1)
            {
                _inventory[id]--;
            }
            else
            {
                _inventory.Remove(id);
            }
        }

        private static void CleanInventory()
        {
            foreach(KeyValuePair<int,int> pair in _inventory)
            {
                if(pair.Value < 1)
                {
                    _inventory.Remove(pair.Key);
                }
            }
        }

        public static Hero ChoosePartyMember()
        {
            if(_party.Count == 1)
            {
                return _party.First();
            } else
            {
                int heroId = PlayerChoosesObjectByName(GetActiveParty());
                return Assets.GetObjectById<Hero>(heroId);
            }
        }
        public static void CloseMenu()
        {
            // just moves along
        }
        
        public static int SelectInventoryItemByType(string type)
        {
            switch (type)
            {
                case "Weapon":
                    return SelectInventoryItemOfType<Weapon>();
                    
                case "Armor":
                    return SelectInventoryItemOfType<Armor>();
                
                default:
                    return SelectInventoryItemOfType<Weapon>();

                    // can add consumable here. would need its own new method.
            }

        }

        
        public static int SelectInventoryItemOfType<T>() where T : Equipment
        {
            Dictionary<string, int> itemChoices = new(); 
            foreach(int id in _inventory.Values)
            {
                try
                {
                    Equipment? item = Assets.GetObjectById<Equipment>(id);
                    if(item.GetType() == typeof(T))
                    {
                        string s = $"{item.Name} Str: {item.Strength} Def: {item.Defence}";
                        itemChoices[s] = id;
                    }
                } catch(Exception ex) { Console.WriteLine(ex.Message); }
            }
            
            if (itemChoices.Count > 0) 
            {
                string choice = PlayerChoosesString(itemChoices.Keys.ToArray());
                int choiceId = itemChoices[choice];
                return choiceId;
            }
            

            return -1;
        }

        public static int SelectInventoryItem()
        {
            Dictionary<string, int> inventoryItemsAndQuantities = new();
            foreach (int id in _inventory.Keys)
            {
                Item? item = Assets.GetObjectById<Item>(id);
                if (item != null)
                {
                    string itemNameAndQuantity = $"{item.Name}  {_inventory[id]}";
                    inventoryItemsAndQuantities.Add(itemNameAndQuantity, id);
                }
            }

            inventoryItemsAndQuantities.Add("Cancel", 0);

            string choice = PlayerChoosesString(inventoryItemsAndQuantities.Keys.ToArray());

            if (choice == "Cancel") { return -1; }

            return inventoryItemsAndQuantities[choice];
        }

        /*
        public static void UseItem() 
        {
            //_inventory
            Dictionary<string, int> inventoryItemsAndQuantities = new();
            foreach (int id in _inventory.Keys)
            {
                Item? item = Assets.GetObjectById<Item>(id);
                if (item != null)
                {
                    string itemNameAndQuantity = $"{item.Name}  {_inventory[id]}";
                    inventoryItemsAndQuantities.Add(itemNameAndQuantity, id);
                }
            }

            inventoryItemsAndQuantities.Add("Cancel", 0);
            
            string choice = PlayerChoosesString(inventoryItemsAndQuantities.Keys.ToArray());
            
            if (choice == "Cancel") { return; }

            
            int itemId = inventoryItemsAndQuantities[choice];
            Item? selectedItem = Assets.GetObjectById<Item>(itemId);
           
            if (selectedItem != null)
            {
                Console.WriteLine($"Use {selectedItem.Name}?");
                if(GetPlayerConfirmation())
                {
                    selectedItem.ChooseAction();
                    
                }
            }
        }
       */
        /*
        public static void TryEquipEquipment(int id)
        {
            Equipment? item = Assets.GetObjectById<Equipment>(id);

            int[] partyIds = GetActiveParty();
            Hero heroToEquip = _party.First();

            
            if (_party.Count > 1)
            {
                Console.WriteLine("Equip which character?");
                int heroIdToEquip = PlayerChoosesObjectByName(GetActiveParty());
                heroToEquip = Assets.GetObjectById<Hero>(heroIdToEquip);
            }


            if(heroToEquip.CompareEquip(id))
            {
                heroToEquip.EquipItem(id);
            }
            
            
            
            try
            {
                if (item.GetType() == typeof(Weapon))
                {
                    heroToEquip.SetWeapon(id);
                }
                else if (item.GetType() == typeof(Armor))
                {
                    heroToEquip.SetArmor(id);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assets.GetObjectById<item.GetType()>(id);
        }
        */

        /*
        public static bool CompareEquip(int id)
        {
            Equipment? newItem = Assets.GetObjectById<Equipment>(id);
            Equipment? equipped = null;
            foreach (KeyValuePair<string, int> pair in _equippedEquipment)
            {
                Equipment? checkedEquipment = Assets.GetObjectById<Equipment>(pair.Value);
                if (newItem.GetType() == checkedEquipment.GetType())
                {
                    equipped = checkedEquipment;
                    break;

                }
            }

            if (equipped != null)
            {
                Console.WriteLine($"Replace {equipped.Name} with {newItem.Name}?");
                Console.WriteLine();
                Console.WriteLine($"Strength {equipped.Strength} > {newItem.Strength}");
                Console.WriteLine($"Defence {equipped.Defence} > {newItem.Defence}");
                Console.WriteLine();
                return Game.GetPlayerConfirmation();
            }
            else
            {
                return false;
            }

        }
        */
        /*  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *
         *                                                              *
         * Controls and Player Input                                    *
         *                                                              *
         *  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   */

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
            ClearScreen();
            Status();
            return input;
        }

        public static bool GetPlayerConfirmation()
        {
            bool hasPlayerConfirmed = false;
            string[] yesOrNo = new string[2] { "Yes", "No" };
            string answer = PlayerChoosesString(yesOrNo);
            if (answer == "Yes")
            {
                hasPlayerConfirmed = true;
            }
            return hasPlayerConfirmed;
        }
        public static Location PlayerChooseLocation(Location[] arg)
        {
            Dictionary<string, Location> locationNames = new();

            foreach(Location location in arg)
            {
                locationNames.Add(location.Name, location);
            }

            string[] locationNamesArray = locationNames.Keys.ToArray();
            string ChosenName = PlayerChoosesString(locationNamesArray);

            Location choice = locationNames[ChosenName];
            return choice;
            /*
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
            */


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
        /*
        public static int GetPlayerChoice(int[] arg)
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
                    GameObject? obj = Assets.GetObjectById(arg[i + rollingIndex]);
                    try
                    {
                        Console.Write($"{i + 1}: ");
                        if (obj != null) 
                        { 
                            Console.WriteLine(FormatObjectName(obj));
                        } else
                        {
                            Console.WriteLine("Target not found");
                        }
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
        */

        public static int PlayerChoosesObjectByName(int[] objIds)
        {
            Dictionary<string, int> objIdsByName = new();

            foreach(int id in objIds)
            {
                GameObject? obj = Assets.GetObjectById(id);
                if (obj != null)
                {
                    objIdsByName.Add(obj.Name, id);
                }
            }

            string choice = PlayerChoosesString(objIdsByName.Keys.ToArray());
            return objIdsByName[choice];
        }

        

        public static void MoveLocation(int newLocationId)
        {
            Location? newLocation = Assets.GetObjectById<Location>(newLocationId);
            if (newLocation != null)
            {
                _currentLocation = newLocation;
                ClearScreen();
                Status();
            } else
            {
                SetToStartingLocation();
            }
        }

       /*  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *
        *                                                              *
        *   Information and Utility Methods                            *
        *                                                              *
        *  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   */

        // 

        public static void ReturnToTown()
        {
            
            //Status();
            SetToStartingLocation();
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

            Console.Write($" Day {_gameDay} ");

            Console.Write($"Gold: {_currentGold} ");
            Console.Write($"Xp {_currentXp} ");

            // If Dungeon Or Fight Display Health of Party
            try
            {
                if ((Location.Dungeon)_currentLocation != null || (Fight)_currentLocation != null)
                {
                    Console.WriteLine();
                    foreach (Hero hero in _party)
                    {
                        Console.Write($"{hero.GetStatus()} ");
                    }
                    Console.WriteLine();
                }
            } catch (Exception) {  }

            Console.WriteLine();
    
        }
        public static void DisplayStats()
        {
            foreach(KeyValuePair<string, int> pair in _statistics)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.WriteLine();

            OpenMenu();
        }

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


        private static void TestMethod()
        {
            
            

        }

       
    }
}
