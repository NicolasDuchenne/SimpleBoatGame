public class Save
{
    private static Save? instance;

    public static Save Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Save();
            }
            return instance;
        }
    }
    public Dictionary<string, Score> levelsScore = new Dictionary<string, Score>();

    public void PrintSave()
    {
        Console.WriteLine("printSave");
        foreach(KeyValuePair<string, Score> levelScore in levelsScore)
        {

            Console.WriteLine($"level {levelScore.Key}");
            Console.WriteLine(levelScore.Value.Timer);
            Console.WriteLine(levelScore.Value.Moves);
            Console.WriteLine(levelScore.Value.SendToPast);
        }
    }
    public void ResetSave()
    {
        foreach(KeyValuePair<string, Score> levelScore in levelsScore)
        {
            levelScore.Value.SetDefaultScore();
        }
        GameState.Instance.maxCurrentLevel = 1;
    }

    public void LoadSave()
    {
        OptionsFile saveFile = new OptionsFile(OptionsFile.SAVEFILLNAME);
        saveFile.Load();
        foreach(KeyValuePair<string, Score> levelScore in levelsScore)
        {
            if (saveFile.IsOptionExists(levelScore.Key))
            {   
                string scoreStr = saveFile.GetOptionString(levelScore.Key);
                (float timer, int moves, int sendToPast) = ParseStringScore(scoreStr);
                levelScore.Value.SetScore(timer, moves, sendToPast);
            }
        }


        GameState.Instance.maxCurrentLevel = 1;
        if (saveFile.IsOptionExists("maxCurrentLevel"))
        {
            GameState.Instance.maxCurrentLevel = saveFile.GetOptionInt("maxCurrentLevel");
        }
        PrintSave();
    }
    public void SaveGame()
    {
        OptionsFile saveFile = new OptionsFile(OptionsFile.SAVEFILLNAME);
        foreach(KeyValuePair<string, Score> levelScore in levelsScore)
        {
            saveFile.AddOption(levelScore.Key, $"{levelScore.Value.Timer}|{levelScore.Value.Moves}|{levelScore.Value.SendToPast}");
        }

        saveFile.AddOption("maxCurrentLevel", GameState.Instance.maxCurrentLevel);
        saveFile.Save();
    }

    public (float, int, int) ParseStringScore(string input)
    {
        // Split the string by '|'
        string[] parts = input.Split('|');
        // Convert to respective types
        float floatValue = float.Parse(parts[0]); 
        int intValue1 = int.Parse(parts[1]);
        int intValue2 = int.Parse(parts[2]);
        return (floatValue, intValue1, intValue2);
    }

    
}