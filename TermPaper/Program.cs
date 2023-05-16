using TermPaper.Data_Structures;
using TermPaper.Handlers;

namespace TermPaper;

static class Program
{
    private static readonly string[] TestInputs =
    {
        "DEFINE func1(a, b): \"a & b\"",
        "DEFINE func2(a, b, c): \"func1(a, b) | c\"",
        "DEFINE func3(a, b, c, d): \"a & (b | c) & !d\"",
        "DEFINE func5(a, b, c, d): \"func1(a, b) & func2(a, b, c) & func3(a, b, c, d)\""
    };

    static void Main()
    {
        Helper helper = new Helper();
        FileHandler fileHandler = new FileHandler();
        FunctionHandler functionHandler = new FunctionHandler();
        Hashmap map = fileHandler.LoadFromFile();
        int testIndex = 0;

        while (true)
        {
            string input;
            if (testIndex < TestInputs.Length)
            {
                input = TestInputs[testIndex];
                testIndex++;
            }
            else
            {
                Console.WriteLine("Enter command: ");
                input = Console.ReadLine()!;
            }

            if (input != "" && input[0] == ' ')
                input = helper.SubString(helper.FindFirstChar(input), input); 
            string command = helper.Split(input, ' ', 1)[0];

            switch (command)
            {
                case "DEFINE":
                    functionHandler.Define(input, map);
                    fileHandler.Save(map);

                    break;
                case "SOLVE":
                    functionHandler.Solve(input, map);

                    break;
                case "ALL":
                    functionHandler.All(input, map);

                    break;
                case "FIND":
                    fileHandler.Save(map);
                    Console.WriteLine("FIND");

                    break;
                case "EXIT":
                    fileHandler.Save(map);
                    Console.WriteLine("Have a nice day!");
                    
                    return;
                default:
                    Console.WriteLine("Invalid command! Please enter with all capital letters.");
                    break;
            }
        }
    }
}