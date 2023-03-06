namespace TermPaper;
class Program
{
    static string parseCommand(string input)
    {
        string command = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == ' ')
            {
                break;
            }
            command += input[i];
        }
        
        command = Helper.ToUpperCase(command);
        return command;
    }
    
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter command: ");
            
            string input = Console.ReadLine();
            string command = parseCommand(input);
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

