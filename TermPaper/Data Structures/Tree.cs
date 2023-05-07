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

    private TreeNode? Root;
    public Tree()
    {
        Root = null;
    }

    public Tree CopyTree()
    {
        var newTree = new Tree();
        newTree.Root = CopyNode(Root);
        return newTree;
    }

    private TreeNode CopyNode(TreeNode? node)
    {
        if (node == null)
        {
            return null;
        }
        else
        {
            var newNode = new TreeNode(node.Value);
            newNode.Left = CopyNode(node.Left);
            newNode.Right = CopyNode(node.Right);
            return newNode;
        }
    }
    
    public void PrintTree()
    {
        Print2DUtil(Root, 0);
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
        
        Root = stack.Pop();
    }
    
    public string TreeToPostfix()
    {
        return TreeToPostfix(Root);
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

    public string ReplaceValuesAndReturnPostfix(string[] toReplace, string[] replacement)
    {
        // We look at the original tree and replace the variable
        // Then we move the changed elements to the working copy
        string workingCopyPostfix = TreeToPostfix();
        string originalPostfix = workingCopyPostfix;

        workingCopyPostfix = Helper.Replace(workingCopyPostfix, toReplace[0], replacement[0]);
        for (int i = 1; i < toReplace.Length; i++)
        {
            // Go though the original copy and find where u need to replace
            for (int ch = 0; ch < originalPostfix.Length; ch++)
            {
                if (originalPostfix[ch] == toReplace[i][0])
                    workingCopyPostfix = Helper.ReplaceCharAt(workingCopyPostfix, replacement[i][0], ch);
            }
        }

        return workingCopyPostfix;
    }
}