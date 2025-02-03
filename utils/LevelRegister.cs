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
    static Level12 Level12 = new Level12("12");
    static Level13 Level13 = new Level13("13");
    static Level14 Level14 = new Level14("14");
    static Level15 Level15 = new Level15("15");
    static Level16 Level16 = new Level16("16");


    public static void Register()
    {
        SceneManager scenesManager = Services.Get<SceneManager>();
        scenesManager.RegisterScene(sceneMenu);
        scenesManager.RegisterScene(sceneMenuLevel);
        scenesManager.RegisterScene(sceneOptions);
        scenesManager.RegisterScene(Level1, "2");
        scenesManager.RegisterScene(Level2, "3");
        scenesManager.RegisterScene(Level3, "4");
        scenesManager.RegisterScene(Level4, "5");
        scenesManager.RegisterScene(Level5, "6");
        scenesManager.RegisterScene(Level6, "7");
        scenesManager.RegisterScene(Level7, "8");
        scenesManager.RegisterScene(Level8, "9");
        scenesManager.RegisterScene(Level9, "10");
        scenesManager.RegisterScene(Level10, "11");
        scenesManager.RegisterScene(Level11, "12");
        scenesManager.RegisterScene(Level12, "13");
        scenesManager.RegisterScene(Level13, "14");
        scenesManager.RegisterScene(Level14, "15");
        scenesManager.RegisterScene(Level15, "16");
        scenesManager.RegisterScene(Level16, "menu");
        Save.Instance.LoadSave(); // Must always be after scene creation for the score system to work
    }
}