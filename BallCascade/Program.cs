using BallCascade;
using System.CommandLine;

var rootCommand = new RootCommand("Ball Cascade");
var depthOption = new Option<int?>(name: "--depth", description: "system depth");
rootCommand.AddOption(depthOption);
rootCommand.SetHandler((depth) =>
{
    if (depth == null)
    {
        string val;
        Console.Write("Enter system depth: ");
        val = Console.ReadLine();
        if (!int.TryParse(val, out int parsedDepth))
        {
            Console.WriteLine("Please provide a whole number");
            return;
        }

        depth = parsedDepth;
    }

    if (depth == 0)
    {
        Console.WriteLine("Please provide a depth greater than 0");
        return;
    }

    BallCascadeProcess.CascadeForDepth(depth!.Value);
}, depthOption);
Console.WriteLine("Welcome to the Ball Cascade!");

rootCommand.Invoke(args);



