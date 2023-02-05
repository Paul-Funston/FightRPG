using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class GameObject
    {
        private int _id;
        public int Id { get { return _id; } }

        protected string _name;
        public string Name { get { return _name; } }

        protected Dictionary<string, Action> _actionsAvailable = new();
        //public Dictionary<string, Action> GetActionsAvailable { get { return new Dictionary<string, Action>(_actionsAvailable); } }
           
        protected string[] ActionsAsStrings()
        {
            return _actionsAvailable.Keys.ToArray();
        }
        public void ChooseAction()
        {
            if (_actionsAvailable.Count == 0) 
            {
                throw new Exception($"No actions found for {Name}.");
            } else if (_actionsAvailable.Count == 1)
            {
                string onlyOption = _actionsAvailable.Keys.First();
                _actionsAvailable[onlyOption].Invoke();
            } else
            {
                string choice = Game.PlayerChoosesString(ActionsAsStrings());
                _actionsAvailable[choice].Invoke();
            }
        }

        private int GetNextID()
        {
            return ++currentID;
        } 
        private static int currentID = 0;

        public GameObject(string name) 
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

            Assets.AddObject(_id, this);
            Console.WriteLine($"Created Object {_name} Id: {_id}");

        }

    }
}
