namespace TermPaper;

public class Helper
{
    public static string ToUpperCase(string input)
    {
        string upperCase = "";
        
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] >= 'a' && input[i] <= 'z')
                upperCase += (char)(input[i] - 32);
            else
                upperCase += input[i];
        }

        return upperCase;
    }
    
    public static string Substring(string str, int start, int lenght = -1)
    {
        string subString = "";
        
        if (lenght == -1)
            lenght = str.Length - start;
        
        for (int i = start; i < start + lenght; i++)
        {
            subString += str[i];
        }
        
        return subString;
    }
}