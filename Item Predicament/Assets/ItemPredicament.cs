using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;

public class ItemPredicament : MonoBehaviour
{

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public GameObject[] GreenStatHighlights;
    public GameObject[] RedStatHighlights;

    //Constants
    private const int NumberOfStats = 8;
    private const int ConditionCount = 5;


    //Texts on Module
    public KMSelectable[] Buttons;
    public TextMesh[] DisplayTexts;

    //static Dictionarys

    //Name / ID / Quality / Stats effected
    Dictionary<string, List<object>> ItemList = new Dictionary<string, List<object>>
{
    // Quality 0
    { "Dead Bird", new List<object> { 117, 0, "0" } },
    { "Hold", new List<object> { 715, 0, "0" } },
    { "Razor Blade", new List<object> { 126, 0, "3" } },
    { "The Poop", new List<object> { 36, 0, "0" } },

    // Quality 1
    { "Anemic", new List<object> { 214, 1, "4" } },
    { "Box of\nFriends", new List<object> { 357, 1, "0" } },
    { "Guppy's\nCollar", new List<object> { 212, 1, "0" } },
    { "Guppy's\nHairball", new List<object> { 187, 1, "0" } },
    { "Spider Mod", new List<object> { 403, 1, "0" } },
    { "The Belt", new List<object> { 28, 1, "1" } },
    { "Wooden\nNickel", new List<object> { 349, 1, "0" } },
    { "Yum Heart", new List<object> { 45, 1, "0" } },

    // Quality 2
    { "1UP", new List<object> { 11, 2, "0" } },
    { "120 Volt", new List<object> { 559, 2, "0" } },
    { "Bobby-Bomb", new List<object> { 125, 2, "0" } },
    { "Blank Card", new List<object> { 286, 2, "0" } },
    { "Cambion\nConception", new List<object> { 412, 2, "0" } },
    { "Dark Arts", new List<object> { 705, 2, "1" } },
    { "Guardian\nAngel", new List<object> { 112, 2, "0" } },
    { "Guppy's Eye", new List<object> { 665, 2, "0" } },
    { "Guppy's Head", new List<object> { 145, 2, "0" } },
    { "Guppy's Tail", new List<object> { 134, 2, "0" } },
    { "Kidney Stone", new List<object> { 440, 2, "2" } },
    { "Lucky Foot", new List<object> { 46, 2, "6" } },
    { "Money\n=\nPower", new List<object> { 109, 2, "3" } },
    { "Pandora's\nBox", new List<object> { 297, 2, "0" } },
    { "Soy Milk", new List<object> { 330, 2, "2-3" } },
    { "Steam Sale", new List<object> { 64, 2, "0" } },
    { "Suplex!", new List<object> { 709, 2, "0" } },
    { "The Book\nof Belial", new List<object> { 34, 2, "3" } },
    { "Whore of\nBabylon", new List<object> { 122, 2, "1,3" } },

    // Quality 3
    { "Anima Sola", new List<object> { 722, 3, "0" } },
    { "Bag of\nCrafting", new List<object> { 710, 3, "0" } },
    { "Book of\nVirtues", new List<object> { 584, 3, "0" } },
    { "Chaos", new List<object> { 402, 3, "0" } },
    { "Cricket's\nBody", new List<object> { 224, 3, "2,-4" } },
    { "Cricket's\nHead", new List<object> { 4, 3, "3" } },
    { "Dead Cat", new List<object> { 81, 3, "0" } },
    { "Dead Eye", new List<object> { 373, 3, "3" } },
    { "Death's\nTouch", new List<object> { 237, 3, "-2,3" } },
    { "Eden's\nBlessing", new List<object> { 381, 3, "2" } },
    { "Eternal D6", new List<object> { 609, 3, "0" } },
    { "Guppy's Paw", new List<object> { 133, 3, "0" } },
    { "Keeper's\nSack", new List<object> { 716, 3, "0" } },
    { "Lemegeton", new List<object> { 712, 3, "0" } },
    { "Proptosis", new List<object> { 261, 3, "3" } },
    { "Rock Bottom", new List<object> { 562, 3, "0" } },
    { "Sumptorium", new List<object> { 713, 3, "0" } },
    { "Technology\nZero", new List<object> { 524, 3, "0" } },
    { "Wire Coat\nHanger", new List<object> { 32, 3, "2" } },

    // Quality 4
    { "20/20", new List<object> { 245, 4, "-3" } },
    { "Abyss", new List<object> { 706, 4, "0" } },
    { "Brimstone", new List<object> { 118, 4, "-2" } },
    { "C Section", new List<object> { 678, 4, "0" } },
    { "Flip", new List<object> { 711, 4, "0" } },
    { "Godhead", new List<object> { 331, 4, "-2,3,-5" } },
    { "Holy Mantle", new List<object> { 313, 4, "0" } },
    { "Incubus", new List<object> { 360, 4, "0" } },
    { "Mom's Knife", new List<object> { 114, 4, "0" } },
    { "Polyphemus", new List<object> { 169, 4, "-2,3" } },
    { "R Key", new List<object> { 636, 4, "0" } },
    { "Tech X", new List<object> { 395, 4, "0" } },
    { "The D6", new List<object> { 105, 4, "0" } },
    { "Void", new List<object> { 477, 4, "0" } }
};


