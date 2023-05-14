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
        
        Tree funcTree = map.Get(funcName)!;
        string[] variables = map.GetArguments(funcName)!;
        for (int i = 0; i <= (int)Math.Pow(2, funcArgsCount) - 1; i++)
        {
            string[] arguments = Helper.DecimalToBinary(i, funcArgsCount);
                
            bool answer = funcTree.Solve(variables, arguments);
            Console.WriteLine($"Answer for {funcName}, with parameters {string.Join(", ", arguments)} is {answer}");
        }
    }
}