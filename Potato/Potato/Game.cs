using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Potato;
internal class Game
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Score> Scores { get; set; }
    public RemoveOrcAction RemoveOrcAction { get; set; }

    public Game()
    {
        Title = "\t  _____      _        _        " +
                "\r\n\t |  __ \\    | |      | |       " +
                "\r\n\t | |__) |__ | |_ __ _| |_ ___  " +
                "\r\n\t |  ___/ _ \\| __/ _` | __/ _ \\ " +
                "\r\n\t | |  | (_) | || (_| | || (_) |" +
                "\r\n\t |_|   \\___/ \\__\\__,_|\\__\\___/ " +
                "\r\n";
        Description = "You are a halfling, just trying to exist. Meanwhile, the dark lord rampages across the world. \r\n" +
            "You do not care about this. You are trying to farm potatoes because what could a halfling possibly do about it anyway.";
        Scores = ScoreBuilder();
        RemoveOrcAction = RemoveOrcActionBuilder();
    }
    public void LoadGameTitle()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(Title);
        Console.WriteLine(Description);
        DisplayScores();
        Console.ResetColor();
    }

    public void GrassAndMud()
    {
        if (CanAffordToRemoveOrc())
        {
            LoadGameTitle();
        }

        Console.WriteLine();
        var diceVal = RollDice();

        switch (diceVal)
        {
            case 1 or 2:
                InTheGarden();
                Continue();
                break;
            case 3 or 4:
                AKnockAtTheDoor();
                Continue();
                break;
            case 5 or 6:
                OrcCostIncrease();
                Continue();
                break;
        }
    }

    public bool CheckEndGame()
    {
        var won = Scores.Any(s => s.CurrentValue == s.EndValue);

        if (!won)
        {
            return won;
        }
        LoadGameTitle();
        Score ending = new Score();
        var endings = Scores.Where(s => s.CurrentValue == s.EndValue).ToList();
        if (endings.Count() == 1)
        {
            ending = endings.First();
        }
        else if (endings.Count() > 1)
        {
            var rng = RandomNumberGenerator.GetInt32(0, endings.Count() - 1);
            ending = endings[rng];
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(ending.Ending);
        Console.ResetColor();

        return won;
    }

    public void DisplayScores()
    {
        Scores.ForEach(s => Console.WriteLine($"{s.Name}: {s.CurrentValue}"));
        var potatoWording = RemoveOrcAction.CostOfAction > 1 ? "potatoes" : "potato";
        Console.WriteLine($"Action \"Remove Orc\" costs: {RemoveOrcAction.CostOfAction} {potatoWording}");
    }

    private void InTheGarden()
    {
        Console.WriteLine("In the garden...");
        var diceVal = RollDice();

        switch (diceVal)
        {
            case 1:
                Console.Write("You happily root about all day in your garden.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+1 Potato");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.AddBy(1);
                break;
            case 2:
                Console.Write("You narrowly avoid a visitor by hiding in a potato sack.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+1 Destiny");
                Console.Write("\t+1 Potato");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(DestinyScore))?.AddBy(1);
                Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.AddBy(1);
                break;
            case 3:
                Console.Write("A hooded stranger lingers outside your farm.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+1 Orc");
                Console.Write("\t+1 Destiny");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(OrcScore))?.AddBy(1);
                Scores.SingleOrDefault(s => s.GetType() == typeof(DestinyScore))?.AddBy(1);
                break;
            case 4:
                Console.Write("Your field is ravaged in the night by unseen enemies.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t-1 Potato");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+1 Orc");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(OrcScore))?.AddBy(1);
                Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.RemoveBy(1);
                break;
            case 5:
                Console.Write("You trade potatoes for other delicious food stuffs.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t-1 Potato");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.RemoveBy(1);
                break;
            case 6:
                Console.Write("You burrow into a bumper crop of potatoes. Do you cry with joy? Possibly.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+2 Potatoes");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.AddBy(2);
                break;
        }
    }

    private void AKnockAtTheDoor()
    {
        Console.WriteLine("There's a knock at the door...");
        var diceVal = RollDice();

        switch (diceVal)
        {
            case 1:
                Console.Write("A distant cousin. They are after your potatoes. They may snitch on you.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+1 Orc");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(OrcScore))?.AddBy(1);
                break;
            case 2:
                Console.Write("A dwarven stranger. You refuse them entry. Ghastly creatures.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+1 Destiny");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(DestinyScore))?.AddBy(1);
                break;
            case 3:
                Console.Write("A wizard strolls by. You pointedly draw the curtains.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+1 Orc");
                Console.Write("\t+1 Destiny");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(OrcScore))?.AddBy(1);
                Scores.SingleOrDefault(s => s.GetType() == typeof(DestinyScore))?.AddBy(1);
                break;
            case 4:
                Console.Write("There are rumours of war in the reaches. You eat some potatoes.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t-1 Potato");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+2 Orc");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(OrcScore))?.AddBy(2);
                Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.RemoveBy(1);
                break;
            case 5:
                Console.Write("It's an elf. They are not serious people.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+1 Destiny");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(DestinyScore))?.AddBy(1);
                break;
            case 6:
                Console.Write("It's a sack of potatoes from a generous neighbour. You really must remember to pay them a visit one of these years.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t+2 Potatoes");
                Console.ResetColor();
                Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.AddBy(2);
                break;
        }
    }

    private void OrcCostIncrease()
    {
        Console.Write("The world becomes a darker, more dangerous place. From now on, removing an Orc costs an additional Potato.");
        RemoveOrcAction.IncreaseCostBy(1);
    }

    private bool CanAffordToRemoveOrc()
    {
        var potatoScore = Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.CurrentValue;
        var orcScore = Scores.SingleOrDefault(s => s.GetType() == typeof(OrcScore))?.CurrentValue;

        var canAffordToRemoveOrc = (potatoScore >= RemoveOrcAction.CostOfAction && orcScore != 0);

        if (canAffordToRemoveOrc)
        {
            Console.WriteLine(RemoveOrcAction.Description);
            Console.WriteLine("Would you like to do so? (y/n)");
            while (true)
            {
                var input = Console.ReadKey(true).KeyChar.ToString().ToLower();
                if (input == "y")
                {
                    RemoveOrc();
                    break;
                }
                else if (input == "n")
                {
                    break;
                }
            }
        }

        return canAffordToRemoveOrc;
    }

    private void RemoveOrc()
    {
        Scores.SingleOrDefault(s => s.GetType() == typeof(OrcScore))?.RemoveBy(1);
        Scores.SingleOrDefault(s => s.GetType() == typeof(PotatoScore))?.RemoveBy(RemoveOrcAction.CostOfAction);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\r\nYou lure away an orc by throwing potatoes to cause distractions.");
        Console.ResetColor();
        Continue();
    }

    private int RollDice()
    {
        var val = RandomNumberGenerator.GetInt32(1, 6);

        Console.ResetColor();
        Console.WriteLine("Roll your dice! (Press any key to roll the dice)");
        Console.ReadKey();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"You roll a d6 and get {val}!");

        return val;
    }

    private void Continue()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\r\n(Press any key to continue!)");
        Console.ReadKey();
    }

    private List<Score> ScoreBuilder()
    {
        var scores = new List<Score> {
            new DestinyScore(),
            new PotatoScore(),
            new OrcScore()
        };

        return scores;
    }

    private RemoveOrcAction RemoveOrcActionBuilder()
    {
        var removeOrcAction = new RemoveOrcAction();
        return removeOrcAction;
    }
}
