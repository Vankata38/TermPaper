using TermPaper.Data_Structures;
namespace TermPaper.Handlers;

public class FunctionHandler
{
    private readonly Helper _helper = new Helper();
    private readonly Validator _validator = new Validator();
    public void Define(string input, Hashmap map)
    {
        bool valid = _validator.IsValidInput(input, 'd', map, out string funcName, out string[]? arguments,
            out string? expression);
        Console.WriteLine($"Command is valid: {valid}");
        if (!valid)
            return;

        Tree newTree = new Tree();
        newTree.BuildTree(expression!);
        newTree.PrintTree();
                    
        map.Add(funcName, arguments, newTree);
    }

    public void Solve(string input, Hashmap map)
    {
        bool valid = _validator.IsValidInput(input, 's', map, out string funcName, out string[]? arguments, out string? _);
        Console.WriteLine($"Command is valid: {valid}");
        if (!valid)
            return;
        
        // Get the function from the hashmap
        Tree funcTree = map.Get(funcName)!;
        string[] variables = map.GetArguments(funcName)!;
        bool answer = funcTree.Solve(variables, arguments!);

        // Print the answer
        Console.Write($"Answer for {funcName}, with parameters ");
        for (int i = 0; i < arguments!.Length; i++)
        {
            Console.Write($"{arguments[i]}");
            if (i != arguments.Length - 1)
                Console.Write(", ");
        }
        Console.Write($" is {answer} \n");
    }

    public void All(string input, Hashmap map)
    {
        bool valid = _validator.IsValidInput(input, 'a', map, out string funcName, out _, out string? _);
        Console.WriteLine($"Command is valid: {valid}");
        if (!valid)
            return;
        
        int funcArgsCount = map.GetArgumentsCount(funcName);
        string[] inputs = new string[(int)Math.Pow(2, funcArgsCount)];
        string[] answers = new string[inputs.Length];
        Tree funcTree = map.Get(funcName)!;
        
        string[] variables = map.GetArguments(funcName)!;
        for (int i = 0; i < inputs.Length; i++)
        {
            string[] inputI = _helper.DecimalToBinary(i, funcArgsCount);
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

    private void FormattedPrint(int funcArgsCount, string funcName, string[] variables, string[] inputs, string[] answers)
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