using System.Collections.Generic;

public static class ItemPoolDatabase
{
    //Name / Item
    public static readonly Dictionary<string, List<string>> ItemPools = new Dictionary<string, List<string>>
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
}
