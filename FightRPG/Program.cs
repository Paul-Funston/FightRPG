using FightRPG;


foreach( Armor armor in Assets.GetArmor())
{
    Console.WriteLine($"{armor.Name} Grants {armor.Defence} defence.");
}

foreach(Weapon weapon in Assets.GetWeapons())
{
    Console.Write($"{weapon.Name} gives {weapon.Strength} strength");
    if(weapon.Defence != 0)
    {
        Console.WriteLine($" and {weapon.Defence} defence.");
    } else
    {
        Console.WriteLine(".");
    }

}

Game.GameStart();