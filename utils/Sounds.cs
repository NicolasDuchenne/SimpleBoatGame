using System;
using Raylib_cs;


public class Sounds
{
     public static Sounds explosionSound = new Sounds("ressources/Sounds/explosionSound.mp3");
     public static Sounds shootingSound = new Sounds("ressources/Sounds/shootingSound.mp3");
     Sound sound;

     public Sounds(string soundPath)
     {
        sound = Raylib.LoadSound(soundPath); 
     }

     public void Play()
     {
        Random random = new Random();
        float randomPitch = (float)(random.NextDouble() * 0.2 + 0.9);
        Raylib.SetSoundPitch(sound, randomPitch);
        Raylib.PlaySound(sound);
     }

     public void Unload()
     {
         Raylib.UnloadSound(sound);
     }

}
public class Musics
{
   public static string ambianceSeaPath = "ressources/Sounds/ambianceMer.wav";
   Music music;

   public Musics(string musicPath)
   {
        music = Raylib.LoadMusicStream(musicPath); 
        Raylib.PlayMusicStream(music);
   }

   public void Update()
   {  
      Raylib.UpdateMusicStream(music);
   }

   public void Unload()
   {
      Raylib.UnloadMusicStream(music);
   }
}