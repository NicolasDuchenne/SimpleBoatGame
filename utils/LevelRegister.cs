public static class LevelRegister
{
    static SceneMenu sceneMenu = new SceneMenu("menu");
    static SceneMenuLevel sceneMenuLevel = new SceneMenuLevel("menuLevel");
    static SceneOptions sceneOptions = new SceneOptions("options");
    static Level1 Level1 = new Level1("1");
    static Level2 Level2 = new Level2("2");
    static Level3 Level3 = new Level3("3");
    static Level4 Level4 = new Level4("4");
    static Level5 Level5 = new Level5("5");
    static Level6 Level6 = new Level6("6");
    static Level7 Level7 = new Level7("7");
    static Level8 Level8 = new Level8("8");
    static Level9 Level9 = new Level9("9");
    static Level10 Level10 = new Level10("10");
    static Level11 Level11 = new Level11("11");

    public static void Register()
    {
        GameState gameState = GameState.Instance;
        gameState.RegisterScene(sceneMenu);
        gameState.RegisterScene(sceneMenuLevel);
        gameState.RegisterScene(sceneOptions);
        gameState.RegisterScene(Level1, "2");
        gameState.RegisterScene(Level2, "3");
        gameState.RegisterScene(Level3, "4");
        gameState.RegisterScene(Level4, "5");
        gameState.RegisterScene(Level5, "6");
        gameState.RegisterScene(Level6, "7");
        gameState.RegisterScene(Level7, "8");
        gameState.RegisterScene(Level8, "9");
        gameState.RegisterScene(Level9, "10");
        gameState.RegisterScene(Level10, "11");
        gameState.RegisterScene(Level11, "menu");
        Save.Instance.LoadSave(); // Must always be after scene creation for the score system to work
    }
}