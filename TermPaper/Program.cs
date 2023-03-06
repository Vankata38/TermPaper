namespace TermPaper;

static class Program
{
    static string ParseCommand(string input)
    {
        string command = "";
        foreach (char c in input)
        {
            if (c == ' ')
            {
                break;
            }
            command += c;
        }
        
        command = Helper.ToUpperCase(command);
        return command;
    }
    
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter command: ");
            
            string input = Console.ReadLine()!;
            string command = ParseCommand(input);
            string parameters = Helper.Substring(input, command.Length + 1);

            switch (command)
            {
                case "DEFINE":
                    Console.WriteLine("DEFINE");
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
                    return;
            }
        }
    }
}

