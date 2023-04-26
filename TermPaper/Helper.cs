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

    public static string SplitOne(string input, char separator)
    {
        string result = "";
        int i = 0;

        while (input[i] != separator)
        {
            result += input[i];
            i++;

            if (i == input.Length)
                return "";
        }
        
        return result;
    }
    
    // TODO - Create a function to check if the input is valid.
    public static bool IsValidInput(string input)
    {
        // TODO: We have to check the definition for functions that are already defined, maybe look before the brace
        // TODO: Try to check formatting, ATM it may fail to recognise funX(a, (b), c) as invalid
        
        // Get the name, arguments and definition of the function
        string name = Extract(' ', '(', input);
        string args = Extract('(', ')', input);
        string exp = Extract('"', '"', input);

        // TODO: Remove debug statements
        Console.WriteLine($"\nName: {name}");
        Console.WriteLine($"Args: {args}");
        Console.WriteLine($"Def: {exp}\n");
        
        // Handle empty input
        if (name == "" || args == "")
            return false;

        // Extract the arguments of the function and error if 0
        string[] vars = GetArguments(args, out bool valid);
        if (!valid)
            return false;

        // Validate the brackets
        if (!IsValidBrackets(input))
            return false;
        
        // TODO: Validate the expression
        if (!IsValidExpression(exp, vars))
            return false;
        
        return true;
    }

    // Returns the arguments of the function
    private static string[] GetArguments(string args, out bool valid)
    {
        List<string> variables = new List<string>();
        bool inVariable = false;
        string temp = "";
        valid = true;
        
        foreach (char c in args)
        {
            if (c != ' ' && c != ',')
            {
                // Check if the character is a valid 
                if (!(c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z'))
                    valid = false;
                
                inVariable = true;
                temp += c;
            }
            else
            {
                if (!inVariable)
                    continue;
                
                variables.Add(temp);
                temp = "";
                inVariable = false;
            }
        }
        if (inVariable)
            variables.Add(temp);
        
        if (variables.Count == 0)
            valid = false;

        return variables.ToArray();
    }
    
    // Checks the number of brackets and if there are flipped brackets (first closing then opening)
    private static bool IsValidBrackets(string input)
    {
        bool inBrackets = false;
        int bracketsCount = 0;
        
        foreach (char c in input)
        {
            if (c == '(')
            {
                inBrackets = true;
                bracketsCount++;
            }
            else if (c == ')')
            {
                if (!inBrackets)
                    return false;

                inBrackets = false;
                bracketsCount--;
            }
        }
        
        return bracketsCount == 0;
    }
    
    private static bool IsValidExpression(string exp, string[] variables)
    {
        // TODO: Check if the expression is valid
        

        return false;
    }
}