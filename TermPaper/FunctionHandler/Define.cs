using TermPaper.Data_Structures;
namespace TermPaper.FunctionHandler;

public class Define
{
    // TODO Write postfix notation finder
    public static bool IsPostfix(string input)
    {
        return true;
    }
    
    // TODO Write postfix converter
    public static string ConvertToPostfix()
    {
        return "test";
    }
    
    public static Tree.TreeNode BuildTree(string postfix)
    {
        // TODO: Handle ! if it's not functional
        Stack stack = new Stack();
        foreach (var c in postfix)
        {
            if (Helper.IsLetter(c))
            {
                var treeNode = new Tree.TreeNode(c);
                stack.Push(treeNode);
            }
            else
            {
                var right = stack.Pop();
                var left = stack.Pop();
                var node = new Tree.TreeNode(c) {Left = left, Right = right};
                stack.Push(node);
            }
        }
        
        return stack.Pop();
    }
}