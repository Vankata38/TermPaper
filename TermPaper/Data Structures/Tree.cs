namespace TermPaper.Data_Structures;

public class Tree
{
    private static int COUNT = 10;
    
    public class TreeNode
    {
        public readonly char Value;
        public TreeNode? Left = null;
        public TreeNode? Right = null;
        
        public TreeNode(char value)
        {
            this.Value = value;
        }
    }
    
    public static TreeNode? Root;
    public Tree()
    {
        Root = null;
    }

    public void PrintTree()
    {
        Print2DUtil(Root, 0);
    }
    
    private void Print2DUtil(TreeNode? root, int space)
    {
        if (root == null)
            return;
        space += COUNT;
        
        Print2DUtil(root.Right, space);
        
        for (int i = COUNT; i < space; i++)
            Console.Write(" ");
        Console.Write(root.Value + "\n");
        
        Print2DUtil(root.Left, space);
    }

    public void BuildTree(string postfix)
    {
        // TODO: Handle ! if it's not functional
        Stack stack = new Stack();
        foreach (var c in postfix)
        {
            if (Helper.IsLetter(c))
            {
                if (stack.Count() > 0 && stack.Peek().Value == '!')
                {
                    var notNode = new Tree.TreeNode(stack.Pop().Value);
                    notNode.Left = new TreeNode(c);
                    stack.Push(notNode);
                    continue;
                }
                
                var treeNode = new Tree.TreeNode(c);
                stack.Push(treeNode);
                
            } else if (c == '!')
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
        
        Root = stack.Pop();
    }
}