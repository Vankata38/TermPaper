using TermPaper.Data_Structures;
namespace TermPaper;

static class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Hashmap map = new Hashmap();

            Console.WriteLine("Enter command: ");
            string input = Console.ReadLine()!;
            string command = Helper.Split(input, ' ', 1)[0];

            switch (command)
            {
                case "DEFINE":
                    
                    // TODO - Remove debug check validity of function name
                    Console.WriteLine(Validator.IsValidInput(input, 'd', out string funcName, out int argumentsCount, out string expression));
                    
                    // Handle notation
                    Tree functionTree = new Tree();
                    if (!Validator.IsPostfix(expression))
                        expression = Helper.ConvertToPostfix(expression);
                    else
                        expression = Helper.RemoveChar(expression, ' ');

                    // TODO - Remove debug
                    Console.WriteLine(expression);
                    
                    // Build the tree
                    functionTree.BuildTree(expression);
                    functionTree.PrintTree();

                    // Save into the hashmap
                    map.Add(funcName, argumentsCount, functionTree);

                    if (map.Get(funcName) != null)
                        map.Get(funcName)?.PrintTree();
                    
                    break;
                case "SOLVE":
                    Console.WriteLine(Validator.IsValidInput(input, 's', out funcName, out argumentsCount, out expression));
                    
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

