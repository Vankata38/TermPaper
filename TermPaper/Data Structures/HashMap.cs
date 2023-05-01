namespace TermPaper.Data_Structures;

public class Hashmap
{
    private class Entry
    {
        public LinkedList Value;

        public Entry()
        { 
            Value = new LinkedList();
        }
    }

    private static int _size;
    private readonly Entry[] _entries;
    
    public Hashmap(int size = 17)
    {
        if (size < 17)
            size = 17;
        
        _size = size;
        _entries = new Entry[size];
    }

    private static int Hash(string key)
    {
        var hash = 0;
        foreach (var c in key)
        {
            hash += ((hash << 5) + c);
        }

        return hash % _size;
    }

    public Tree? Get(string funcName)
    {
        // Get the KVP
        var index = Hash(funcName);
        var current = _entries[index];
        
        // Get the list and search
        var list = current.Value;
        Tree tree = list.Get(funcName);

        return tree;
    }
    
    public void Add(string funcName, Tree value)
    {
        int index = Hash(funcName);
        if (_entries[index] == null)
        {
            _entries[index] = new Entry();
        } 
        
        LinkedList list = _entries[index].Value;
        list.AddLast(funcName, value);
    }
}