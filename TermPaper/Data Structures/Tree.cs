namespace TermPaper.Data_Structures;

public class Tree
{
    private readonly Helper _helper = new Helper();
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

    private Tree CopyTree()
    {
        var newTree = new Tree();
        newTree._root = CopyNode(_root);
        return newTree;
    }

    private TreeNode? CopyNode(TreeNode? node)
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
            if (_helper.IsLetter(c))
            {
                if (stack.Count() > 0 && stack.Peek()!.Value == '!' && stack.Peek()!.Right == null)
                {
                    var notNode = new TreeNode(stack.Pop()!.Value);
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
    
    private string TreeToPostfix(TreeNode? root)
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

        workingCopyPostfix = _helper.Replace(workingCopyPostfix, toReplace[0], replacement[0]);
        for (int i = 1; i < toReplace.Length; i++)
        {
            for (int ch = 0; ch < originalPostfix.Length; ch++)
            {
                if (originalPostfix[ch] == toReplace[i][0])
                    workingCopyPostfix = _helper.ReplaceCharAt(workingCopyPostfix, replacement[i][0], ch);
            }
        }

        return workingCopyPostfix;
    }

    public bool Solve(string[] variables, string[] values)
    {
        Tree copy = CopyTree();
        
        if (_root != null) 
            ReplaceValues(copy._root!, variables, values);

        bool result = SolveTree(copy._root!);
        return result;
    }

    private bool SolveTree(TreeNode node)
    {
        bool result = false;

        Stack stack = new Stack();
        string expression = TreeToPostfix(node);

        bool reverseNext = false;
        for (int i = 0; i < expression.Length; i++)
        {
            if (Stack.IsEmpty())
            {
                stack.Push(new TreeNode(expression[i]));
                continue;
            }
            else if (reverseNext) {
                reverseNext = false;
                var a = stack.Pop()!.Value;
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
            else if (stack.Peek()!.Value == '&')
            {
                stack.Pop();
                var a = stack.Pop()!.Value;
                var b = stack.Pop()!.Value;

                bool value = _helper.ParseCharToBool(a) && _helper.ParseCharToBool(b);
                stack.Push(new TreeNode(_helper.ParseBoolToChar(value)));
            } else if (stack.Peek()!.Value == '|')
            {
                stack.Pop();
                var a = stack.Pop()!.Value;
                var b = stack.Pop()!.Value;

                bool value = _helper.ParseCharToBool(a) || _helper.ParseCharToBool(b);
                stack.Push(new TreeNode(_helper.ParseBoolToChar(value)));
            } else if (stack.Peek()!.Value == '!')
            {
                reverseNext = true;
            }
            
            stack.Push(new TreeNode(expression[i]));
        }

        if (stack.Count() == 3)
        {
            if (stack.Peek()!.Value == '&')
            {
                stack.Pop();
                var a = stack.Pop()!.Value;
                var b = stack.Pop()!.Value;

                result = _helper.ParseCharToBool(a) && _helper.ParseCharToBool(b);
            } else if (stack.Peek()!.Value == '|')
            {
                stack.Pop();
                var a = stack.Pop()!.Value;
                var b = stack.Pop()!.Value;

                result = _helper.ParseCharToBool(a) || _helper.ParseCharToBool(b);
            }
        }
        else if (stack.Count() == 1)
        {
            result = _helper.ParseCharToBool(stack.Pop()!.Value);
        }
        else
        {
            throw new Exception("Failed to solve tree");
        }
        
        return result;
    }

    private void ReplaceValues(TreeNode? treeNode, string[] variables, string[] values)
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