    //Name / Pickups / Vowels * Konsonants
    static Dictionary<string, List<int>> CharacterList = new Dictionary<string, List<int>>
        {

            //Normal Characters

            { "Isaac", new List<int> { 1, 6 } },
            { "Magdalene", new List<int> { 1, 20 } },
            { "Cain", new List<int> { 2, 4 } },
            { "Judas", new List<int> { 3, 6 } },
            { "??? (Blue Baby)", new List<int> { 0, 15 } },
            { "Eve", new List<int> { 0, 2 } },
            { "Samson", new List<int> { 1, 8 } },
            { "Azazel", new List<int> { 1, 9 } },
            { "Lazarus", new List<int> { 1, 12 } },
            { "Eden", new List<int> { 0, 4 } },
            { "The Lost", new List<int> { 1, 10 } },
            { "Lilith", new List<int> { 0, 8 } },
            { "Keeper", new List<int> { 3, 9 } },
            { "Apollyon", new List<int> { 0, 15 } },
            { "The Forgotten", new List<int> { 0, 32 } },
            { "Bethany", new List<int> { 4, 10 } },
            { "Jacob and Esau", new List<int> { 0, 36 } },

            //Tainted Characters

            { "Tainted Isaac", new List<int> { 1, 36 } },
            { "Tainted Magdalene", new List<int> { 0, 63 } },
            { "Tainted Cain", new List<int> { 1, 30 } },
            { "Tainted Judas", new List<int> { 3, 35 } },
            { "Tainted ??? (Blue Baby)", new List<int> { 0, 54 } },
            { "Tainted Eve", new List<int> { 0, 25 } },
            { "Tainted Samson", new List<int> { 0, 40 } },
            { "Tainted Azazel", new List<int> { 1, 42 } },
            { "Tainted Lazarus", new List<int> { 0, 48 } },
            { "Tainted Eden", new List<int> { 0, 30 } },
            { "Tainted Lost", new List<int> { 1, 28 } },
            { "Tainted Lilith", new List<int> { 0, 40 } },
            { "Tainted Keeper", new List<int> { 3, 42 } },
            { "Tainted Apollyon", new List<int> { 0, 54 } },
            { "Tainted Forgotten", new List<int> { 0, 60 } },
            { "Tainted Bethany", new List<int> { 6, 45 } },
            { "Tainted Jacob", new List<int> { 0, 35 } }
        };

