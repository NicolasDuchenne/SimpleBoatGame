using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

public static class RaylibGame
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
    

    public static int Main()
    {
        int gameScreenWidth = 960;
        int gameScreenHeight = 540;
        
        //SceneManagerService scenesManager = new SceneManagerService();

        InitWindow(gameScreenWidth, gameScreenHeight, "Premier programme Raylib");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        //Raylib.SetWindowState(ConfigFlags.UndecoratedWindow);
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.SetExitKey(KeyboardKey.P);

        RenderTexture2D target = Raylib.LoadRenderTexture(gameScreenWidth, gameScreenHeight);
        Raylib.SetTextureFilter(target.Texture, TextureFilter.Point); // pour ne pas avoir d'anti aliasing

        GameState gameState = GameState.Instance;
        gameState.SetVirtualGameResolution(gameScreenWidth, gameScreenHeight);

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
        gameState.RegisterScene(Level10, "menu");

        Save.Instance.LoadSave();
        gameState.changeScene("menu");
        Water water = new Water();

        //Services.Get<ISceneManagerService>().Load<TestSceneService>();

        while (!WindowShouldClose() & gameState.finishGame == false)
        {
            gameState.Update();
            //scenesManager.Update();
            
            water.Update();
            
            BeginDrawing();
            ClearBackground(Color.White);
            Raylib.BeginTextureMode(target); // dessine dans la target
            ClearBackground(Color.SkyBlue);

            water.Draw();
            gameState.Draw();
            //scenesManager.Draw();
            Raylib.EndTextureMode();
            Rectangle sourceRect = new Rectangle(0, 0, target.Texture.Width, -target.Texture.Height); // Dans OpenGl, les axe des texturesy sont inversé, donc on mets le -
            Rectangle targetRect = new Rectangle(gameState.XOffset, gameState.YOffset, gameState.ResizedGameWidth, gameState.ResizedGameHeight);
            Raylib.DrawTexturePro(target.Texture, sourceRect, targetRect, new Vector2(), 0, Color.White);
            EndDrawing();
        }

        Save.Instance.SaveGame();
        gameState.Close();
        

        CloseWindow();
        return 0;
    }
}