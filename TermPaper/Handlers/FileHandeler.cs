using TermPaper.Data_Structures;
namespace TermPaper.Handlers;

public class FileHandler
{
    private const int DefaultSize = 23;
    private const string Filename = "hashmap.txt";
    private readonly Helper _helper = new Helper();
    private readonly Validator _validator = new Validator();
    
    public void Save(Hashmap map)
    {
        using FileStream stream = new FileStream(Filename, FileMode.Create);
        using StreamWriter writer = new StreamWriter(stream);
        writer.Write(map.Size() + "\n");
        for (int i = 0; i < map.Size(); i++)
        {
            var list = map[i];
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

    public Hashmap LoadFromFile()
    {
        Hashmap map = new Hashmap();
        if (!File.Exists(Filename))
            return map;

        using StreamReader reader = new StreamReader(Filename);
        string line;
            
        if ((line = reader.ReadLine()!) != null)
        {
            int mapSize = _helper.ParseInt(line);
            if (mapSize != DefaultSize && mapSize > 0)
                map = new Hashmap(mapSize);
        }
            
        while ((line = reader.ReadLine()!) != null)
        {
            string[] lineSplit = _helper.Split(line, ' ');
                
            if (lineSplit.Length != 3)
                continue;
                
            string[] funcAruments = _helper.Split(lineSplit[1], ',');
            string treePostfix = lineSplit[2];
            string input = " " + lineSplit[0] + "(";
            for (int i = 0; i < funcAruments.Length; i++)
            {
                if (i == 0)
                {
                    input += $"{funcAruments[i]}";
                    continue;
                }
                input += ", " + funcAruments[i];
            }
            input += "): " + "\"" + treePostfix + "\"";

            bool valid = _validator.IsValidInput(input, 'd', map, out string funcName, out string[]? funcArgs, out string _);
            if (!valid)
                continue;
            
            Tree tree = new Tree();
            tree.BuildTree(treePostfix);
                    
            map.Add(funcName, funcArgs, tree);
        }

        return map;
    }
    
    public Hashmap DeleteFile()
    {
        if (File.Exists(Filename))
            File.Delete(Filename);
        return new Hashmap();
    }
}