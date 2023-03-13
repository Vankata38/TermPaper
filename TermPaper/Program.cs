namespace TermPaper;

static class Program
{
    
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter command: ");
            
            string input = Console.ReadLine()!;
            string[] parsedInput = Helper.SplitLine(input, ' ');
            string body = parsedInput[1] + parsedInput[2];
            
            switch (parsedInput[0])
            {
                case "DEFINE":
                    Console.WriteLine("Syntax is {0}", Helper.IsValidInput(body));
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

