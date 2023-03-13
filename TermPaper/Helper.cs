using TermPaper.Data_Structures;
namespace TermPaper;

public class Helper
{
    
    public static string[] SplitLine(string input, char separator)
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

        return result;
    }
    
    // TODO - Create a function to check if the input is valid.
    public static bool IsValidInput(string input)
    {
        // TODO - Use a Queue to check if the syntax is correct.
        // Example input: func1(a, b): "a & b"
        // Example input: func2(a, b, c): "func1(a, b) | c"
        Queue funcKeywordValidation = new Queue("func");
        
        // Make sure the function keyword is valid.
        for (int i = 0; i < 4; i++)
        {
            if (funcKeywordValidation.Dequeue()!.Key != input[i])
                return false;
        }
        
        int countBrackets = 0;
        bool instructionPart = false;
        Queue variableValidation = new Queue();
        
        for (int i = 4; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                countBrackets++;
            }
            else if (input[i] == ')')
            {
                countBrackets--;
            }
            else if (input[i] == '\"')
            {
                if (!instructionPart)
                    instructionPart = true;
                else
                {
                    if (countBrackets != 0)
                        return false;
                    if (variableValidation.IsEmpty)
                        // If we got to the end and we dont have any variables left, then the input is valid.
                        return true; 
                }
            }
            else if (input[i] >= 'a' && input[i] <= 'z')
            {
                if (!instructionPart)
                {
                    variableValidation.Enqueue(input[i]);
                }
                else
                {
                    if (variableValidation.IsEmpty)
                        return false;
                    Console.WriteLine(variableValidation.Peek().Key);
                    
                    if (variableValidation.Peek().Key == input[i])
                    {
                        variableValidation.Dequeue();
                        Console.WriteLine("Dequeued: " + input[i]);
                    }
                }
            }
        }

        return false;
    }
}