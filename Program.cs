using Raylib_cs;

using System.Numerics;

public static class RaylibGame
{

    

    public static int Main()
    {
        int gameScreenWidth = 960;
        int gameScreenHeight = 540;
        
        //SceneManagerService scenesManager = new SceneManagerService();

        Raylib.InitWindow(gameScreenWidth, gameScreenHeight, "Premier programme Raylib");
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

        LevelRegister.Register();
        gameState.changeScene("menu");
        

        //Services.Get<ISceneManagerService>().Load<TestSceneService>();

        while (!Raylib.WindowShouldClose() & gameState.finishGame == false)
        {
            gameState.Update();
            //scenesManager.Update();
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.BeginTextureMode(target); // dessine dans la target
            Raylib.ClearBackground(Color.SkyBlue);

            gameState.Draw();
            //scenesManager.Draw();
            Raylib.EndTextureMode();
            Rectangle sourceRect = new Rectangle(0, 0, target.Texture.Width, -target.Texture.Height); // Dans OpenGl, les axe des texturesy sont inversé, donc on mets le -
            Rectangle targetRect = new Rectangle(gameState.XOffset, gameState.YOffset, gameState.ResizedGameWidth, gameState.ResizedGameHeight);
            Raylib.DrawTexturePro(target.Texture, sourceRect, targetRect, new Vector2(), 0, Color.White); // Dessine la target sur le full screen
            Raylib.EndDrawing();
        }

        Save.Instance.SaveGame();
        gameState.Close();
        

        Raylib.CloseWindow();
        return 0;
    }
}