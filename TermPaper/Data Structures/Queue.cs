namespace TermPaper.Data_Structures;

public class Queue
{
    public class Node
    {
        public readonly char Key;
        public Node? Next;
        
        public Node(char key)
        {
            this.Key = key;
            this.Next = null;
        }
    }

    public bool IsEmpty = true;
    private Node? _front, _rear;

    public Queue()
    {
        this._front = this._rear = null;
    }
    
    public Queue(string input)
    {
        this._front = this._rear = null;
        foreach (char c in input)
        {
            this.Enqueue(c);
        }
    }

    public Node? Dequeue()
    {
        if (this._front == null)
            return null;
        
        Node temp = this._front;
        this._front = this._front.Next;

        if (this._front == null)
        {
            IsEmpty = true;
            this._rear = null;
        }
        
        return temp;
    }
    
    public void Enqueue(char key)
    {
        // Create a new node.
        Node temp = new Node(key);
        IsEmpty = false;

        if (this._rear == null)
        {
            this._front = this._rear = temp;
            return;
        }

        this._rear.Next = temp;
        this._rear = temp;
    }
    
    public Node? Peek()
    {
        return this._front;
    }
}