    //Name / I, II or None / Letter Count
    static Dictionary<string, List<int>> ChapterList = new Dictionary<string, List<int>>
    {
        { "Basement I", new List<int> { 1, 8 } },
        { "Basement II", new List<int> { 2, 8 } },
        { "Cellar I", new List<int> { 1, 6 } },
        { "Cellar II", new List<int> { 2, 6 } },
        { "Burning Basement I", new List<int> { 1, 15 } },
        { "Burning Basement II", new List<int> { 2, 15 } },
        { "Downpour I", new List<int> { 1, 8 } },
        { "Downpour II", new List<int> { 2, 8 } },
        { "Dross I", new List<int> { 1, 5 } },
        { "Dross II", new List<int> { 2, 5 } },
        { "Caves I", new List<int> { 1, 5 } },
        { "Caves II", new List<int> { 2, 5 } },
        { "Catacombs I", new List<int> { 1, 9 } },
        { "Catacombs II", new List<int> { 2, 9 } },
        { "Flooded Caves I", new List<int> { 1, 12 } },
        { "Flooded Caves II", new List<int> { 2, 12 } },
        { "Mines I", new List<int> { 1, 5 } },
        { "Mines II", new List<int> { 2, 5 } },
        { "Ashpit I", new List<int> { 1, 6 } },
        { "Ashpit II", new List<int> { 2, 6 } },
        { "Depths I", new List<int> { 1, 6 } },
        { "Depths II", new List<int> { 2, 6 } },
        { "Necropolis I", new List<int> { 1, 10 } },
        { "Necropolis II", new List<int> { 2, 10 } },
        { "Dank Depths I", new List<int> { 1, 10 } },
        { "Dank Depths II", new List<int> { 2, 10 } },
        { "Mausoleum I", new List<int> { 1, 9 } },
        { "Mausoleum II", new List<int> { 2, 9 } },
        { "Gehenna I", new List<int> { 1, 7 } },
        { "Gehenna II", new List<int> { 2, 7 } },
        { "Womb I", new List<int> { 1, 4 } },
        { "Womb II", new List<int> { 2, 4 } },
        { "Utero I", new List<int> { 1, 5 } },
        { "Utero II", new List<int> { 2, 5 } },
        { "Scarred Womb I", new List<int> { 1, 11 } },
        { "Scarred Womb II", new List<int> { 2, 11 } },
        { "???", new List<int> { 0, 0 } },
        { "Corpse I", new List<int> { 1, 6 } },
        { "Corpse II", new List<int> { 2, 6 } },
        { "Cathedral", new List<int> { 0, 9 } },
        { "Sheol", new List<int> { 0, 5 } },
        { "Chest", new List<int> { 0, 5 } },
        { "Dark Room", new List<int> { 0, 8 } },
        { "The Void", new List<int> { 0, 7 } },
        { "Home", new List<int> { 0, 4 } },
    };

    //Name / Start Item
    static Dictionary<string, List<string>> CharacterStartItemList = new Dictionary<string, List<string>>
    {

        //Normal Characters

        { "Isaac", new List<string> { "The D6" } },
        { "Magdalene", new List<string> { "Yum Heart" } },
        { "Cain", new List<string> { "Lucky Foot" } },
        { "Judas", new List<string> { "The Book\nof Belial" } },
        { "Eve", new List<string> { "Whore of\nBabylon", "Dead Bird", "Razor Blade" } },
        { "Samson", new List<string> { "Bloody Lust" } },
        { "Azazel", new List<string> { "Brimstone" } },
        { "Lazarus", new List<string> { "Anemic" } },
        { "Eden", new List<string> { "Error 404" } },
        { "The Lost", new List<string> { "Eternal D6", "Holy Mantle" } },
        { "Lilith", new List<string> { "Incubus", "Cambion\nConcept", "Box of\nFriends" } },
        { "Keeper", new List<string> { "Wooden\nNickel" } },
        { "Apollyon", new List<string> { "Void" } },
        { "Bethany", new List<string> { "Book of\nVirtues" } },

        // Tainted Characters

        { "Tainted Magdalene", new List<string> { "Yum Heart" } },
        { "Tainted Cain", new List<string> { "Bag of\nCrafting" } },
        { "Tainted Judas", new List<string> { "Dark Arts" } },
        { "Tainted ??? (Blue Baby)", new List<string> { "Hold" } },
        { "Tainted Eve", new List<string> { "Sumptorium" } },
        { "Tainted Lazarus", new List<string> { "Flip" } },
        { "Tainted Eden", new List<string> { "Error 404" } },
        { "Tainted Bethany", new List<string> { "Lemegeton" } },
        { "Tainted Jacob", new List<string> { "Anima Sola" } }
    };

