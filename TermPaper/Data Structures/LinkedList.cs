namespace TermPaper.Data_Structures;

public class LinkedList
{
    private Node? _head;

    public LinkedList()
    {
        _head = null;
    }

    public void AddLast(string funcName, string[]? arguments, Tree value)
    {
        Node newNode = new Node(funcName, arguments, value);

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

    public string[]? GetArguments(string funcName)
    {
        Node? current = _head;

        while (current != null)
        {
            if (current.Key == funcName)
            {
                if (current.Arguments != null) 
                    return current.Arguments;
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
                if (current.Arguments != null) 
                    return current.Arguments.Length;
            }
            
            current = current.Next;
        }

        return 0;
    }

    public string TreeToPostfix(string funcName)
    {
        var tree = Get(funcName);
        
        if (tree == null)
            return "";

        var postfix = tree.TreeToPostfix();
        return postfix;
    }
    
    public int GetCount()
    {
        int count = 0;
        Node? current = _head;

        while (current != null)
        {
            count++;
            current = current.Next;
        }

        return count;
    }

    public Node? GetHead()
    {
        if (_head != null)
            return _head;
        return null;
    }
    
    public class Node
    {
        public readonly string? Key;
        public readonly string[]? Arguments;
        public readonly Tree? Value;
        public Node? Next;

        public Node(string? funcName, string[]? arguments, Tree? value)
        {
            Key = funcName;
            Arguments = arguments;
            Value = value;
            Next = null;
        }
    }
}