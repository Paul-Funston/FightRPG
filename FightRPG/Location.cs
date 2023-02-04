using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Location : GameObject
    {
        private string _name;
        public string Name { get { return _name; } }
        private Dictionary<string, Action> _actionsAvailable = new();
        public Dictionary<string, Action> GetActionsAvailable { get { return new Dictionary<string, Action>(_actionsAvailable); } }

        private HashSet<Location> _locationsAvailable = new();
        private Location[] _locationsAvailableArray { get { return _locationsAvailable.ToArray(); } }

        private Location? _previousLocation = null;
        public Location? PreviousLocation { get { return _previousLocation; } }
        public int AddConnection(Location l)
        {
            _locationsAvailable.Add(l);
            return _locationsAvailable.Count();
        }

        private string[] ActionsAsStrings()
        {
            return _actionsAvailable.Keys.ToArray();
        }

        public void ChooseAction()
        {
            string choice = Game.PlayerChoosesString(ActionsAsStrings());
            _actionsAvailable[choice].Invoke();
        }

        private void Travel()
        {
            if (_locationsAvailable.Count == 0)
            {
                if (_previousLocation != null)
                {

                    Game.MoveLocation(_previousLocation, this);
                    _previousLocation = null;
                } else
                {
                    throw new Exception("No locations found to travel to.");
                }

            } else if(_locationsAvailable.Count == 1) 
            {
                Game.MoveLocation(_locationsAvailable.First(), this);
            } else
            {
                Location choice = Game.PlayerChooseLocation(_locationsAvailableArray);
                Game.MoveLocation(choice, this);
            }
        }

        public Location TravelTo(Location travelingFrom)
        {
            _previousLocation = travelingFrom;
            return this;
        }

        /*
        public int ShowTravelOptions()
        {
            if (_locationsAvailable.Count > 8) { throw new Exception("Too many options."); }

            for (int i = 1; i <= _locationsAvailable.Count(); i++ )
            {
                Console.WriteLine($"{i}: {_locationsAvailableArray[i - 1].Name}");
            }

            Console.WriteLine($"0: Return to last location");
            return _locationsAvailable.Count();
        }
 
        public Location GetLocationAtOption(int option)
        {
            return _locationsAvailableArray[option - 1];
        }



        public void DoLocationAction()
        {
            // shops purchase items
            // dungeon get into fight
            // 
        }

        public void OpenMenu()
        {
            if (this.Name != "Menu")
            {
                Assets.Menu.TravelHere(this);

            }

        }
        */

        public Location(string name)
        {
            if (name.Length < 2 || !name.All(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)))
            {
                throw new Exception("A location name must have at least 2 characters that are all letters.");
            }
            else
            {
                _name = name;
            }

            _actionsAvailable.Add("Travel", Travel);
        }

        public class Shop : Location
        {
            private HashSet<Item> _inventory = new();
            public Item[] InventoryArray { get { return _inventory.ToArray(); } }
            public int AddItem(Item item)
            {
                _inventory.Add(item);
                return _inventory.Count();
            }






            
            public Shop(string name) : base(name)
            {

            }
        }

        public class Dungeon : Location
        {
            private int _fightsHere = 0;
            private Dictionary<String, int> _inhabitants = new();
            public void AddMonster(String monsterName, int howMany)
            {
                _inhabitants.Add(monsterName, howMany);
            }
            public Dungeon(string name) : base(name)
            {

            }
        }

    }
}