    //Name / Item
    static Dictionary<string, List<string>> ItemPools = new Dictionary<string, List<string>>
    {
        // Boss Room Item Pool
        { "Boss Room", new List<string> { "The Belt", "Wire Coat\nHanger", "Lucky Foot", "Cricket's\nBody", "Soy Milk", "120 Volt", "Anima Sola", "Suplex!" } },

        // Treasure Room Item Pool
        { "Treasure Room", new List<string> { "Cricket's\nHead", "The D6", "Technology\nZero", "Dead Bird", "Bobby-Bomb", "20/20", "The Poop", "Yum Heart", "Razor Blade", "Dead Eye", "Polyphemus", "Anemic", "Proptosis" } },

        // Shop Room Item Pool
        { "Shop", new List<string> { "Steam Sale", "Hold", "Money\n=\nPower", "Blank Card", "Keeper's\nSack", "Wooden\nNickel", "Bag of\nCrafting", "Spider Mod", "Box of\nFriends" } },

        // Curse Room Item Pool
        { "Curse Room", new List<string> { "Guppy's Eye", "Whore of\nBabylon", "Guppy's Paw", "Pandora's\nBox", "Dark Arts", "Guppy's Head", "Guppy's\nHairball" } },

        // Secret Room Item Pool
        { "Secret Room", new List<string> { "1UP", "Tech X", "Chaos", "Sumptorium", "Rock Bottom", "Dead Cat", "R Key", "Death's\nTouch", "C Section", "Kidney Stone" } },

        // Angel Deal Item Pool
        { "Angel Deal", new List<string> { "Guardian\nAngel", "Holy Mantle", "Eternal D6", "Godhead", "Eden's\nBlessing", "Void", "Book of\nVirtues" } },

        // Devil Deal Item Pool
        { "Devil Deal", new List<string> { "The Book\nof Belial", "Abyss", "Mom's Knife", "Brimstone", "Flip", "Guppy's Tail", "Lemegeton", "Guppy's\nCollar", "Incubus", "Cambion\nConception" } }
    };

    //Default Var

    string SerialNumber;
    string ConvertedSerialNumber;
    int BatteryCount;

    string ChapterWithRoman;
    string Chapter;
    string Character;
    List<float> Stats;
    List<string> Items;
    int EdenPickups;
    string EdenItem;

    //Step 1 Var
    int Roman;
    int FirstMatchInSerialNumber;
    string YourRoom;

    //Step 2 Var
    int CharacterNumber;
    List<int> YourStats;

    //Step 3 Var
    Dictionary<int, List<int>> ItemStats;
    List<bool> ButtonNeedsPress;
    List<int> ButtonOrder;


    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

    void Awake()
    { //Avoid doing calculations in here regarding edgework. Just use this for setting up buttons for simplicity.
        ModuleId = ModuleIdCounter++;
        GetComponent<KMBombModule>().OnActivate += Activate;

        foreach (KMSelectable Button in Buttons) {
            Button.OnInteract += delegate () { ButtonPress(Button); return false; };
        }
    }

    void OnDestroy()
    { //Shit you need to do when the bomb ends

    }

    void Activate()
    { //Shit that should happen when the bomb arrives (factory)/Lights turn on

    }

    void Start()
    { //Shit that you calculate, usually a majority if not all of the module

        //Get Bomb Info
        SerialNumber = Bomb.GetSerialNumber();
        ConvertedSerialNumber = ConvertSerialNumber(SerialNumber);
        BatteryCount = Bomb.GetBatteryCount();

        //
        // Setup Bomb (Randomize stuff)
        //

        //Eden
        EdenPickups = GetEdenPickups();
        CharacterList["Eden"][0] = EdenPickups;
        CharacterList["Tainted Eden"][0] = EdenPickups;

        EdenItem = GetEdenItem();
        CharacterStartItemList["Eden"][0] = EdenItem;
        CharacterStartItemList["Tainted Eden"][0] = EdenItem;

        //Set Highlights
        DeHighlight();

        //Chapter Name
        DisplayTexts[0].text = ChapterWithRoman = GetRandomChapterName();

        //Character Name
        DisplayTexts[1].text = Character = GetRandomCharacterName();

        //Stats Values
        //Coming after prep
        Stats = GetRandomStats();
        DisplayTexts[2].text = "Speed:      " + Stats[0] +
                               "\nTears:      " + Stats[1] +
                               "\nDamage:     " + Stats[2] +
                               "\nRange:      " + Stats[3] +
                               "\nShotspeed:  " + Stats[4] +
                               "\nLuck:       " + Stats[5] +
                               "\nDevil Deal: " + Stats[6] + "%" +
                               "\nAngel Deal: " + Stats[7] + "%";

        //Item Names
        Items = GetRandomItemNames();
        for (int i = 0; i < Items.Count; i++)
        {
            DisplayTexts[i + 3].text = Items[i];
        }

        //
        //Step 1
        //

        Roman = GetRomanAsIntAndConvertChapterRomanToChapter();
        FirstMatchInSerialNumber = GetFirstMatchInConvertedSerialNumber();
        YourRoom = GetYourRoom();
        Debug.Log("---YOUR ROOM---\n" + YourRoom);

        //
        //Step 2
        //

        CharacterNumber = CalculateCharacterNumber();
        YourStats = GetYourStats();
        Debug.Log("---YOUR STATS---");
        foreach (int stats in YourStats)
        {
            Debug.Log(stats);
        }



        //
        //Step 3
        //

        //Stats for our Items on the Buttons for ease of use
        List<int> Item1Stats = ConvertIntToList(ItemList[Items[0]][2]);
        List<int> Item2Stats = ConvertIntToList(ItemList[Items[1]][2]);
        List<int> Item3Stats = ConvertIntToList(ItemList[Items[2]][2]);
        List<int> Item4Stats = ConvertIntToList(ItemList[Items[3]][2]);
        ItemStats = new Dictionary<int, List<int>>
        {
            {0, Item1Stats },
            {1, Item2Stats },
            {2, Item3Stats },
            {3, Item4Stats },
        };

        ButtonNeedsPress = GetButtonsThatNeedsToBePressed();
        ButtonOrder = GetButtonOrder();
        Debug.Log("---CORRECT BUTTON ORDER IN ID'S---");
        foreach (int button in ButtonOrder)
        {
            Debug.Log(button);
        }

        foreach (KMSelectable Button in Buttons)
        {
            Button.OnHighlight += delegate () { HighlightStats(Button); };
        }

        foreach (KMSelectable Button in Buttons)
        {
            Button.OnHighlightEnded += delegate () { DeHighlight(); };
        }
    }

