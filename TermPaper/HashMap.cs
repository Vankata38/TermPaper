namespace TermPaper;

public class HashMap
{
    private const int MIN_CAPACITY = 16;
    private Entry[] entries;
    
    // Entry class, used to store the name of the function, the tree, and the hash code.
    private class Entry
    {
        public string name;
        public Tree tree;
        public int hashCode;
        
        public Entry(string Name, Tree Value)
        {
            this.name = Name;
            this.tree = Value;
            this.hashCode = Hash(name);
        }
    }
    
    // Hashing fucntion, uses the format: funcX, where X is the number of the function for hash key.
    // This hashing function can't have collisions.
    public static int Hash(string key)
    {
        int hashCode = 0;
        for (int i = 0; i < key.Length & hashCode == 0; i++)
        {
            if (key[i] == '(')
            {
                char number = key[i - 1];
                hashCode = number - '0';
            }
        }

        return hashCode;
    }
    
    public HashMap(int capacity = MIN_CAPACITY)
    {
        entries = new Entry[capacity];
    }

    // TODO - Create a function to add an entry to the hash table.
    
    // TODO - Create a function to get an entry from the hash table.
    
    // TODO - Create a function to remove an entry from the hash table.
    
    // TODO - Create a function to check if the hash table contains an entry.
    
}