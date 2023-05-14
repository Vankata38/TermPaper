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
            
            return left + right + current;
        }
    }

    public string ReplaceValuesAndReturnPostfix(string[] toReplace, string[] replacement)
    {
        string workingCopyPostfix = TreeToPostfix();
        string originalPostfix = workingCopyPostfix;

        workingCopyPostfix = Helper.Replace(workingCopyPostfix, toReplace[0], replacement[0]);
        for (int i = 1; i < toReplace.Length; i++)
        {
            for (int ch = 0; ch < originalPostfix.Length; ch++)
            {
                if (originalPostfix[ch] == toReplace[i][0])
                    workingCopyPostfix = Helper.ReplaceCharAt(workingCopyPostfix, replacement[i][0], ch);
            }
        }

        return workingCopyPostfix;
    }

    public bool Solve(string[] variables, string[] values)
    {
        bool result = false;
        Tree copy = CopyTree();
        
        if (Root != null) 
            ReplaceValues(copy.Root!, variables, values);

        result = SolveTree(copy.Root!, variables, values);
        return result;
    }

    private bool SolveTree(TreeNode node, string[] variables, string[] values)
    {
        bool result = false;

        Stack stack = new Stack();
        string expression = TreeToPostfix(node);

        bool reverseNext = false;
        for (int i = 0; i < expression.Length; i++)
        {
            if (stack.isEmpty())
            {
                stack.Push(new TreeNode(expression[i]));
                continue;
            }
            else if (reverseNext) {
                reverseNext = false;
                var a = stack.Pop().Value;
                stack.Pop();
                
                char aReverseChar;
                if (a == '0')
                    aReverseChar = '1';
                else if (a == '1')
                    aReverseChar = '0';
                else
                    throw new Exception("Failed to parse char to bool");
                
                stack.Push(new TreeNode(aReverseChar));
            }
            else if (stack.Peek().Value == '&')
            {
                stack.Pop();
                var a = stack.Pop().Value;
                var b = stack.Pop().Value;

                bool value = Helper.ParseCharToBool(a) && Helper.ParseCharToBool(b);
                stack.Push(new TreeNode(Helper.ParseBoolToChar(value)));
            } else if (stack.Peek().Value == '|')
            {
                stack.Pop();
                var a = stack.Pop().Value;
                var b = stack.Pop().Value;

                bool value = Helper.ParseCharToBool(a) || Helper.ParseCharToBool(b);
                stack.Push(new TreeNode(Helper.ParseBoolToChar(value)));
            } else if (stack.Peek().Value == '!')
            {
                reverseNext = true;
            }
            
            stack.Push(new TreeNode(expression[i]));
        }

        if (stack.Count() == 3)
        {
            if (stack.Peek().Value == '&')
            {
                stack.Pop();
                var a = stack.Pop().Value;
                var b = stack.Pop().Value;

                result = Helper.ParseCharToBool(a) && Helper.ParseCharToBool(b);
            } else if (stack.Peek().Value == '|')
            {
                stack.Pop();
                var a = stack.Pop().Value;
                var b = stack.Pop().Value;

                result = Helper.ParseCharToBool(a) || Helper.ParseCharToBool(b);
            }
        }
        else if (stack.Count() == 1)
        {
            result = Helper.ParseCharToBool(stack.Pop().Value);
        }
        else
        {
            throw new Exception("Failed to solve tree");
        }
        
        return result;
    }

    private void ReplaceValues(TreeNode treeNode, string[] variables, string[] values)
    {
        if (treeNode == null)
            return;
        
        for (int i = 0; i < variables.Length; i++)
        {
            if (treeNode.Value == variables[i][0])
            {
                treeNode.Value = values[i][0];
            }
        }

        ReplaceValues(treeNode.Left, variables, values);
        ReplaceValues(treeNode.Right, variables, values);
    }
}