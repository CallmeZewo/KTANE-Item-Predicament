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

    public KMSelectable[] Buttons;
    public TextMesh[] DisplayTexts;

    private Dictionary<string, List<int>> ItemList;

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

        ItemList = new Dictionary<string, List<int>>
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


    }

    void Start()
    { //Shit that you calculate, usually a majority if not all of the module

    }

    void Update()
    { //Shit that happens at any point after initialization

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
