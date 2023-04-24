namespace TermPaper;

public static class Helper
{
    public static string Extract(char start, char end, string input)
    {
        string result = "";
        
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == start)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[j] == end)
                        return result;
                    else
                        result += input[j];
                }
            }
        }

        return result;
    }

    public static void HandleInput(string input, out string name, out string args, out string def)
    {
        name = Extract(' ', '(', input);
        args = Extract('(', ')', input);
        def = Extract('"', '"', input);
        
        if (name == "")
            throw new Exception("Function has no name!");
        if (args == "")
            throw new Exception("Function has no arguments!");
    }
    
    public static string[] Split(string input, char separator)
    {
        int count = 0;
        foreach (char c in input)
        {
            if (c == separator)
            {
                count++;
            }
        }
        
        string[] result = new string[count + 1];
        int index = 0;
        string temp = "";
        foreach (char c in input)
        {
            if (c == separator)
            {
                result[index] = temp;
                temp = "";
                index++;
            }
            else
            {
                temp += c;
            }
        }

        result[index] = temp;
        
        return result;
    }
    
    // TODO - Create a function to check if the input is valid.
    public static bool IsValidInput(string args, string def)
    {
        return false;
    }
}