    void Update()
    { //Shit that happens at any point after initialization

    }

    #region Setup Methodes

    string GetRandomChapterName()
    {
        return ChapterList.Keys.ElementAt(Random.Range(0, ChapterList.Count));
    }

    string GetRandomCharacterName()
    {
        return CharacterList.Keys.ElementAt(Random.Range(0, CharacterList.Count));
    }

    List<string> GetRandomItemNames()
    {
        List<string> keys = ItemList.Keys.ToList();
        List<string> randomItems = new List<string>();

        int count = keys.Count;
        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, count - i);
            randomItems.Add(keys[index]);
            keys[index] = keys[count - i - 1];
        }
        return randomItems;
    }

    List<float> GetRandomStats()
    {
        int[] minValues = { 85, 223, 250, 500, 75, 0, 0 };
        int[] maxValues = { 155, 348, 450, 800, 125, 100, 100 };
        List<float> randomStats = new List<float>();

        for (int i = 0; i < minValues.Length - 1; i++)
        {
            randomStats.Add(Random.Range(minValues[i], maxValues[i]) / 100f);
        }

        randomStats.Add(Random.Range(minValues[6], maxValues[6]));
        randomStats.Add(100 - randomStats[6]);

        return randomStats;
    }

    #endregion

    #region Step 1

    string ConvertSerialNumber(string serialNumber)
    {
        string result = "";

        foreach (char c in serialNumber)
        {
            if (char.IsDigit(c))
            {
                result += (char)('a' + (c - '0'));
            }
            else
            {
                result += c;
            }
        }
        return result;
    }

    int GetRomanAsIntAndConvertChapterRomanToChapter()
    {
        if (ChapterWithRoman.EndsWith(" I"))
        {
            Chapter = ChapterWithRoman.Substring(0, ChapterWithRoman.Length - 2);
            return 1;
        }
        else if (ChapterWithRoman.EndsWith(" II"))
        {

            Chapter = ChapterWithRoman.Substring(0, ChapterWithRoman.Length - 3);
            return 2;
        }

        Chapter = ChapterWithRoman;
        return 0;
    }

    int GetFirstMatchInConvertedSerialNumber()
    {
        for (int i = 0; i < ConvertedSerialNumber.Length; i++)
        {
            string currentCharacter = ConvertedSerialNumber[i].ToString();
            if (Chapter.IndexOf(currentCharacter, System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return i + 1;
            }
        }
        return 0;
    }

    string GetYourRoom()
    {
        Dictionary<int, string> yourRooms = new Dictionary<int, string>
        {
            { 10, "Secret Room" },
            { 11, "Shop" },
            { 12, "Treasure Room" },
            { 20, "Shop" },
            { 21, "Curse Room" },
            { 22, "Boss Room" },
            { 30, "Treasure Room" },
            { 31, "Secret Room" },
            { 32, "Shop" },
            { 40, "Secret Room" },
            { 41, "Boss Room" },
            { 42, "Treasure Room" },
            { 50, "Curse Room" },
            { 51, "Treasure Room" },
            { 52, "Boss Room" },
            { 60, "Treasure Room" },
            { 61, "Shop" },
            { 62, "Curse Room" },
        };

        int generatedKey = FirstMatchInSerialNumber * 10 + Roman;
        if (yourRooms.ContainsKey(generatedKey))
        {
            return yourRooms[generatedKey];
        }

        return "Boss Room";
    }

    #endregion

    #region Step 2

    int CalculateCharacterNumber()
    {
        int yourRoomCount = YourRoom.Count(char.IsLetter);
        int vowelsTimesConsonantsNumber = CharacterList[Character][1];

        if (BatteryCount != 0 && vowelsTimesConsonantsNumber % BatteryCount == 0)
        {
            return vowelsTimesConsonantsNumber + yourRoomCount;
        }

        return Mathf.Abs(vowelsTimesConsonantsNumber - yourRoomCount);
    }

    string ConvertCharacterNumber()
    {
        if (CharacterNumber <= 10)
            return "1";
        if (CharacterNumber <= 20)
            return "2";
        if (CharacterNumber <= 30)
            return "3";
        if (CharacterNumber == 30)
            return "4";
        if (CharacterNumber >= 30)
            return "5";
        if (CharacterNumber >= 40)
            return "6";
        if (CharacterNumber >= 50)
            return "7";
        return "8";
    }

    List<int> GetYourStats()
    {
        List<int> yourStats = new List<int>();

        Dictionary<string, List<int>> statDictionary = new Dictionary<string, List<int>>
        {
            //Boss Room
            { "Boss Room_1", new List<int> { 6, 1, 4 } },
            { "Boss Room_2", new List<int> { 6, 1 } },
            { "Boss Room_3", new List<int> { 6 } },
            { "Boss Room_4", new List<int> { 6, 3 } },
            { "Boss Room_5", new List<int> { 3 } },
            { "Boss Room_6", new List<int> { 3, 5 } },
            { "Boss Room_7", new List<int> { 3, 5, 2 } },
            { "Boss Room_8", new List<int> { 3, 5, 2, 8 } },

            //Treasure Room
            { "Treasure Room_1", new List<int> { 2, 1, 5 } },
            { "Treasure Room_2", new List<int> { 2, 1 } },
            { "Treasure Room_3", new List<int> { 2 } },
            { "Treasure Room_4", new List<int> { 2, 4 } },
            { "Treasure Room_5", new List<int> { 4 } },
            { "Treasure Room_6", new List<int> { 4, 3 } },
            { "Treasure Room_7", new List<int> { 4, 3, 8 } },
            { "Treasure Room_8", new List<int> { 4, 3, 8, 6 } },

            //Shop
            { "Shop_1", new List<int> { 2, 4, 6 } },
            { "Shop_2", new List<int> { 2, 4 } },
            { "Shop_3", new List<int> { 2 } },
            { "Shop_4", new List<int> { 2, 1 } },
            { "Shop_5", new List<int> { 1 } },
            { "Shop_6", new List<int> { 1, 3 } },
            { "Shop_7", new List<int> { 1, 3, 7 } },
            { "Shop_8", new List<int> { 1, 3, 7, 5 } },

            //Curse Room
            { "Curse Room_1", new List<int> { 7, 3, 5 } },
            { "Curse Room_2", new List<int> { 7, 3 } },
            { "Curse Room_3", new List<int> { 7 } },
            { "Curse Room_4", new List<int> { 7, 6 } },
            { "Curse Room_5", new List<int> { 6 } },
            { "Curse Room_6", new List<int> { 6, 4 } },
            { "Curse Room_7", new List<int> { 6, 4, 2 } },
            { "Curse Room_8", new List<int> { 6, 4, 2, 1 } },

            //Secret Room
            { "Secret Room_1", new List<int> { 1, 5, 3 } },
            { "Secret Room_2", new List<int> { 1, 5 } },
            { "Secret Room_3", new List<int> { 1 } },
            { "Secret Room_4", new List<int> { 1, 7 } },
            { "Secret Room_5", new List<int> { 7 } },
            { "Secret Room_6", new List<int> { 7, 2 } },
            { "Secret Room_7", new List<int> { 7, 2, 8 } },
            { "Secret Room_8", new List<int> { 7, 2, 8, 4 } },
        };

        string generateKey = YourRoom + "_" + ConvertCharacterNumber();

        yourStats.AddRange(statDictionary[generateKey]);

        return yourStats;
    }

    #endregion

    #region Step 3

    List<bool> GetButtonsThatNeedsToBePressed()
    {
        List<bool> buttonPressListMain = new List<bool> { false, false, false, false };

        for (int i = 0; i < buttonPressListMain.Count; i++)
        {
            if (!buttonPressListMain[i]) buttonPressListMain[i] = FirstCheck(i);
            if (!buttonPressListMain[i]) buttonPressListMain[i] = SecondCheck(i);
            if (!buttonPressListMain[i]) buttonPressListMain[i] = ThirdCheck(i);
            if (!buttonPressListMain[i]) buttonPressListMain[i] = FourthCheck(i);
        }

        if (buttonPressListMain.All(x => !x))
        {
            for (int i = 0; i < buttonPressListMain.Count; i++)
            {
                buttonPressListMain[i] = true;
            }
        }



        return buttonPressListMain;

    }

    bool FirstCheck(int index)
    {
        if (ItemStats[index].Any(x => YourStats.Contains(x) && x >= 0))
        {
            return true;
        }

        return false;
    }

    bool SecondCheck(int index)
    {
        int characterPickups = CharacterList[Character][0];
        int itemID = int.Parse(ItemList[Items[index]][0].ToString());

        if (itemID.ToString().Contains(characterPickups.ToString()))
        {
            return true;
        }

        return false;
    }

    bool ThirdCheck(int index)
    {
        if (Stats[6] < 70 && Stats[7] < 70)
        {
            return false;
        }

        if (ItemPools["Angel Deal"].Contains(Items[index]) || ItemPools["Devil Deal"].Contains(Items[index]))
        {
            return true;
        }

        return false;

    }

    bool FourthCheck(int index)
    {
        if (!CharacterStartItemList.ContainsKey(Character))
        {
            return false;
        }

        if (CharacterStartItemList[Character].Contains(Items[index]))
        {
            return true;
        }

        return false;
    }


    List<int> GetButtonOrder()
    {
        List<int> buttonOrder = new List<int>();

        Dictionary<int, List<int>> buttonOrderingDictionary = new Dictionary<int, List<int>>
        {
            { int.Parse(ItemList[DisplayTexts[3].text][0].ToString()), new List<int>() },
            { int.Parse(ItemList[DisplayTexts[4].text][0].ToString()), new List<int>() },
            { int.Parse(ItemList[DisplayTexts[5].text][0].ToString()), new List<int>() },
            { int.Parse(ItemList[DisplayTexts[6].text][0].ToString()), new List<int>() }
        };
        
        for (int i = 0; i < 4; i++)
        {
            if (!ButtonNeedsPress[i])
            {
                buttonOrderingDictionary.Remove(int.Parse(ItemList[DisplayTexts[i + 3].text][0].ToString()));
            }
        }

        int index = 0;
        foreach ( var kav in buttonOrderingDictionary)
        {
            kav.Value.Add(FirstOrderCheck(index));
            kav.Value.Add(SecondOrderCheck(index));
            kav.Value.Add(ThirdOrderCheck(index));
            kav.Value.Add(FourthOrderCheck(index));
            kav.Value.Add(FifthOrderCheck(index));
            index++;
        }

        for (int i = 1; i <= ConditionCount; i++)
        {
            if (buttonOrderingDictionary.Values.Any() && buttonOrderingDictionary.Values.Select(val => val[0]).Any())
            {
                int keyID = buttonOrderingDictionary.Where(kav => kav.Value.Contains(i)).OrderByDescending(kav => kav.Key).Select(kav => kav.Key).FirstOrDefault();
                if (keyID != 0)
                {
                    buttonOrder.Add(keyID);
                    buttonOrderingDictionary.Remove(keyID);
                }
            }
        }

        foreach (var kvp in ItemList)
        {
            if (buttonOrderingDictionary.ContainsKey(int.Parse(kvp.Value[0].ToString())))
            {
                buttonOrder.Add(int.Parse(kvp.Value[0].ToString()));
            }
        }

        return buttonOrder;

    }

    int FirstOrderCheck(int index)
    {
        Debug.Log("---ITEMPOOL CHECK---\n" + Items[index] + " in " + YourRoom + "?: " + ItemPools[YourRoom].Contains(Items[index]).ToString());
        if (ItemPools[YourRoom].Contains(Items[index]))
        {
            return 1;
        }

        return 0;
    }

    int SecondOrderCheck(int index)
    {
        Debug.Log("---ITEM IN DD OR AD?---\n" + Items[index] + ": " + (ItemPools["Devil Deal"].Contains(Items[index]) || ItemPools["Angel Deal"].Contains(Items[index])).ToString());
        if (ItemPools["Devil Deal"].Contains(Items[index]) || ItemPools["Angel Deal"].Contains(Items[index]))
        {
            return 2;
        }

        return 0;
    }

    int ThirdOrderCheck(int index)
    {
        Debug.Log("---AFFECTED STAT EVEN?---");
        foreach (int stat in ItemStats[index])
        {
            int absoluteStat = System.Math.Abs(stat);

            Debug.Log("Absolute stat value of " + Items[index] + ", " + absoluteStat.ToString() + " even?: " + (YourStats.Contains(stat) && absoluteStat % 2 == 0).ToString());

            if (YourStats.Contains(absoluteStat) && absoluteStat % 2 == 0)
            {
                return 3;
            }
        }

        return 0;
    }

    int FourthOrderCheck(int index)
    {
        Debug.Log("---ITEM IS AT LEAST QUALITY 3---\nQuality of Item " + Items[index] + " is " + ItemList[Items[index]][1].ToString());
        if (int.Parse(ItemList[Items[index]][1].ToString()) >= 3)
        {
            return 4;
        }
        return 0;
    }

    int FifthOrderCheck(int index)
    {
        Debug.Log("---ID IS UNEVEN AND QUALITY IS NOT 2?---\n" + Items[index] + " ID " + ItemList[Items[index]][0].ToString() + " is not even?: " + (int.Parse(ItemList[Items[index]][0].ToString()) % 2 != 0).ToString() + ", " + Items[index] + " is not quality 2?: " + (int.Parse(ItemList[Items[index]][1].ToString()) != 2).ToString());
        if (int.Parse(ItemList[Items[index]][0].ToString()) % 2 != 0 && int.Parse(ItemList[Items[index]][1].ToString()) != 2)
        {
            return 5;
        }
        return 0;
    }

    #endregion

    #region Extras

    int GetEdenPickups()
    {
        return Bomb.GetSerialNumberNumbers().FirstOrDefault();
    }

    string GetEdenItem()
    {
        string lastNumber = Bomb.GetSerialNumberLetters().LastOrDefault().ToString();

        var sortedItemDictonary = ItemList.OrderBy(kvp => kvp.Value[0])
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        foreach (var kvp in sortedItemDictonary)
        {
            if (kvp.Value[0].ToString().Contains(lastNumber))
            {
                return kvp.Key;
            }
        }
        return "None";
    }

    static List<int> ConvertIntToList(object number)
    {
        string str = number.ToString();
        List<int> resultList = new List<int>();

        foreach (string num in str.Split(','))
        {
            int result;
            if (int.TryParse(num.Trim(), out result))
            {
                resultList.Add(result);
            }
        }

        return resultList;
    }

    void HighlightStats(KMSelectable button)
    {
        for (int i = 0; i < 4; i++)
        {
            if (button == Buttons[i])
            {
                foreach (int item in ItemStats[i])
                {
                    if (item > 0)
                    {
                        GreenStatHighlights[item - 1].SetActive(true);
                    }
                    else if (item < 0)
                    {
                        RedStatHighlights[System.Math.Abs(item) - 1].SetActive(true);
                    }
                }

            }
        }
    }

    void DeHighlight()
    {
        foreach (GameObject highlight in GreenStatHighlights)
        {
            highlight.SetActive(false);
        }
        foreach (GameObject highlight in RedStatHighlights)
        {
            highlight.SetActive(false);
        }
    }

    #endregion

    #region Solving

    void ButtonPress(KMSelectable button)
    {
        button.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, button.transform);

        if (ModuleSolved)
        {
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            if (button == Buttons[i])
            {

                if (int.Parse(ItemList[Items[i]][0].ToString()) == ButtonOrder.First())
                {
                    ButtonOrder.RemoveAt(0);
                }
                else
                {
                    Strike();
                }

                if (ButtonOrder.Count() == 0)
                {
                    Solve();
                }

            }
        }
    }

    #endregion

    void Solve()
    {
        ModuleSolved = true;
        GetComponent<KMBombModule>().HandlePass();
    }

    void Strike()
    {
        GetComponent<KMBombModule>().HandleStrike();
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string Command)
    {
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
    }
}
