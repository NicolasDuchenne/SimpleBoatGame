using Raylib_cs;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

public class OptionsFile
{
    public static string OPTIONSFILENAME = "options.json";
    public static string SAVEFILLNAME = "save.json";
    private string AppName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
    private string fullPath;
    protected Dictionary<string, string> options;
    public OptionsFile(string name)
    {
        options = new Dictionary<string, string>();
        fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName, name);
        Console.WriteLine($"Nom du fichier d'options: {fullPath}");
    }

    public void Clear()
    {
        options.Clear();
    }

    public void AddOption(string key, string value)
    {
        options[key] = value;
    }

    public void AddOption(string key, float value)
    {
        options[key] = value.ToString();
    }
    public void AddOption(string key, int value)
    {
        options[key] = value.ToString();
    }

    public bool IsOptionExists(string key)
    {
        return options.ContainsKey(key);
    }

    public void AddOption(string key, object value)
    {
        string json = JsonSerializer.Serialize(value);
        options[key] = json;
    }

    public string GetOptionString(string key)
    {
        if (options.ContainsKey(key))
        {
            return options[key];
        }
        return "";
    }
    public int GetOptionInt(string key)
    {
        if (options.ContainsKey(key))
        {
            try
            {
                return int.Parse(options[key]);
            }
            catch
            {
                return 0;
            }
            
        }
        else
            return 0;
    }

    public float GetOptionFloat(string key)
    {
        if (options.ContainsKey(key))
        {
            try
            {
                return float.Parse(options[key]);
            }
            catch
            {
                return 0;
            }
            
        }
        else
            return 0;
    }

    public bool GetOptionBool(string key)
    {
        if (options.ContainsKey(key))
        {
            try
            {
                return bool.Parse(options[key]);
            }
            catch
            {
                return false;
            }
            
        }
        else
            return false;
    }

    public void Load()
    {
        if (File.Exists(fullPath))
        {
            var jsonString = File.ReadAllText(fullPath);
            options = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString)!;
            if (options == null)
            {
                throw new JsonException($"Fichier d'option invalide {fullPath}");
            }
        }
        else
        {
            options.Clear();
        }
    }


    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        var jsonString = JsonSerializer.Serialize(options);
        Debug.WriteLine($"JSON: {jsonString}");
        File.WriteAllText(fullPath, jsonString);
    }
}