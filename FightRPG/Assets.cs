using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightRPG
{
    public static class Assets
    {
        private static HashSet<Monster> _bestiary= new HashSet<Monster>();
        public static HashSet<Monster> GetMonsters() { return _bestiary.ToHashSet(); }

        private static HashSet<Weapon> _weapons = InitializeWeapons();
        public static HashSet<Weapon> GetWeapons() { return _weapons.ToHashSet(); }

        private static HashSet<Armor> _armor = InitializeArmor();
        public static HashSet<Armor> GetArmor() { return _armor.ToHashSet(); }



        // Weapons
        private static HashSet<Weapon> InitializeWeapons()
        {
            return new HashSet<Weapon>()
            {
                new Weapon("Club",          5, 2, 0),
                new Weapon("Dagger",        10, 3, 0),
                new Weapon("Spear",         10, 2, 1),
                new Weapon("Mace",          20, 3, 2),
                new Weapon("Sword",         50, 6, 0),
                new Weapon("Warhammer",     100, 10, 0),
                new Weapon("Battleaxe",     250, 20, 0),
                new Weapon("Halberd",       500, 30, 5),
                new Weapon("Greatsword",    1000, 50, 0),

            };
        }

        // Armor
        private static HashSet<Armor> InitializeArmor()
        {
                return new HashSet<Armor>() {

                new Armor("Padded Armor",       5, 0, 2),
                new Armor("Leather Armor",      10, 0, 3),
                new Armor("Studded Leather",    45, 0, 5),

                new Armor("Hide",               10, -5, 5),
                new Armor("Chain Shirt",        50, -10, 10),
                new Armor("Scale Mail",         150, 0, 12),

                new Armor("Spiked Armor",       400, 5, 10),
                new Armor("Breastplate",        750, 0, 30),
                new Armor("Plate",              1500, 0, 50)
            };
        }

        // Consumables?


        // Monsters

        public static HashSet<int> InitializeMonsters()
        {
            return new HashSet<int>()
            {

            }
        }



    }
}
