using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using Math = ExMath;

public class ItemPredicament : MonoBehaviour
{

    public KMBombInfo Bomb;
    public KMAudio Audio;

    //Texts on Module
    public KMSelectable[] Buttons;
    public TextMesh[] DisplayTexts;

    //static Dictionarys

        //Name / ID / Quality
    static Dictionary<string, List<int>> ItemList = new Dictionary<string, List<int>>
            {
                { "Cricket's Head", new List<int> { 4, 3 } },
                { "1UP", new List<int> { 11, 1 } },
                { "The Belt", new List<int> { 28, 2 } },
                { "Wire Coat Hanger", new List<int> { 32, 2 } },
                { "The Book of Belial", new List<int> { 34, 4 } },
                { "The Poop", new List<int> { 36, 0 } },
                { "Yum Heart", new List<int> { 45, 1 } },
                { "Lucky Foot", new List<int> { 46, 2 } },
                { "Steam Sale", new List<int> { 64, 3 } },
                { "Dead Cat", new List<int> { 81, 3 } },
                { "The D6", new List<int> { 105, 4 } },
                { "Money = Power", new List<int> { 109, 3 } },
                { "Guardian Angel", new List<int> { 112, 4 } },
                { "Mom's Knife", new List<int> { 114, 4 } },
                { "Dead Bird", new List<int> { 117, 1 } },
                { "Brimstone", new List<int> { 118, 4 } },
                { "Whore of Babylon", new List<int> { 122, 2 } },
                { "Bobby-Bomb", new List<int> { 125, 2 } },
                { "Razor Blade", new List<int> { 126, 1 } },
                { "Guppy's Paw", new List<int> { 133, 3 } },
                { "Guppy's Tail", new List<int> { 134, 3 } },
                { "Guppy's Head", new List<int> { 145, 3 } },
                { "Polyphemus", new List<int> { 169, 4 } },
                { "Guppy's Hairball", new List<int> { 187, 2 } },
                { "Guppy's Collar", new List<int> { 212, 3 } },
                { "Anemic", new List<int> { 214, 1 } },
                { "Cricket's Body", new List<int> { 224, 3 } },
                { "Death's Touch", new List<int> { 237, 4 } },
                { "20/20", new List<int> { 245, 3 } },
                { "Proptosis", new List<int> { 261, 4 } },
                { "Blank Card", new List<int> { 286, 2 } },
                { "Pandora's Box", new List<int> { 297, 3 } },
                { "Holy Mantle", new List<int> { 313, 4 } },
                { "Soy Milk", new List<int> { 330, 0 } },
                { "Godhead", new List<int> { 331, 4 } },
                { "Wooden Nickel", new List<int> { 349, 2 } },
                { "Box of Friends", new List<int> { 357, 3 } },
                { "Incubus", new List<int> { 360, 4 } },
                { "Dead Eye", new List<int> { 373, 3 } },
                { "Eden's Blessing", new List<int> { 381, 4 } },
                { "Tech X", new List<int> { 395, 4 } },
                { "Chaos", new List<int> { 402, 2 } },
                { "Spider Mod", new List<int> { 403, 1 } },
                { "Cambion Conception", new List<int> { 412, 2 } },
                { "Kidney Stone", new List<int> { 440, 1 } },
                { "Void", new List<int> { 477, 4 } },
                { "Technology Zero", new List<int> { 524, 3 } },
                { "120 Volt", new List<int> { 559, 2 } },
                { "Rock Bottom", new List<int> { 562, 4 } },
                { "Book of Virtues", new List<int> { 584, 4 } },
                { "Eternal D6", new List<int> { 609, 4 } },
                { "R Key", new List<int> { 636, 4 } },
                { "Guppy's Eye", new List<int> { 665, 3 } },
                { "C Section", new List<int> { 678, 4 } },
                { "Dark Arts", new List<int> { 705, 4 } },
                { "Abyss", new List<int> { 706, 4 } },
                { "Suplex!", new List<int> { 709, 1 } },
                { "Bag of Crafting", new List<int> { 710, 2 } },
                { "Flip", new List<int> { 711, 4 } },
                { "Lemegeton", new List<int> { 712, 3 } },
                { "Sumptorium", new List<int> { 713, 1 } },
                { "Hold", new List<int> { 715, 1 } },
                { "Keeper's Sack", new List<int> { 716, 3 } },
                { "Anima Sola", new List<int> { 722, 2 } }
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

    //static Lists

    //Name / Items
    static List<string> BossItemPool = new List<string>
    {
        "The Belt", "Wire Coat Hanger", "Lucky Foot", "Crickets's Body", "Soy Milk", "120 Volt", "Anima Sola", "Suplex!"
    };

    static List<string> TreasureRoomItemPool = new List<string>
    {
        "Cricket's Head", "The D6", "Technology Zero", "Dead Bird", "Bobby-Bomb", "20/20", "The Poop", "Yum Heart", "Razor Blade", "Dead Eye", "Polyphemus", "Anemic", "Proptosis"
    };

    static List<string> ShopItemPool = new List<string>
    {
        "Steam Sale", "Hold", "Money = Power", "Blank Card", "Keeper's Sack", "Wooden Nickel", "Bag of Crafting", "Spider Mod", "Box of Friends"
    };

    static List<string> CurseRoomItemPool = new List<string>
    {
        "Guppy's Eye", "Whore of Babylon", "Guppy's Paw", "Pandora's Box", "Dark Arts", "Guppy's Head", "Guppy's Hairball"
    };

    static List<string> SecretRoomItemPool = new List<string>
    {
        "1UP", "Tech X", "Chaos", "Sumptorium", "Rock Bottom", "Dead Cat", "R Key", "Death's Touch", "C Section", "Kidney Stone"
    };

    static List<string> AngelDealItemPool = new List<string>
    {
        "Guardian Angel", "Holy Mantel", "Eternal D6", "Godhead", "Eden's Blessing", "Void", "Book of Virtues"
    };

    static List<string> DevilDealItemPool = new List<string>
    {
        "The Book of Belial", "Abyss", "Mom's Knife", "Brimstone", "Flip", "Guppy's Tail", "Lemegeton", "Guppy's Colar", "Incubus", "Cambion Conception"
    };

    //Variables i need

    string SerialNumber;
    string ConvertedSerialNumber;

    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

    void Awake()
    { //Avoid doing calculations in here regarding edgework. Just use this for setting up buttons for simplicity.
        ModuleId = ModuleIdCounter++;
        GetComponent<KMBombModule>().OnActivate += Activate;
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */

        //button.OnInteract += delegate () { buttonPress(); return false; };

    }

    void OnDestroy()
    { //Shit you need to do when the bomb ends

    }

    void Activate()
    { //Shit that should happen when the bomb arrives (factory)/Lights turn on

        //Get Bomb Info
        SerialNumber = Bomb.GetSerialNumber();
        ConvertedSerialNumber = ConvertSerialNumber(SerialNumber);

    }

    void Start()
    { //Shit that you calculate, usually a majority if not all of the module

    }

    void Update()
    { //Shit that happens at any point after initialization

    }

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

    void Solve()
    {
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
