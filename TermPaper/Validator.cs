namespace TermPaper;

public class Validator
{
    // TODO: Make this function take func format, so we can validate d, s, f separately
    public static bool IsValidInput(string input, char mode, out string name, out int argumentsCount, out string expression)
    {
        // TODO: We have to check the definition for functions that are already defined
        
        // Get the name, arguments and definition of the function
        name = Helper.Extract(' ', '(', input);
        string args = Helper.Extract('(', ':', input);
        expression = Helper.Extract('"', '"', input);
        argumentsCount = 0;
        
        // Handle empty input
        if ((name == "" || args == "") || (mode == 'd' && expression == ""))
            return false;
        
        // Remove the ')' in args and add ' ' to exp so the foreach can check the last character
        if (mode != 'f')
            args = args.Remove(args.Length - 1, 1);
        expression += " ";

        // TODO: Remove debug statements
        Console.WriteLine("\nDEBUG: ");
        Console.WriteLine($"Input: {input}");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Args: {args}");
        Console.WriteLine($"Def: {expression}");

        // Extract the arguments of the function and error if 0
        string[] arguments = GetArguments(args, out bool valid, mode);
        argumentsCount = arguments.Length;
        if (!valid)
            return false;

        // Validate the brackets and make sure we only have 2 (' " ') parentheses
        if (!IsValidBrackets(input, mode))
            return false;
        
        // Validate the expression if in definition mode
        if (!IsValidExpression(expression, arguments))
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
    
    // TODO Fix postfix validation
    private static bool IsValidExpression(string exp, string[] variables)
    {
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
    
    public static bool IsPostfix(string expression)
    {
        // We need to handle funcX(a, b) having a ' ' after the "а,"
        for (int i = 0; i < expression.Length - 1; i++)
        {
            if (expression[i] == ',')
            {
                expression = Helper.RemoveAt(expression, i);
                expression = Helper.RemoveAt(expression, i);
            }
        }
        
        // If the expression is postfix, we will have 2 operands before an operator
        string[] tokens = Helper.Split(expression, ' ');

        // Means we have two variables and an operator
        if (tokens[0] != "&" && tokens[0] != "|" &&
            tokens[1] != "&" && tokens[1] != "|")
            return true;

        return false;
    }
}