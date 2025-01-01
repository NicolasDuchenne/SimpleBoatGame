using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

public static class RaylibGame
{
    static SceneMenu sceneMenu = new SceneMenu();
    static SceneOptions sceneOptions = new SceneOptions();
    static SceneLevel1 sceneLevel1 = new SceneLevel1();
    static SceneLevel2 sceneLevel2 = new SceneLevel2();
    

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
        gameState.RegisterScene("options", sceneOptions);
        gameState.RegisterScene("level 1", sceneLevel1, "level 2");
        gameState.RegisterScene("level 2", sceneLevel2, "menu");
        gameState.changeScene("level 1");

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

        gameState.Close();
        

        CloseWindow();
        return 0;
    }
}