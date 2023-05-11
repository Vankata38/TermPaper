namespace TermPaper.Data_Structures;

public class Stack
{
    private class Node
    {
        public Tree.TreeNode Value;
        public Node? Next;
    }

    private static Node _top;

    public Stack()
    {
        Stack._top = null;
    }
    
    public void Push(Tree.TreeNode value)
    {
        var newNode = new Node {Value = value, Next = _top};
        _top = newNode;
    }

    public Tree.TreeNode Pop()
    {
        if (_top == null)
        {
            throw new InvalidOperationException("Stack is empty!");
        }

        Tree.TreeNode value = _top.Value;
        _top = _top.Next;
        return value;
    }
    
    public Tree.TreeNode Peek()
    {
        return _top.Value;
    }
    
    public bool isEmpty()
    {
        return _top == null;
    }
    public int Count()
    {
        int count = 0;
        Node? current = _top;
        while (current != null)
        {
            count++;
            current = current.Next;
        }

        return count;
    }
}