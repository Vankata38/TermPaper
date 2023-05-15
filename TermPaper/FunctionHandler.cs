using TermPaper.Data_Structures;

namespace TermPaper;

public class FunctionHandler
{
    public static void Define(string input, Hashmap map)
    {
        Console.WriteLine(Validator.IsValidInput(input, 'd', map, out string funcName, out string[]? arguments, out string expression));
                    
        if (!Validator.IsValidInput(input, 'd', map, out funcName, out arguments, out expression))
            return;
                    
        Console.WriteLine(expression);
                    
        Tree newTree = new Tree();
        newTree.BuildTree(expression);
        newTree.PrintTree();
                    
        map.Add(funcName, arguments, newTree);
    }

    public static void Solve(string input, Hashmap map)
    {
        Console.WriteLine(Validator.IsValidInput(input, 's', map, out string funcName, out string[]? arguments, out string? _));
        if (!Validator.IsValidInput(input, 's', map, out funcName, out arguments, out string? _))
            return;
        
        // Get the function from the hashmap
        Tree funcTree = map.Get(funcName)!;
        string[] variables = map.GetArguments(funcName)!;
        bool answer = funcTree.Solve(variables, arguments!);
        
        Console.WriteLine($"Answer for {funcName}, with parameters {string.Join(", ", arguments)} is {answer}");
    }

    public static void All(string input, Hashmap map)
    {
        Console.WriteLine(Validator.IsValidInput(input, 'a', map, out string funcName, out string[]? _, out string? _));
        if (!Validator.IsValidInput(input, 'a', map, out funcName, out _, out string? _))
            return;
        
        int funcArgsCount = map.GetArgumentsCount(funcName);
        string[] inputs = new string[(int)Math.Pow(2, funcArgsCount)];
        string[] answers = new string[inputs.Length];
        Tree funcTree = map.Get(funcName)!;
        
        string[] variables = map.GetArguments(funcName)!;
        for (int i = 0; i < inputs.Length; i++)
        {
            string[] inputI = Helper.DecimalToBinary(i, funcArgsCount);
            for (int j = 0; j < inputI.Length; j++)
            {
                if (j == 0)
                {
                    inputs[i] += $"{inputI[j]}";
                    continue;
                }
                inputs[i] += $" , {inputI[j]}";
            }
            
            bool answer = funcTree.Solve(variables, inputI);
            if (answer)
                answers[i] = 1.ToString();
            else 
                answers[i] = 0.ToString();
        }

        FormattedPrint(funcArgsCount, funcName, variables, inputs, answers);
    }

    private static void FormattedPrint(int funcArgsCount, string funcName, string[] variables, string[] inputs, string[] answers)
    {
        string printFunc = $"All {funcName} -> ";
        Console.Write(printFunc);
        for (int i = 0; i < funcArgsCount; i++)
        {
            if (i == funcArgsCount - 1)
            {
                Console.Write(variables[i]);
                continue;
            }
            Console.Write($"{variables[i]} , ");
        }
        Console.Write($": {funcName}");

        int offset = printFunc.Length;
        for (int i = 0; i < inputs.Length; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < offset; j++)
                Console.Write(" ");
            Console.Write(inputs[i] + " : " + answers[i]);
        }
        Console.WriteLine();
    }
}