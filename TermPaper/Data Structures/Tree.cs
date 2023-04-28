namespace TermPaper.Data_Structures;

public class Tree
{
    public class TreeNode
    {
        public char Value;
        public TreeNode? Left = null;
        public TreeNode? Right = null;
        
    public TreeNode(char value)
        {
            this.Value = value;
        }
    }
    
    private readonly TreeNode _root;
    public Tree(TreeNode root)
    {
        this._root = root;
    }
}