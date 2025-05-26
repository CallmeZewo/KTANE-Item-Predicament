using System.Collections.Generic;

public static class CharacterDatabase
{
    public static readonly Dictionary<string, List<int>> CharacterList = new Dictionary<string, List<int>>
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

    //Name / Start Item
    public static readonly Dictionary<string, List<string>> CharacterStartItemList = new Dictionary<string, List<string>>
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
}
