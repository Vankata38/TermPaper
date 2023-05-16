using TermPaper.Data_Structures;
namespace TermPaper;

public  class Helper
{
    public string Extract(char start, char end, string input)
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

    private int IndexOf(string input, string toFind)
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
    
    public string Replace(string input, string toReplace, string replacement)
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
    
    public string SubString(int start, string input, int end = -1)
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
    
    public int FindFirstChar(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != ' ')
                return i;
        }

        return -1;
    }

    public  string RemoveChar(string input, char c)
    {
        string result = "";
        foreach (char ch in input)
        {
            if (ch != c)
                result += ch;
        }

        return result;
    }
    
    public string[] Split(string input, char separator, int limit = -1)
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

    public string ReplaceCharAt(string input, char c, int index)
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
    
    public bool Contains(string[] input, string s)
    {
        foreach (string str in input)
        {
            if (str == s)
                return true;
        }

        return false;
    }

    public string RemoveAt(string input, int index)
    {
        string result = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (i != index)
                result += input[i];
        }

        return result;
    }
    
    public bool IsLetter(char c)
    {
        return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z';
    }
    
    public bool IsNumber(char c)
    {
        return c >= '0' && c <= '9';
    }

    public bool IsOperator(char c)
    {
        return c == '&' || c == '|' || c == '!' || c == '(' || c == ')';
    }
    
    private int Precedence(char c)
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
    
    public string ConvertToPostfix(string expression)
    {
        string postfix = "";
        Stack stack = new Stack();

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            if (IsLetter(c) || c == '!')
            {
                postfix += c;
            } else if (c == '&' || c == '|')
            {
                while (stack.Count() > 0 && Precedence(stack.Peek()!.Value) >= Precedence(c))
                {
                    postfix += stack.Pop()!.Value;
                }
                stack.Push(new Tree.TreeNode(c));
            } else if (c == '(')
            {
                stack.Push(new Tree.TreeNode(c));
            } else if (c == ')')
            {
                while (stack.Count() > 0 && stack.Peek()!.Value != '(')
                {
                    postfix += stack.Pop()!.Value;
                }
                stack.Pop();
            }
        }

        while (stack.Count() > 0)
        {
            postfix += stack.Pop()!.Value;
        }
        
        return postfix;
    }
    
    public  string CreateReplacementFunc(string funcName ,string funcArgs)
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

    public  bool ParseCharToBool(char input)
    {
        if (input == '1')
            return true;
        else if (input == '0')
            return false;
        else
            throw new Exception("Invalid char");
    }
    
    public  string[] DecimalToBinary(int number, int numberSize) {
        string[] binaryNum = new string[numberSize];
        int index = numberSize - 1;
        
        // fill 1s
        while (number > 0) {
            binaryNum[index] = (number % 2 == 1) ? "1" : "0";
            number /= 2;
            index--;
        }
        
        // fill 0s
        while (index >= 0) {
            binaryNum[index] = "0";
            index--;
        }

        return binaryNum;
    }
    
    public char ParseBoolToChar(bool input)
    {
        return input ? '1' : '0';
    }

    public int ParseInt(string input)
    {
        int result = 0;
        foreach (char ch in input)
        {
            if (IsNumber(ch))
                result = result * 10 + (ch - '0');
        }

        return result;
    } 
}