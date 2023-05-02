namespace TermPaper.Data_Structures;

public class LinkedList
{
    private Node? _head;
    private int _count;

    public LinkedList()
    {
        _head = null;
        _count = 0;
    }

    public int Count
    {
        get { return _count; }
    }

    public void AddFirst(string funcName, int argumentsCount, Tree value)
    {
        Node newNode = new Node(funcName, argumentsCount, value);

        if (_head == null)
        {
            _head = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head = newNode;
        }

        _count++;
    }

    public void AddLast(string funcName, int argumentsCount, Tree value)
    {
        Node newNode = new Node(funcName, argumentsCount, value);

        if (_head == null)
        {
            _head = newNode;
        }
        else
        {
            Node current = _head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }

        _count++;
    }

    public bool Contains(string funcName)
    {
        Node? current = _head;

        while (current != null)
        {
            if (current.Key == funcName)
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public Tree? Get(string funcName)
    {
        Node? current = _head;

        while (current != null)
        {
            if (current.Key == funcName)
            {
                return current.Value;
            }
            
            current = current.Next;
        }
        return null;
    }

    public int GetArgumentsCount(string funcName)
    {
        Node? current = _head;

        while (current != null)
        {
            if (current.Key == funcName)
            {
                return current.ArgumentCount ?? 0;
            }
            
            current = current.Next;
        }

        return 0;
    }
    
    public class Node
    {
        public readonly string? Key;
        public int? ArgumentCount;
        public Tree? Value;
        public Node? Next;

        public Node(string? funcName, int? count, Tree? value)
        {
            Key = funcName;
            ArgumentCount = count;
            Value = value;
            Next = null;
        }
    }
}