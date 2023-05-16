using TermPaper.Data_Structures;
namespace TermPaper;

public class Validator
{
    private readonly Helper _helper = new Helper();
    
    public bool IsValidInput(string input, char mode, Hashmap map, out string functionName, out string[]? argumentsArray, out string? expression)
    {
        // Get the name, arguments and definition of the function
        functionName = _helper.Extract(' ', '(', input);
        string args = _helper.Extract('(', ':', input);
        expression = _helper.Extract('"', '"', input);
        argumentsArray = null;
        
        if ((functionName == "") || (mode != 'a' && args == "") || (mode == 'd' && expression == ""))
            return false;
        
        if (mode != 'f' && mode != 'a')
        {
            if (args[args.Length-1] != ')') 
                return false;
            args = args.Remove(args.Length - 1, 1);
        }
        args = _helper.RemoveChar(args, ' ');
        expression += " ";

        if (mode == 'd' && map.Contains(functionName))
            return false;

        if ((mode == 's' || mode == 'a') && !map.Contains(functionName))
            return false;
        
        // TODO: Remove debug statements
        Console.WriteLine("\nDEBUG: ");
        Console.WriteLine($"Input: {input}");
        Console.WriteLine($"Name: {functionName}");
        Console.WriteLine($"Arguments: {args}");
        Console.WriteLine($"Expression: {expression} \n");

        if (!IsValidFunctionName(functionName))
            return false;

        bool valid;
        // Extract the arguments of the function and error if 0
        if (mode != 'a' && mode != 'f')
        {
            argumentsArray = GetArguments(args, out valid, mode);
            if (!valid || argumentsArray.Length == 0)
                return false;
        }

        // Validate the brackets and make sure we only have 2 (' " ') parentheses
        if (!IsValidBrackets(input, mode))
            return false;
        
        // TODO Fix number of operators and operands crashing the program 
        // Validate the expression if in definition mode
        if (!IsValidExpression(expression, argumentsArray!))
            return false;

        if (!GetFunctions(expression, out string[] funcNames, out string[] funcArgs, out valid))
            if (!IsPostfix(expression))
            {
                expression = _helper.ConvertToPostfix(expression);
                return true;
            }
        
        if (!valid)
            return false;

        for (int i = 0; i < funcNames.Length; i++)
        {
            int argsCount = GetArguments(funcArgs[i], out valid, 'd').Length;
            if (!map.Contains(funcNames[i]) || (argsCount != map.GetArgumentsCount(funcNames[i])))
                return false;
            
            string toReplace = _helper.CreateReplacementFunc(funcNames[i], funcArgs[i]);
            char replacement = (char)('z' - i);

            expression = _helper.Replace(expression, toReplace, replacement.ToString());
        }

        expression = _helper.ConvertToPostfix(expression);

        for (int i = 0; i < funcNames.Length; i++)
        {
            char toReplace = (char)('z' - i);
            string[] argsFromCall = _helper.Split(funcArgs[i], ',');
            string[] argsFromOriginalTree = map.GetArguments(funcNames[i])!;
            Tree originalFunctionTree = map.Get(funcNames[i])!;
            
            string postfix = originalFunctionTree.ReplaceValuesAndReturnPostfix(argsFromOriginalTree, argsFromCall);
            expression = _helper.Replace(expression, toReplace.ToString(), postfix);
        }
        
        return true;
    }

    // Returns the arguments of the function
    private string[] GetArguments(string args, out bool valid, char mode)
    {
        List<string> variables = new List<string>();
        bool inVariable = false;
        string temp = "";
        valid = true;
        
        foreach (char c in args)
        {
            if (c != ',')
            {
                // Check if the character is a valid 
                if (!(_helper.IsLetter(c)) && !(_helper.IsNumber(c)))
                    valid = false;
                
                // If we have a num outside of a variable, it's invalid
                if (!inVariable && _helper.IsNumber(c) && mode != 's')
                    valid = false;
                
                if (mode == 's' && c != '0' && c != '1')
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
    
    private bool IsValidFunctionName(string name)
    {
        foreach (char c in name)
        {
            if (!_helper.IsLetter(c) && !_helper.IsNumber(c))
                return false;
        }

        return true;
    }
    
    // Checks the number of brackets and if there are flipped brackets (first closing then opening)
    private bool IsValidBrackets(string input, char mode)
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
    
    private bool IsValidExpression(string exp, string[] variables)
    {
        bool inVariable = false;
        string variable = "";
        foreach (char c in exp)
        {
            if (_helper.IsLetter(c) || _helper.IsNumber(c))
            {
                if (!inVariable && _helper.IsNumber(c))
                    return false;
                
                inVariable = true;
                variable += c;
            } else if (_helper.IsOperator(c) || c == ' ' || c == ',')
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
                    
                    if (!_helper.Contains(variables, variable))
                        return false;

                    variable = "";
                    inVariable = false;
                }
            } else
            {
                return false;
            }
        }
        
        return true;
    }

    private bool GetFunctions(string expression, out string[] functionNames, out string[] functionArgs, out bool valid)
    {
        bool weHaveFunction = false;
        bool inText = false;
        int count = 0;
        foreach (char c in expression)
        {
            if (_helper.IsLetter(c) || _helper.IsNumber(c))
            {
                if (inText)
                    continue;
                
                inText = true;
            } else if (c == '(')
            {
                if (!inText)
                    continue;
                
                count++;
                inText = false;
                weHaveFunction = true;
            } else if (c == ')' || c == ' ')
            {
                inText = false;
            }
        }

        if (!weHaveFunction)
        {
            functionNames = new string[0];
            functionArgs = new string[0];
            valid = true;
            return false;
        }

        functionNames = new string[count];
        functionArgs = new string[count];

        bool inName = false;
        bool possibleFunction = false;

        int index = 0;
        string funcArgs = "";
        string funcName = "";
        
        foreach (char c in expression)
        {
            if (_helper.IsLetter(c) || _helper.IsNumber(c) || c == ',')
            {
                if (possibleFunction)
                {
                    funcArgs += c;
                    continue;
                }
                
                if (_helper.IsNumber(c) && !inName)
                {
                    valid = false;
                    return false;
                }

                inName = true;
                funcName += c;
            } else if (c == '(')
            {
                if (inName)
                    possibleFunction = true;
            } else if (c == ')')
            {
                if (possibleFunction)
                {
                    functionNames[index] = funcName;
                    functionArgs[index] = funcArgs;

                    index++;
                    funcName = "";
                    funcArgs = "";
                }
            } else if (c == '!' || c == '&' || c == '|')
            {
                inName = false;
                possibleFunction = false;
                funcName = "";
                funcArgs = "";
            }
        }

        valid = true;
        return true;
    }

    private bool IsPostfix(string expression)
    {
        // We need to handle funcX(a, b) having a ' ' after the "а,"
        for (int i = 0; i < expression.Length - 1; i++)
        {
            if (expression[i] == ',')
            {
                expression = _helper.RemoveAt(expression, i);
                expression = _helper.RemoveAt(expression, i);
            }
        }
        
        // If the expression is postfix, we will have 2 operands before an operator
        string[] tokens = _helper.Split(expression, ' ');

        // Means we have two variables and an operator
        if (tokens[0] != "&" && tokens[0] != "|" &&
            tokens[1] != "&" && tokens[1] != "|")
            return true;

        return false;
    }
}