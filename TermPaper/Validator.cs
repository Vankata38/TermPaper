namespace TermPaper;

public class Validator
{
    // TODO: Make this function take func format, so we can validate d, s, f separately
    public static bool IsValidInput(string input, char mode, out string exp)
    {
        // TODO: We have to check the definition for functions that are already defined
        
        // Get the name, arguments and definition of the function
        string name = Helper.Extract(' ', '(', input);
        string args = Helper.Extract('(', ':', input);
        exp = Helper.Extract('"', '"', input);
        
        // Handle empty input
        if ((name == "" || args == "") || (mode == 'd' && exp == ""))
            return false;
        
        // Remove the ')' in args and add ' ' to exp so the foreach can check the last character
        if (mode != 'f')
            args = args.Remove(args.Length - 1, 1);
        exp = exp + " ";

        // TODO: Remove debug statements
        Console.WriteLine($"\nName: {name}");
        Console.WriteLine($"Args: {args}");
        Console.WriteLine($"Def: {exp}\n");

        // Extract the arguments of the function and error if 0
        string[] vars = GetArguments(args, out bool valid, mode);
        if (!valid)
            return false;

        // Validate the brackets and make sure we only have 2 (' " ') parentheses
        if (!IsValidBrackets(input, mode))
            return false;
        
        // Validate the expression if in definition mode
        if (!IsValidExpression(exp, vars))
            return false;
        
        return true;
    }

    // Returns the arguments of the function
    private static string[] GetArguments(string args, out bool valid, char mode)
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
                if (!(c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z') && !(c >= '0' && c <= '9'))
                    valid = false;
                
                // If we have a num outside of a variable, it's invalid
                if (!inVariable && c >= '0' && c <= '9' && mode != 's')
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
    private static bool IsValidBrackets(string input, char mode)
    {
        bool inBrackets = false;
        int bracketsCount = 0;
        int parenthesesCount = 2;
        
        foreach (char c in input)
        {
            if (c == '"')
                parenthesesCount--;
            
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

        if (mode == 'd' && parenthesesCount != 0)
            return false;
        
        return bracketsCount == 0;
    }
    
    private static bool IsValidExpression(string exp, string[] variables)
    {
        // Our expression mustn't have variables that arent in the arguments
        // Our expression must only have operations: &, |, ! and priority brackets: (, )

        bool inVariable = false;
        string variable = "";
        foreach (char c in exp)
        {
            if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || (c >= '0' && c <= '9'))
            {
                if ((c >= '0' && c <= '9') && !inVariable)
                    return false;
                
                inVariable = true;
                variable += c;
            } else if (c == '&' || c == '|' || c == '!' || c == '(' || c == ')' || c == ' ' || c == ',')
            {
                if (inVariable)
                {
                    // We are in a func like func2 or we have priority brackets
                    if (c == '(')
                    {
                        inVariable = false;
                        variable = "";
                        
                        continue;
                    }
                    
                    if (!Helper.Contains(variables, variable))
                        return false;
                    
                    variable = "";
                    inVariable = false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}