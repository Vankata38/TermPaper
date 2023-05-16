using TermPaper.Data_Structures;
namespace TermPaper.Handlers;

public class FileHandler
{
    private const string Filename = "hashmap.txt";
    private readonly Helper _helper = new Helper();
    
    public void Save(Hashmap map)
    {
        using FileStream stream = new FileStream(Filename, FileMode.Create);
        // using BinaryWriter writer = new BinaryWriter(stream);
        using StreamWriter writer = new StreamWriter(stream);
        for (int i = 0; i < map.Size(); i++)
        {
            var list = map[i];
            for (int j = 0; j < list.GetCount(); j++)
            {
                var node = list.GetHead();
                while (node != null)
                {
                    var tree = node.Value;
                    
                    writer.Write(node.Key + " ");
                    for (int k = 0; k < node.Arguments!.Length; k++)
                    {
                        if (k == 0)
                        {
                            writer.Write($"{node.Arguments[k]}");
                            continue;
                        }
                        writer.Write("," + node.Arguments[k]);
                    }
                    writer.Write(" ");
                    writer.Write(tree!.TreeToPostfix());
                    writer.Write("\n");
                    
                    node = node.Next;
                }
            }
        }
    }

    public Hashmap LoadFromFile()
    {
        Hashmap map = new Hashmap();

        
        if (!File.Exists(Filename))
            return map;
        
        using (StreamReader reader = new StreamReader(Filename))
        {
            string line;
            while ((line = reader.ReadLine()!) != null)
            {
                string[] lineSplit = _helper.Split(line, ' ');
                
                if (lineSplit.Length != 3)
                    continue;
                
                string funcName = lineSplit[0];
                string[] funcArgs = _helper.Split(lineSplit[1], ',');
                string treePostfix = lineSplit[2];

                Tree tree = new Tree();
                tree.BuildTree(treePostfix);
                    
                map.Add(funcName, funcArgs, tree);
            }
        }

        return map;
    }
}