using TermPaper.Data_Structures;
using TermPaper.FunctionHandler;

namespace TermPaper;

static class Program
{
    
    static void Main(string[] args)
    {
        // Test the hashmap
        var tree = new Data_Structures.Tree(new Tree.TreeNode('c'));
        
        while (true)
        {
            Console.WriteLine("Enter command: ");
            string input = Console.ReadLine()!;
            string command = Helper.SplitOne(input, ' ');

            switch (command)
            {
                case "DEFINE":
                    
                    // TODO - Remove debug check validity of function name
                    Console.WriteLine(Validator.IsValidInput(input, 'd',  out string exp));

                    // TODO Use the tree
                    // Build the tree
                    if (!Define.IsPostfix(exp))
                        Define.ConvertToPostfix();
                    Define.BuildTree(exp);
                    
                    break;
                case "SOLVE":
                    Console.WriteLine(Validator.IsValidInput(input, 's',  out exp));
                    
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

