using System.Collections.Generic;

public static class ItemDatabase
{
    //Name / ID / Quality / Stats effected
    public static readonly Dictionary<string, ItemData> ItemList = new Dictionary<string, ItemData>
    {
        // Quality 0
        { "Dead Bird", new ItemData { ID = 117, Quality = 0, Stats = new List<int> { 0 } } },
        { "Hold", new ItemData { ID = 715, Quality = 0, Stats = new List<int> { 0 } } },
        { "Razor Blade", new ItemData { ID = 126, Quality = 0, Stats = new List<int> { 3 } } },
        { "The Poop", new ItemData { ID = 36, Quality = 0, Stats = new List<int> { 0 } } },

        // Quality 1
        { "Anemic", new ItemData { ID = 214, Quality = 1, Stats = new List<int> { 4 } } },
        { "Box of\nFriends", new ItemData { ID = 357, Quality = 1, Stats = new List<int> { 0 } } },
        { "Guppy's\nCollar", new ItemData { ID = 212, Quality = 1, Stats = new List<int> { 0 } } },
        { "Guppy's\nHairball", new ItemData { ID = 187, Quality = 1, Stats = new List<int> { 0 } } },
        { "Spider Mod", new ItemData { ID = 403, Quality = 1, Stats = new List<int> { 0 } } },
        { "The Belt", new ItemData { ID = 28, Quality = 1, Stats = new List<int> { 1 } } },
        { "Wooden\nNickel", new ItemData { ID = 349, Quality = 1, Stats = new List<int> { 0 } } },
        { "Yum Heart", new ItemData { ID = 45, Quality = 1, Stats = new List<int> { 0 } } },

        // Quality 2
        { "1UP", new ItemData { ID = 11, Quality = 2, Stats = new List<int> { 0 } } },
        { "120 Volt", new ItemData { ID = 559, Quality = 2, Stats = new List<int> { 0 } } },
        { "Bobby-Bomb", new ItemData { ID = 125, Quality = 2, Stats = new List<int> { 0 } } },
        { "Blank Card", new ItemData { ID = 286, Quality = 2, Stats = new List<int> { 0 } } },
        { "Cambion\nConception", new ItemData { ID = 412, Quality = 2, Stats = new List<int> { 0 } } },
        { "Dark Arts", new ItemData { ID = 705, Quality = 2, Stats = new List<int> { 1 } } },
        { "Guardion\nAngel", new ItemData { ID = 112, Quality = 2, Stats = new List<int> { 0 } } },
        { "Guppy's Eye", new ItemData { ID = 665, Quality = 2, Stats = new List<int> { 0 } } },
        { "Guppy's Head", new ItemData { ID = 145, Quality = 2, Stats = new List<int> { 0 } } },
        { "Guppy's Tail", new ItemData { ID = 134, Quality = 2, Stats = new List<int> { 0 } } },
        { "Kidney Stone", new ItemData { ID = 440, Quality = 2, Stats = new List<int> { 2 } } },
        { "Lucky Foot", new ItemData { ID = 46, Quality = 2, Stats = new List<int> { 6 } } },
        { "Money\n=\nPower", new ItemData { ID = 109, Quality = 2, Stats = new List<int> { 3 } } },
        { "Pandora's\nBox", new ItemData { ID = 297, Quality = 2, Stats = new List<int> { 0 } } },
        { "Soy Milk", new ItemData { ID = 330, Quality = 2, Stats = new List<int> { 2, -3 } } },
        { "Steam Sale", new ItemData { ID = 64, Quality = 2, Stats = new List<int> { 0 } } },
        { "Suplex!", new ItemData { ID = 709, Quality = 2, Stats = new List<int> { 0 } } },
        { "The Book\nof Belial", new ItemData { ID = 34, Quality = 2, Stats = new List<int> { 3 } } },
        { "Whore of\nBabylon", new ItemData { ID = 122, Quality = 2, Stats = new List<int> { 1, 3 } } },

        // Quality 3
        { "Anima Sola", new ItemData { ID = 722, Quality = 3, Stats = new List<int> { 0 } } },
        { "Bag of\nCrafting", new ItemData { ID = 710, Quality = 3, Stats = new List<int> { 0 } } },
        { "Book of\nVirtues", new ItemData { ID = 584, Quality = 3, Stats = new List<int> { 0 } } },
        { "Chaos", new ItemData { ID = 402, Quality = 3, Stats = new List<int> { 0 } } },
        { "Cricket's\nBody", new ItemData { ID = 224, Quality = 3, Stats = new List<int> { 2, -4 } } },
        { "Cricket's\nHead", new ItemData { ID = 4, Quality = 3, Stats = new List<int> { 3 } } },
        { "Dead Cat", new ItemData { ID = 81, Quality = 3, Stats = new List<int> { 0 } } },
        { "Dead Eye", new ItemData { ID = 373, Quality = 3, Stats = new List<int> { 3 } } },
        { "Death's\nTouch", new ItemData { ID = 237, Quality = 3, Stats = new List<int> { 3, -2 } } },
        { "Eden's\nBlessing", new ItemData { ID = 381, Quality = 3, Stats = new List<int> { 2 } } },
        { "Eternal D6", new ItemData { ID = 609, Quality = 3, Stats = new List<int> { 0 } } },
        { "Guppy's Paw", new ItemData { ID = 133, Quality = 3, Stats = new List<int> { 0 } } },
        { "Keeper's\nSack", new ItemData { ID = 716, Quality = 2, Stats = new List<int> { 0 } } },
        { "Lemegeton", new ItemData { ID = 712, Quality = 3, Stats = new List<int> { 0 } } },
        { "Proptosis", new ItemData { ID = 261, Quality = 3, Stats = new List<int> { 3 } } },
        { "Rock Bottom", new ItemData { ID = 562, Quality = 3, Stats = new List<int> { 0 } } },
        { "Sumptorium", new ItemData { ID = 713, Quality = 3, Stats = new List<int> { 0 } } },
        { "Technology\nZero", new ItemData { ID = 524, Quality = 3, Stats = new List<int> { 0 } } },
        { "Wire Coat\nHanger", new ItemData { ID = 32, Quality = 3, Stats = new List<int> { 2 } } },

        // Quality 4
        { "20/20", new ItemData { ID = 245, Quality = 4, Stats = new List<int> { -3 } } },
        { "Abyss", new ItemData { ID = 706, Quality = 4, Stats = new List<int> { 0 } } },
        { "Brimstone", new ItemData { ID = 118, Quality = 4, Stats = new List<int> { -2 } } },
        { "C Section", new ItemData { ID = 678, Quality = 4, Stats = new List<int> { 0 } } },
        { "Flip", new ItemData { ID = 711, Quality = 4, Stats = new List<int> { 0 } } },
        { "Godhead", new ItemData { ID = 331, Quality = 4, Stats = new List<int> { 3, -2, -5 } } },
        { "Holy Mantle", new ItemData { ID = 313, Quality = 4, Stats = new List<int> { 0 } } },
        { "Incubus", new ItemData { ID = 360, Quality = 4, Stats = new List<int> { 0 } } },
        { "Mom's Knife", new ItemData { ID = 114, Quality = 4, Stats = new List<int> { 0 } } },
        { "Polyphemus", new ItemData { ID = 169, Quality = 4, Stats = new List<int> { 3, -2 } } },
        { "R Key", new ItemData { ID = 636, Quality = 4, Stats = new List<int> { 0 } } },
        { "Tech X", new ItemData { ID = 395, Quality = 4, Stats = new List<int> { 0 } } },
        { "The D6", new ItemData { ID = 105, Quality = 4, Stats = new List<int> { 0 } } },
        { "Void", new ItemData { ID = 477, Quality = 4, Stats = new List<int> { 0 } } }
    };
}
