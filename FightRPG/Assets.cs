using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FightRPG
{
    public static class Assets
    {
        private static int _bestiaryCount = 5;
        public static int BestiaryCount { get { return _bestiaryCount; } }

        private static HashSet<Weapon> _weapons = InitializeWeapons();
        public static HashSet<Weapon> GetWeapons() { return _weapons.ToHashSet(); }

        private static HashSet<Armor> _armor = InitializeArmor();
        public static HashSet<Armor> GetArmor() { return _armor.ToHashSet(); }

        private static HashSet<GameObject> _allObjects = new();
        public static HashSet<GameObject> GetAllObjects () { return _allObjects.ToHashSet(); }

        public static void AddObject(GameObject obj) { _allObjects.Add(obj); }

        private static string[] _adjectives = InitializeAdjectives();
        public static string[] Adjectives { get { return _adjectives.ToArray(); } }

        private static string[] _adverbs = InitializeAdverbs();
        public static string[] Adverbs { get { return _adverbs.ToArray(); } }

        public static Fight debugFight = new Fight(new HashSet<Hero>() { new Hero("Dummy", 99, 99, 99, 99, new Armor("Demon", 0, 99, 99), new Weapon("Demon", 0, 99, 99)) }, new HashSet<Monster>() { new Monster("Demon", 1000, 1000, 1000, 1000, 1000) });

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

       
        public static Monster GetMonster(int index, int level)
        {
            switch(index)
            {
                case 0:
                    return new Goblin(level);
                case 1:
                    return new Kobold(level);
                case 2:
                    return new Slime(level);
                case 3:
                    return new YuanTi(level);
                case 4:
                    return new Dragon(level);
                default:
                    throw new Exception("Monster not found.");
            }
        }
        public class Goblin : Monster
        {
            public Goblin(int level) : base("Goblin", level, 10, 2, 4, 0) { }
        }
        public class Kobold : Monster
        {
            public Kobold(int level) : base("Kobold", level, 10, 4, 2, 1) { }
        }
        public class Slime : Monster
        {
            public Slime(int level) : base("Slime", level, 10, 1, 10, 2) { }
        }
        public class YuanTi : Monster
        {
            public YuanTi(int level) : base("YuanTi", level, 15, 5, 5, 3) { }
        }
        public class Dragon : Monster
        {
            public Dragon(int level) : base("Dragon", level, 30, 15, 10, 4) { }
        }

        // Locations

        public static Location Menu = new Location("Menu");
        public static Location Town = new Location("Town");
        public static Location.Shop WeaponShop = new Location.Shop("Weapons");
        
        public static Location.Shop ArmorShop = new Location.Shop("Armor");
        public static Location.Shop Sanctuary = new Location.Shop("Sanctuary");
        public static Location.Shop Tavern = new Location.Shop("Tavern");
        public static Location.Dungeon Forest = new Location.Dungeon("Forest");
        // Other 
        private static string[] InitializeAdjectives()
        {

            string words = "Active, Adaptable, Adept, Adorable, Adventurous, Affable, Affectionate, Aggravating, Aggressive, Aggrieved, Agitated, Agreeable, Aimless, Albino, Alert, Alluring, Aloof, Ambitious, Amiable, Amicable, Amusing, Analytical, Anarchistic, Animated, Annoyed, Annoying, Antsy, Anxious, Apprehensive, Approachable, Argumentative, Arrogant, Attentive, Attractive, Audacious, Avid, Awkward, Baleful, Bashful, Beautiful, Belligerent, Beneficial, Big-headed, Bigoted, Bitchin', Bitchy, Bloviated, Blunt, Boastful, Bold, Bombastic, Bone-idle, Bored, Boring, Bossy, Boundless, Braggadocious, Brash, Brave, Bright, Broke, Buoyant, Burdened, Busted, Callous, Calm, Calming, Cantankerous, Capable, Careful, Careless, Caring, Casual, Catty, Caustic, Cavalier, Changeable, Chaotic, Charming, Cheerful, Cheery, Childish, Chill, Chipper, Chirpy, Chummy, Churlish, Civil, Civilized, Classy, Clean, Clever, Clinging, Clumsy, Cold, Colorful, Commanding, Communicative, Compassionate, Compliant, Compulsive, Concerned, Condescending, Confident, Confused, Conniving, Conscientious, Conservative, Considerate, Conspiratorial, Constant, Contemptuous, Controlling, Convivial, Cooperative, Coquettish, Cordial, Corny, Country, Courageous, Courteous, Courtly, Cowardly, Coy, Cranky, Crass, Crazy, Creative, Creepy, Critical, Crude, Cruel, Cultured, Cunning, Curious, Curt, Curvy, Cute, Cynical, Daring, Dark, Dashing, Dazzling, Debonair, Deceitful, Decisive, Deep, Delighted, Delightful, Deluded, Delusional, Dependent, Depressive, Desperate, Detached, Determined, Diligent, Diplomatic, Directionless, Dirty, Discerning, Discreet, Dishonest, Disorganized, Dissatisfied, Ditzy, Dogmatic, Domineering, Dopey, Dorky, Douchey, Dour, Dowdy, Drunk, Dumb, Dynamic, Eager, Earnest, Earthy, Easygoing, Eclectic, Edgy, Effervescent, Efficient, Egocentric, Egomaniacal, Egotistical, Elated, Elegant, Eloquent, Emo, Emotional, Enchanting, Encouraging, Energetic, Engaging, Enigmatic, Enterprising, Entertaining, Enthusiastic, Entitled, Envious, Erudite, Escapist, Evasive, Evil, Excitable, Excited, Exciting, Experienced, Explosive, Exuberant, Fabulous, Fair, Fair-minded, Faithful, Familiar, Fanatical, Fantastic, Fashionable, Favorable, Fearless, Fervent, Feverish, Finicky, Flexible, Flirtatious, Flowery, Fluffy, Foolhardy, Foolish, Forceful, Forgetful, Forgiving, Frank, Frenzied, Friendly, Frightened, Frizzy, Frumpy, Funny, Fussy, Gabby, Gallant, Garrulous, Generous, Genial, Gentle, Giving, Glamorous, Gloomy, Glorious, Glum, Gnarly, Good-humored, Good-looking, Good-natured, Gormless, Graceful, Gracious, Grand, Grandiose, Greedy, Gregarious, Grouchy, Grumpy, Gullible, Gutsy, Handsome, Happy, Hard-working, Hardcore, Harmonious, Harsh, Hasty, Haughty, Heavy, Heavy Metal, Helpful, Heroic, Hilarious, Hiphop, Hippie, Holy, Honest, Honorable, Hopeful, Hopeless, Horny, Horrible, Hostile, Hot, Humorous, Icy, Ill, Illogical, Imaginative, Impartial, Impassioned, Impatient, Imperial, Impolite, Important, Imposing, Impotent, Impulsive, Incomprehensible, Inconsiderate, Inconsistent, Inconsolable, Indecisive, Indefatigable, Independent, Indifferent, Indiscreet, Industrious, Inflexible, Ingenious, Innocent, Inoffensive, Inquisitive, Insane, Inscrutable, Insightful, Insistent, Insolent, Instinctive, Insubordinate, Intellectual, Intelligent, Intense, Interested, Interesting, Interfering, Intimidating, Intolerant, Intrepid, Intuitive, Inventive, Irascible, Irresponsible, Irritable, Irritated, Itchy, Jaunty, Jealous, Jocky, Jolly, Joyful, Joyous, Keen, Kind, Kind-hearted, Kinky, Knowledgeable, Lackadaisical, Lanky, Lawful, Lax, Lazy, Lecherous, Lethargic, Level-headed, Liberal, Lighthearted, Likeable, Listless, Lively, Logical, Lonely, Longwinded, Loquacious, Lovable, Lovely, Loving, Loyal, Lucky, Machiavellian, Magnificent, Manic, Massive, Materialistic, Mature, Mean, Mellow, Melodramatic, Men's Rights, Menacing, Merry, Messy, Meticulous, Miserly, Misunderstood, Moderate, Modern, Modest, Monumental, Moody, Morbid, Morose, Multi-talented, Mundane, Muscular, Naive, Narcissistic, Narrow-minded, Nasty, Naughty, Near-sighted, Neat, Neckbeard, Negligent, Neighborly, Nerdy, Nervous, Nice, Noble, Non-threatening, Nonchalant, Normcore, Nostalgic, Nosy, Obedient, Obsessive, Obstinate, Optimistic, Orderly, Organized, Ornery, Overcritical, Overemotional, Overreactionary, Painted, Pale, Paranoid, Passionate, Patient, Patronizing, Peaceful, Pensive, Peppy, Perceptive, Perfect, Perky, Persistent, Persuasive, Perverse, Pesky, Pessimistic, Petite, Petulant, Philosophical, Pierced, Pioneering, Placid, Plausible, Pleasant, Pleased, Plucky, Poetic, Polished, Polite, Political, Pompous, Possessive, Powerful, Practical, Preachy, Preppy, Pretentious, Pretty, Privileged, Pro-active, Probing, Productive, Professional, Protective, Proud, Provincial, Punctual, Punk, Pusillanimous, Quarrelsome, Quick-tempered, Quick-witted, Quiet, Rabid, Racist, Randy, Rational, Receptive, Reckless, Reflective, Relentless, Reliable, Relieved, Repeated, Repetitive, Resentful, Reserved, Resilient, Resolute, Resourceful, Respectful, Responsible, Rhapsodic, Rhetorical, Righteous, Robust, Romantic, Rosy, Rude, Ruthless, Sad, Sarcastic, Sassy, Scandalized, Scared, Scarred, Scrubby, Secretive, Sedate, Seemly, Selective, Self-assured, Self-centered, Self-centred, Self-confident, Self-disciplined, Self-effacing, Self-indulgent, Self-serving, Selfish, Selfless, Sensible, Sensitive, Sentimental, Sexy, Shallow, Sharp, Shiftless, Shitty, Short, Short-tempered, Shortsighted, Shrewd, Shy, Silly, Simpering, Simple, Sincere, Single-minded, Skillful, Skittish, Sleazy, Sleepy, Slick, Sloppy, Sluggish, Sly, Small, Smart, Smiling, Smug, Sneaky, Sniffly, Sniveling, Snobbish, Snooty, Sociable, Social Justice, Soft-spoken, Softcore, Solitary, Somber, Sophisticated, Sordid, Soulful, Soulless, Spacey, Spirited, Spiteful, Splendid, Spunky, Squat, Stately, Steadfast, Steady, Stimulating, Stingy, Stinky, Stoic, Stoned, Stout, Straightedge, Straightforward, Strategic, Striking, Strong, Stubborn, Studious, Stupid, Stylish, Succinct, Suggestive, Suicidal, Sullen, Sunny, Superficial, Superstitious, Surly, Suspicious, Sympathetic, Tactless, Talented, Talkative, Tall, Tattooed, Tedious, Teeny, Tenacious, Thick, Thin, Thoughtful, Thoughtless, Thrifty, Tidy, Timid, Tiny, Tired, Tireless, Touchy, Tough, Towering, Tranquil, Trashy, Trustworthy, Turbulent, Ugly, Unassuming, Unbiased, Uncertain, Undercover, Understanding, Unflappable, Unfulfilled, Unholy, Unintelligible, Unkind, Unmotivated, Unpredictable, Unrelenting, Unreliable, Unsteady, Untidy, Untrustworthy, Untruthful, Unusual, Upbeat, Urbane, Useless, Vague, Vain, Valiant, Vapid, Vengeful, Verbose, Vigorous, Violent, Virginal, Vivacious, Vulgar, Warm, Warmhearted, Wary, Wasteful, Weak, Weak-willed, Weary, Welcoming, Well-behaved, Well-mannered, Well-spoken, Whiny, Willing, Windy, Winsome, Wiry, Wise, Wistful, Witty, Woke, Wonderful, Worn-out, Worried, Wrathful, Wretched, Wry, Zealous, Zen";

            return words.Split(',');

        }

        public static string GetRandomAdjective()
        {
            int n = new Random().Next(_adjectives.Length);
            return _adjectives[n].Trim();
        }

        private static string[] InitializeAdverbs()
        {
            string words =
                "abnormally absentmindedly adventurously anxiously arrogantly awkwardly bashfully beautifully bitterly bleakly blindly blissfully boastfully boldly bravely carefully carelessly cautiously cheerfully cleverly courageously cruelly daintily deceivingly defiantly delightfully diligently dreamily elegantly energetically enthusiastically excitedly fast ferociously fervently fiercely foolishly frantically frenetically frightfully furiously gently gleefully gracefully greedily happily hastily helplessly hungrily innocently inquisitively intensely irritably jaggedly jovially joyfully joyously jubilantly judgmentally justly knavishly kookily lazily lightly limply loudly madly majestically miserably mockingly mortally mysteriously naturally oddly optimistically playfully poorly powerfully questionably quickly rapidly recklessly reluctantly repeatedly rigidly roughly rudely slowly swiftly terribly tremendously triumphantly vacantly vainly valiantly viciously victoriously violently vivaciously weakly wearily wildly woefully youthfully zealously zestfully zestily";

            return words.Split(' ');
        }

        public static string GetRandomAdverb()
        {
            int n = new Random().Next(_adverbs.Length);
            return _adverbs[n].Trim() ;
        }
    }
}
