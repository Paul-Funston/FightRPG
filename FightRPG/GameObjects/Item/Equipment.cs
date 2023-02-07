using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public class Equipment : Item
    {
        private int _strength;
        public int Strength { get { return _strength; } }

        private int _defence;
        public int Defence { get { return _defence; } }

        private void EquipItem()
        {
            Game.TryEquipEquipment(Id);
        }

        public override void Examine()
        {
            Console.WriteLine($"{Name} Strength:{_strength} Defence:{_defence}");
        }
        public Equipment(string name, int value, int strength, int defence) : base(name, value)
        {
            _strength= strength;
            _defence= defence;
            _actionsAvailable.Add("Equip", EquipItem);
        }


    }
}
