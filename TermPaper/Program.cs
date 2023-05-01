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
                    Console.WriteLine(Validator.IsValidInput(input, 'd', out string funcName, out string exp));
                    
                    // Handle notation
                    Tree functionTree = new Tree();
                    if (!Validator.IsPostfix(exp))
                        exp = Helper.ConvertToPostfix(exp);
                    else
                        exp = Helper.RemoveChar(exp, ' ');

                    // TODO - Remove debug
                    Console.WriteLine(exp);
                    
                    // Build the tree
                    functionTree.BuildTree(exp);
                    functionTree.PrintTree();

                    // Save into the hashmap
                    map.Add(funcName, functionTree);

                    if (map.Get(funcName) != null)
                        map.Get(funcName)?.PrintTree();
                    
                    break;
                case "SOLVE":
                    Console.WriteLine(Validator.IsValidInput(input, 's', out funcName, out exp));
                    
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

