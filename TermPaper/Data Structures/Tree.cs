namespace TermPaper.Data_Structures;

public class Tree
{
    private const int Count = 10;

    public class TreeNode
    {
        public char Value { get; set; }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }

        public TreeNode(char value)
        {
            this.Value = value;
        }
    }

    private TreeNode? _root;
    public Tree()
    {
        _root = null;
    }

    public void PrintTree()
    {
        Print2DUtil(_root, 0);
    }
    
    private void Print2DUtil(TreeNode? root, int space)
    {
        if (root == null)
            return;
        space += Count;
        
        Print2DUtil(root.Right, space);
        
        for (int i = Count; i < space; i++)
            Console.Write(" ");
        Console.Write(root.Value + "\n");
        
        Print2DUtil(root.Left, space);
    }

    public void BuildTree(string postfix)
    {
        Stack stack = new Stack();
        foreach (char c in postfix)
        {
            if (Helper.IsLetter(c))
            {
                if (stack.Count() > 0 && stack.Peek().Value == '!')
                {
                    var notNode = new TreeNode(stack.Pop().Value);
                    notNode.Right = new TreeNode(c);
                    stack.Push(notNode);
                    continue;
                }
                
                var treeNode = new TreeNode(c);
                stack.Push(treeNode);
                
            } else if (c == '!')
            {
                var treeNode = new TreeNode(c);
                stack.Push(treeNode);
            } else if (c != ' ')
            {
                var right = stack.Pop();
                var left = stack.Pop();
                var node = new TreeNode(c) {Left = left, Right = right};
                stack.Push(node);
            }
        }
        
        _root = stack.Pop();
    }
    
    public string TreeToPostfix()
    {
        return TreeToPostfix(_root);
    }
    
    private static string TreeToPostfix(TreeNode? root)
    {
        if (root == null)
        {
            return "";
        }
        else
        {
            string left = TreeToPostfix(root.Left);
            string right = TreeToPostfix(root.Right);
            char current = root.Value;

            if (root.Value == '!')
                return current + right;
            else
                return left + right + current;
            
        }
    }
}