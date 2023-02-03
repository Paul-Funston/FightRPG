using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Item
    {
        protected int _id;
        public int Id { get { return _id; } }
        protected string _name; 
        public string Name { get; }
        //private string _description;
        //public string Description { get { return _description; } }
        protected int _value;
        public int Value { get { return _value; } }

        
        static Item() => currentID = 0;
        protected int GetNextID() => ++currentID;
        private static int currentID;
        internal static int x;
        // constructors
        public Item() { }
        public Item(string name, int value)
        {
            _id = GetNextID();
            _name = name;
            _value = value;

        }



    }
}
