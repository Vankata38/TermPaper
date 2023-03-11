namespace TermPaper;

public class Tree
{
    public class Node
    {
        public string Value;
        public Node Left;
        public Node Right;
        
        public Node(string value)
        {
            this.Value = value;
        }
    }
    
    public Node root;
    public Tree(Node root)
    {
        this.root = root;
    }
    
    // TODO - Create a function to add a node to the tree.
    
    // TODO - Create a function to solve the tree.
}