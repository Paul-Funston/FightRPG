using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Item : GameObject
    {
 

        protected int _value;
        public int Value { get { return _value; } }

        public virtual void Examine()
        {
            Console.WriteLine(Name);
        }


        // constructors
        public Item(string name, int value) : base(name)
        {
            
            _value = value;

        }



    }
}
