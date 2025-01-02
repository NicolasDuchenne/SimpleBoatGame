using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

public static class RaylibGame
{
    static SceneMenu sceneMenu = new SceneMenu();
    static SceneMenuLevel sceneMenuLevel = new SceneMenuLevel();
    static SceneOptions sceneOptions = new SceneOptions();
    static Level1 Level1 = new Level1();
    static Level2 Level2 = new Level2();
    static Level3 Level3 = new Level3();
    

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

        gameState.RegisterScene("menu", sceneMenu);
        gameState.RegisterScene("menuLevel", sceneMenuLevel);
        gameState.RegisterScene("options", sceneOptions);
        gameState.RegisterScene("1", Level1, "2");
        gameState.RegisterScene("2", Level2, "3");
        gameState.RegisterScene("3", Level3, "menu");
        gameState.changeScene("menu");

        while (!WindowShouldClose() & gameState.finishGame == false)
        {
            gameState.Update();
            
            BeginDrawing();
            ClearBackground(Color.White);
            Raylib.BeginTextureMode(target); // dessine dans la target
            ClearBackground(Color.LightGray);
            gameState.Draw();
            Raylib.EndTextureMode();
            Rectangle sourceRect = new Rectangle(0, 0, target.Texture.Width, -target.Texture.Height); // Dans OpenGl, les axe des texturesy sont inversé, donc on mets le -
            Rectangle targetRect = new Rectangle(gameState.XOffset, gameState.YOffset, gameState.ResizedGameWidth, gameState.ResizedGameHeight);
            Raylib.DrawTexturePro(target.Texture, sourceRect, targetRect, new Vector2(), 0, Color.White);
            EndDrawing();
        }

        OptionsFile saveFile = new OptionsFile(OptionsFile.SAVEFILLNAME);
        saveFile.AddOption("maxCurrentLevel", gameState.maxCurrentLevel);
        saveFile.Save();
        gameState.Close();
        

        CloseWindow();
        return 0;
    }
}