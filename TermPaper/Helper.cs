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

    // TODO: Make this function have limit, so we can eliminate SplitOne
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

    public static bool Contains(string[] input, string s)
    {
        foreach (string str in input)
        {
            if (str == s)
                return true;
        }

        return false;
    }
}