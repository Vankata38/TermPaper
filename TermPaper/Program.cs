using TermPaper.Data_Structures;
namespace TermPaper;

static class Program
{
    private static string[] inputs = new string[]
    {
        "DEFINE func1(a, b): \"a & b\"",
        "DEFINE func2(a, b, c): \"func1(a, b) | c\"",
        "DEFINE func3(a, b, c, d): \"a & (b | c) & !d\"",
    };
    
    static void Main(string[] args)
    {
        Hashmap map = new Hashmap();
        int testIndex = 0;
        
        while (true)
        {
            string input;
            if (testIndex < inputs.Length)
            {
                input = inputs[testIndex];
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
                    Console.WriteLine(Validator.IsValidInput(input, 'd', map, out string funcName, out int argumentsCount, out string expression));
                    
                    if (!Validator.IsValidInput(input, 'd', map, out funcName, out argumentsCount, out expression))
                        break;
                    
                    // Handle notation
                    Tree newTree = new Tree();

                    // TODO - Remove debug
                    Console.WriteLine(expression);
                    
                    // Build the tree
                    newTree.BuildTree(expression);
                    newTree.PrintTree();

                    // Save into the hashmap
                    map.Add(funcName, argumentsCount, newTree);
                    map.Get("func1")?.PrintTree();
                    
                    break;
                case "SOLVE":
                    Console.WriteLine(Validator.IsValidInput(input, 's', map, out funcName, out argumentsCount, out expression));
                    
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

