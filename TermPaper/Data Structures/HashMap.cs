namespace TermPaper.Data_Structures;

public class HashMap
{
    private const int MinCapacity = 19;
    private readonly Entry[] _entries;
    
    // Entry class, used to store the name of the function, the tree, and the hash code.
    public class Entry
    {
        public string Key;
        public Tree Value;
        public Entry? Next;
        
        public Entry(string key, Tree tree)
        {
            this.Key = key;
            this.Value = tree;
        }
    }
    
    public HashMap(int capacity = MinCapacity)
    {
        _entries = new Entry[capacity];
    }
    
    private static int Hash(string key)
    {
        var hash = 0;
        foreach (var c in key)
        {
            hash += ((hash << 5) + c) % MinCapacity;
        }

        return hash;
    }

    public Tree? Get(string key)
    {
        var hash = Hash(key);
        var current = _entries[hash];
        
        while (current != null)
        {
            if (current.Key == key)
            {
                return current.Value;
            }
            current = current.Next;
        }

        return null;
    }
    
    public void Insert(string key, Tree value)
    {
        var index = Hash(key);
        Entry? current = _entries[index];
        
        while (current != null)
        {
            if (current.Key == key)
            {
                current.Value = value;
                return;
            }
            current = current.Next;
        }
        
        var entry = new Entry(key, value);
        if (_entries[index].Next == null)
        {
            _entries[index] = entry;
        }
        else
        {
            entry.Next = _entries[index];
            _entries[index] = entry;
        }
    }

    public void Remove(string key)
    {
        var hash = Hash(key);
        var current = _entries[hash];
        Entry? previous = null;
        
        while (current != null)
        {
            if (current.Key == key)
            {
                if (previous != null)
                {
                    previous.Next = current.Next;
                }
                else
                {
                    _entries[hash] = current.Next;
                }
            }

            previous = current;
            current = current.Next;
        }
    }
}