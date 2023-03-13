namespace TermPaper;

public class Tree
{
    // Node class, used to store the value of the node and the left and right children.
    public class Node
    {
        private readonly char _value;
        public Node? Left = null;
        public Node? Right = null;
        
    // Constructor for the Node class.
    public Node(char value)
        {
            this._value = value;
        }
    }
    
    // Root node of the tree. Set to readonly so it can't be changed.
    public readonly Node Root;
    public Tree(Node root)
    {
        this.Root = root;
    }
}