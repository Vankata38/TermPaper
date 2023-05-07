using TermPaper.Data_Structures;
namespace TermPaper;

public static class Helper
{
    public static string Extract(char start, char end, string input)
    {
        string result = "";
        
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != start) continue;
            for (int j = i + 1; j < input.Length; j++)
            {
                if (input[j] == end)
                    return result;
                result += input[j];
            }
        }

        return result;
    }

    private static int IndexOf(string input, string toFind)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == toFind[0])
            {
                bool found = true;
                for (int j = 1; j < toFind.Length; j++)
                {
                    if (input[i + j] != toFind[j])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                    return i;
            }
        }

        return -1;
    }
    
    public static string Replace(string input, string toReplace, string replacement)
    {
        string result = "";
        int index = IndexOf(input, toReplace);
        if (index == -1)
            return input;
        
        for (int i = 0; i < input.Length; i++)
        {
            if (i == index)
            {
                result += replacement;
                i += toReplace.Length - 1;
            }
            else
                result += input[i];
        }

        return result;
    }
    
    public static string SubString(int start, string input, int end = -1)
    {
        string result = "";
        
        if (end == -1)
            end = input.Length;
        
        for (int i = start; i < end; i++)
        {
            result += input[i];
        }

        return result;
    }
    
    public static int FindFirstChar(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != ' ')
                return i;
        }

        return -1;
    }

    public static string RemoveChar(string input, char c)
    {
        string result = "";
        foreach (char ch in input)
        {
            if (ch != c)
                result += ch;
        }

        return result;
    }
    
    public static string[] Split(string input, char separator, int limit = -1)
    {
        int count = 0;

        if (limit == -1)
        {
            foreach (char c in input)
            {
                if (c == separator)
                {
                    count++;
                }
            }
        } else
        {
            count = limit - 1;
        }
        
        
        string[] result = new string[count + 1];
        int index = 0;
        string temp = "";
        for (int i = 0; i < input.Length && limit != 0; i++)
        {
            if (input[i] == separator)
            {
                result[index] = temp;
                temp = "";
                index++;
                limit--;
            } else
                temp += input[i];
        }
        if (temp != "")
            result[index] = temp;
        
        return result;
    }

    public static string ReplaceCharAt(string input, char c, int index)
    {
        string result = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (i == index)
                result += c;
            else
                result += input[i];
        }

        return result;
    }
    
    public static bool Contains(string[] input, string s)
    {
        foreach (string str in input)
        {
            if (str == s)
                return true;
        }

        return false;
    }

    public static string RemoveAt(string input, int index)
    {
        string result = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (i != index)
                result += input[i];
        }

        return result;
    }
    
    public static bool IsLetter(char c)
    {
        return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z';
    }
    
    public static bool IsNumber(char c)
    {
        return c >= '0' && c <= '9';
    }

    public static bool IsOperator(char c)
    {
        return c == '&' || c == '|' || c == '!' || c == '(' || c == ')';
    }
    
    private static int Precedence(char c)
    {
        switch (c)
        {
            case '&':
                return 2;
            case '|':
                return 1;
            default:
                return 0;
        }
    }
    
    public static string ConvertToPostfix(string expression)
    {
        string postfix = "";
        Stack stack = new Stack();

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            if (IsNumber(c) || IsLetter(c) || c == '!')
            {
                postfix += c;
            } else if (c == '&' || c == '|')
            {
                while (stack.Count() > 0 && Precedence(stack.Peek().Value) >= Precedence(c))
                {
                    postfix += stack.Pop().Value;
                }
                stack.Push(new Tree.TreeNode(c));
            } else if (c == '(')
            {
                stack.Push(new Tree.TreeNode(c));
            } else if (c == ')')
            {
                while (stack.Count() > 0 && stack.Peek().Value != '(')
                {
                    postfix += stack.Pop().Value;
                }
                stack.Pop();
            }
        }

        while (stack.Count() > 0)
        {
            postfix += stack.Pop().Value;
        }
        
        return postfix;
    }
    
    public static string CreateReplacementFunc(string funcName ,string funcArgs)
    {
        var args = "";
        for (int i = 0; i < funcArgs.Length; i++)
        {
            if (funcArgs[i] == ',')
            {
                args += ", ";
                continue;
            } 
            args += funcArgs[i];
        }
        
        return funcName + "(" + args + ")";
    }
}