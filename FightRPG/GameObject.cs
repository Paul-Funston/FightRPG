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

        static GameObject() => currentID = 0;
        private int GetNextID() => ++currentID;
        private static int currentID;

        public GameObject() 
        {
            _id = GetNextID();
            //Assets.AddObject(this);
        }

    }
}
