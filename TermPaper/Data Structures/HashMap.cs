namespace TermPaper.Data_Structures;

public class Hashmap
{
    private class Entry
    {
        public readonly LinkedList Value;

        public Entry()
        { 
            Value = new LinkedList();
        }
    }

    private readonly int _size;
    private readonly Entry[] _entries;
    
    public Hashmap(int size = 17)
    {
        if (size < 17)
            size = 17;
        
        _size = size;
        _entries = new Entry[size];
    }

    private int Hash(string key)
    {
        var hash = 0;
        foreach (var c in key)
        {
            hash += ((hash << 5) + c) % _size;
        }

        return hash % _size;
    }

    public Tree? Get(string funcName)
    {
        var index = Hash(funcName);
        var current = _entries[index];
        
        var list = current.Value;
        Tree? tree = list.Get(funcName);

        return tree;
    }

    public bool Contains(string funcName)
    {
        int hash = Hash(funcName);
        if (_entries[hash] == null)
            return false;
        
        LinkedList list = _entries[hash].Value;
        return list.Contains(funcName);
    }
    
    public int GetArgumentsCount(string funcName)
    {
        var index = Hash(funcName);
        var current = _entries[index];
        
        var list = current.Value;
        int argumentsCount = list.GetArgumentsCount(funcName);

        return argumentsCount;
    }
    
    public void Add(string funcName, string[]? arguments, Tree value)
    {
        int index = Hash(funcName);
        if (_entries[index] == null)
            _entries[index] = new Entry();

        LinkedList list = _entries[index].Value;
        list.AddLast(funcName, arguments, value);
    }
    
    public string TreeToPostfix(string funcName)
    {
        int index = Hash(funcName);
        LinkedList list = _entries[index].Value;
        
        string postfix = list.TreeToPostfix(funcName);
        return postfix;
    }
}