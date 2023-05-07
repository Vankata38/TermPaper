using TermPaper.Data_Structures;

namespace TermPaper;

public class FunctionHandler
{
    public static void Define(string input, Hashmap map)
    {
        Console.WriteLine(Validator.IsValidInput(input, 'd', map, out string funcName, out string[]? arguments, out string expression));
                    
        if (!Validator.IsValidInput(input, 'd', map, out funcName, out arguments, out expression))
            return;
                    
        // TODO - Remove debug
        Console.WriteLine(expression);
                    
        Tree newTree = new Tree();
        newTree.BuildTree(expression);
        newTree.PrintTree();
                    
        map.Add(funcName, arguments, newTree);
    }

    public static void Solve(string input, Hashmap map)
    {
        Console.WriteLine(Validator.IsValidInput(input, 's', map, out string funcName, out string[]? arguments, out string expression));
        if (!Validator.IsValidInput(input, 's', map, out funcName, out arguments, out expression))
            return;
        
        
    }
}