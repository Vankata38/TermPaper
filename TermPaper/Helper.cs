using TermPaper.Data_Structures;
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

    private static int Precedense(char c)
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
                while (stack.Count() > 0 && Precedense(stack.Peek().Value) >= Precedense(c))
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
}