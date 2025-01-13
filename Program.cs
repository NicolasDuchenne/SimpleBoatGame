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
    static Level3 Level4 = new Level3("4");
    static Level3 Level5 = new Level3("5");
    static Level3 Level6 = new Level3("6");
    static Level3 Level7 = new Level3("7");
    static Level3 Level8 = new Level3("8");
    static Level3 Level9 = new Level3("9");
    static Level3 Level10 = new Level3("10");
    

    public static int Main()
    {
        int gameScreenWidth = 960;
        int gameScreenHeight = 540;
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
        gameState.changeScene("10");
        Water water = new Water();

        while (!WindowShouldClose() & gameState.finishGame == false)
        {
            gameState.Update();
            water.Update();
            
            BeginDrawing();
            ClearBackground(Color.White);
            Raylib.BeginTextureMode(target); // dessine dans la target
            ClearBackground(Color.LightGray);
            
            water.Draw();
            gameState.Draw();
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