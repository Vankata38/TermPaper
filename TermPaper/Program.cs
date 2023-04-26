namespace TermPaper;

static class Program
{
    
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter command: ");
            string input = Console.ReadLine()!;
            string command = Helper.SplitOne(input, ' ');

            switch (command)
            {
                case "DEFINE":

                    
                    
                    // Check validity of function name
                    Console.WriteLine(Helper.IsValidInput(input));

                    // Build the tree
                    
                    break;
                case "SOLVE":
                    Console.WriteLine("SOLVE");
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

