namespace TermPaper.Data_Structures;

public class Stack
{
    private class Node
    {
        public Tree.TreeNode Value;
        public Node? Next;
    }

    private Node _top;

    public Stack()
    {
        this._top = null;
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
}