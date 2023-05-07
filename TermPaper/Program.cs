using TermPaper.Data_Structures;
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

    static void Main(string[] args)
    {
        Hashmap map = new Hashmap();
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

            if (input[0] == ' ')
                input = Helper.SubString(Helper.FindFirstChar(input), input); 
            string command = Helper.Split(input, ' ', 1)[0];

            switch (command)
            {
                case "DEFINE":
                    
                    // TODO - Remove debug check validity of function name
                    FunctionHandler.Define(input, map);
                    
                    break;
                case "SOLVE":
                    FunctionHandler.Solve(input, map);

                    break;
                case "ALL":
                    Console.WriteLine("ALL");
                    break;
                case "FIND":
                    Console.WriteLine("FIND");
                    break;
                case "EXIT":
                    Console.WriteLine("Have a nice day!");
                    return;
                default:
                    Console.WriteLine("Invalid command! Please enter with all capital letters.");
                    break;
            }
        }
    }
}

