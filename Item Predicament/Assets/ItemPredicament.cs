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
    List<int> ButtonNeedsPress;
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
        CharacterDatabase.CharacterList["Eden"][0] = EdenPickups;
        CharacterDatabase.CharacterList["Tainted Eden"][0] = EdenPickups;

        EdenItem = GetEdenItem();
        CharacterDatabase.CharacterStartItemList["Eden"][0] = EdenItem;
        CharacterDatabase.CharacterStartItemList["Tainted Eden"][0] = EdenItem;

        //Set Highlights
        DeHighlight();

        //Chapter Name
        DisplayTexts[0].text = ChapterWithRoman = GetRandomChapterName();

        //Character Name
        DisplayTexts[1].text = Character = GetRandomCharacterName();
        if (Character == "Eden")
        {
            Debug.LogFormat("[Item Predicament #{0}] Eden pickup count: {1}\n Eden starting item: {2}", ModuleId, EdenPickups, EdenItem);
        }

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
        Debug.LogFormat("[Item Predicament #{0}] Your roman chapter number is {1}", ModuleId, Roman);
        FirstMatchInSerialNumber = GetFirstMatchInConvertedSerialNumber();
        if (FirstMatchInSerialNumber != 0)
            Debug.LogFormat("[Item Predicament #{0}] The first match in the converted serial number is at position {1}", ModuleId, FirstMatchInSerialNumber);
        else
            Debug.LogFormat("[Item Predicament #{0}] There is no matching position in the converted serial number", ModuleId);
        YourRoom = GetYourRoom();
        Debug.LogFormat("[Item Predicament #{0}] Your room is: {1}", ModuleId, YourRoom);

        //
        //Step 2
        //

        CharacterNumber = CalculateCharacterNumber();
        YourStats = GetYourStats();
        Debug.LogFormat("[Item Predicament #{0}] Your stats from inputting your calculated number into the table are: {1}",ModuleId, string.Join(", ", YourStats.Select(s => s.ToString()).ToArray()));

        //
        //Step 3
        //

        //Stats for our Items on the Buttons for ease of use
        ItemStats = new Dictionary<int, List<int>>
        {
            {0, ItemDatabase.ItemList[Items[0]].Stats },
            {1, ItemDatabase.ItemList[Items[1]].Stats },
            {2, ItemDatabase.ItemList[Items[2]].Stats },
            {3, ItemDatabase.ItemList[Items[3]].Stats },
        };

        ButtonNeedsPress = GetButtonsThatNeedsToBePressed();
        if (ButtonNeedsPress.Count == 1)
        {
            ButtonOrder = ButtonNeedsPress;
            Debug.LogFormat("[Item Predicament #{0}] No Order since there is only the button {1} that needs to be pressed", ModuleId, Items[ButtonOrder[0]].Replace("\n", " "));
        }
        else
        {
            ButtonOrder = GetButtonOrder();
            Debug.LogFormat("[Item Predicament #{0}] Your correct button order is: {1}", ModuleId, string.Join(", ", ButtonOrder.Select(s => Items[s].Replace("\n", " ")).ToArray()));
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
        return ChapterDatabase.ChapterList.Keys.ElementAt(Random.Range(0, ChapterDatabase.ChapterList.Count));
    }

    string GetRandomCharacterName()
    {
        return CharacterDatabase.CharacterList.Keys.ElementAt(Random.Range(0, CharacterDatabase.CharacterList.Count));
    }

    List<string> GetRandomItemNames()
    {
        List<string> keys = ItemDatabase.ItemList.Keys.ToList();
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
        Debug.LogFormat("[Item Predicament #{0}] Converted serial number: {1}", ModuleId, ConvertedSerialNumber.ToUpper());
        for (int i = 0; i < ConvertedSerialNumber.Length; i++)
        {
            string currentCharacter = ConvertedSerialNumber[i].ToString();
            if (Chapter.ContainsIgnoreCase(currentCharacter))
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
        int vowelsTimesConsonantsNumber = CharacterDatabase.CharacterList[Character][1];
        Debug.LogFormat("[Item Predicament #{0}] Your characters vowel and consonant product is {1}", ModuleId, vowelsTimesConsonantsNumber);

        if (BatteryCount != 0 && vowelsTimesConsonantsNumber % BatteryCount == 0)
        {
            Debug.LogFormat("[Item Predicament #{0}] The Product is divisible by the battery count ({1}). Adding it with the letter count of your room ({2}) results in {3}", ModuleId, BatteryCount, yourRoomCount, vowelsTimesConsonantsNumber + yourRoomCount);
            return vowelsTimesConsonantsNumber + yourRoomCount;
        }
        Debug.LogFormat("[Item Predicament #{0}] The Product is not divisible by the battery count ({1}). The difference between the product and the letter count of your room ({2}) results in {3}", ModuleId, BatteryCount, yourRoomCount, Mathf.Abs(vowelsTimesConsonantsNumber - yourRoomCount));
        return Mathf.Abs(vowelsTimesConsonantsNumber - yourRoomCount);
    }

    string ConvertCharacterNumber()
    {
        if (CharacterNumber <= 10)
            return "1";
        if (CharacterNumber <= 20)
            return "2";
        if (CharacterNumber < 30)
            return "3";
        if (CharacterNumber == 30)
            return "4";
        if (CharacterNumber >= 60)
            return "8";
        if (CharacterNumber >= 50)
            return "7";
        if (CharacterNumber >= 40)
            return "6";
        return "5";
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

    List<int> GetButtonsThatNeedsToBePressed()
    {
        List<int> result = new List<int>();
        List<string> startItem;
        List<bool> LogBools = new List<bool>();

        for (int i = 0; i < Items.Count; i++)
        {
            string itemName = Items[i];
            ItemData item = ItemDatabase.ItemList[itemName];
            List<int> stats = item.Stats;

            bool StatIncrease = stats.Any(stat => YourStats.Contains(Mathf.Abs(stat)) && stat > 0);
            bool pickupCountInID = item.ID.ToString().Contains(CharacterDatabase.CharacterList[Character][0].ToString());
            bool isDealItemAndInDealPool = (Stats[6] >= 70 || Stats[7] >= 70) && (ItemPoolDatabase.ItemPools["Devil Deal"].Contains(itemName) || ItemPoolDatabase.ItemPools["Angel Deal"].Contains(itemName));
            bool isStartItem = CharacterDatabase.CharacterStartItemList.TryGetValue(Character, out startItem) && startItem.Contains(itemName);
            LogBools = new List<bool> { StatIncrease, pickupCountInID, isDealItemAndInDealPool, isStartItem};
            if (StatIncrease || pickupCountInID || isDealItemAndInDealPool || isStartItem)
            {
                result.Add(i);
            }

            var trueRuleStrings = Enumerable.Range(0, LogBools.Count).Where(b => LogBools[b]).Select(b => (b + 1).ToString()).ToArray();

            if (trueRuleStrings.Length == 0)
                Debug.LogFormat("[Item Predicament #{0}] The Item {1}, doesn't apply to any rules",ModuleId, Items[i].Replace("\n", " "));
            else
                Debug.LogFormat("[Item Predicament #{0}] The Item {1}, applies to following rules: {2}",ModuleId, Items[i].Replace("\n", " "), string.Join(", ", trueRuleStrings));
        }

        if (result.Count() == 0)
        {
            result = new List<int> { 0, 1, 2, 3 };
            Debug.LogFormat("[Item Predicament #{0}] All buttons are necessary to be pressed", ModuleId);
        }
        else
            Debug.LogFormat("[Item Predicament #{0}] Buttons that are necessary to be pressed: {1}", ModuleId, string.Join(", ", result.Select(s => Items[s].Replace("\n", " ")).ToArray()));

        return result;

    }

    List<int> GetButtonOrder()
    {
        List<int> result = new List<int>();

        Dictionary<int, List<int>> CheckOrdering = new Dictionary<int, List<int>>
        {
            { 1, new List<int>() },
            { 2, new List<int>() },
            { 3, new List<int>() },
            { 4, new List<int>() },
            { 5, new List<int>() },
            { 6, new List<int>() }
        };

        foreach (int button in ButtonNeedsPress)
        {
            CheckOrdering[6].Add(button);
            if (ItemPoolDatabase.ItemPools[YourRoom].Contains(Items[button])) { CheckOrdering[1].Add(button); }
            if (ItemPoolDatabase.ItemPools["Devil Deal"].Contains(Items[button]) || ItemPoolDatabase.ItemPools["Angel Deal"].Contains(Items[button])) { CheckOrdering[2].Add(button); }
            foreach (int stat in ItemDatabase.ItemList[Items[button]].Stats)
            {
                int absStat = Mathf.Abs(stat);
                if (YourStats.Contains(absStat) && (int)Stats[absStat - 1] % 2 == 0 && stat != 0)
                {
                    CheckOrdering[3].Add(button);
                    break;
                }
            }
            if (ItemDatabase.ItemList[Items[button]].Quality >= 3) { CheckOrdering[4].Add(button); }
            if (ItemDatabase.ItemList[Items[button]].ID % 2 != 0 && ItemDatabase.ItemList[Items[button]].Quality != 2) { CheckOrdering[5].Add(button); }
        }

        foreach (List<int> sortingList in CheckOrdering.Values)
        {
            sortingList.OrderByDescending(x => ItemDatabase.ItemList[Items[x]].ID).ToList();
        }
        CheckOrdering[6].OrderBy(x => ItemDatabase.ItemList[Items[x]].Quality).ToList();

        for (int i = 1; i < CheckOrdering.Count - 1; i++)
        {
            if (CheckOrdering[i].Count != 0 && !result.Contains(CheckOrdering[i].First()))
            {
                result.Add(CheckOrdering[i].First());
            }
        }

        for (int i = 0; i < CheckOrdering[6].Count; i++)
        {
            if (CheckOrdering[6].Count != 0 && !result.Contains(CheckOrdering[6][i]))
            {
                result.Add(CheckOrdering[6][i]);
            }
        }

        return result;
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

        var sortedItemDictonary = ItemDatabase.ItemList.OrderBy(kvp => kvp.Value.ID)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        foreach (var kvp in sortedItemDictonary)
        {
            if (kvp.Value.ID.ToString().Contains(lastNumber))
            {
                return kvp.Key;
            }
        }
        return "None";
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

                if (i == ButtonOrder.First())
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
    private readonly string TwitchHelpMessage = @"Use !{0} '1/2/3/4' for Input and 'Hover' for the item stats read upon hovering over a button. Seperate chained commands with spaces";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.Trim().ToUpper();
        yield return null;
        string[] commands = command.Split(' ');
        for (int i = 0; i < commands.Length; i++)
        {
            if (commands[0] == "HOVER" && commands.Count() == 1)
            {
                for (int index = 0; index < Buttons.Length; index++)
                {
                    Buttons[index].OnHighlight();
                    Buttons[index].gameObject.GetComponent<Renderer>().material.color = new Color32(70, 70, 70, 255);
                    yield return new WaitForSeconds(2f);
                    Buttons[index].OnHighlightEnded();
                    Buttons[index].gameObject.GetComponent<Renderer>().material.color = new Color32(30, 30, 30, 255);
                }
                yield break;
            }
            if ("1234".Contains(commands[i][0]))
            {
                for (int index = 0; index < commands.Length; index++)
                {
                    Buttons[int.Parse(commands[index]) - 1].OnInteract();
                    yield return new WaitForSeconds(.1f);
                }
                yield break;
            }
            yield return "sendtochaterror This is not a valid command.";
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
    }
    
}

public class ItemData
{
    public int ID;
    public int Quality;
    public List<int> Stats;
}