using System;
using Raylib_cs;


public class Sounds
{
     public static Sounds explosionSound = new Sounds("ressources/Sounds/explosionSound.mp3");
     public static Sounds shootingSound = new Sounds("ressources/Sounds/shootingSound.mp3");
     public static Sounds banishSound = new Sounds("ressources/Sounds/banishSound.mp3");
     public static Sounds banishSoundReversed = new Sounds("ressources/Sounds/banishSoundReversed.mp3");
     Sound sound;

     public Sounds(string soundPath)
     {
        sound = Raylib.LoadSound(soundPath); 
     }

     public void Play(float volume=1f, float? pitch=null)
     {
         if (pitch is null)
         {
            Random random = new Random();
            float randomPitch = (float)(random.NextDouble() * 0.2 + 0.9);
            Raylib.SetSoundPitch(sound, randomPitch);
         }
         else
         {
            Raylib.SetSoundPitch(sound, (float)pitch);
         }
         
         Raylib.SetSoundVolume(sound, volume);
         Raylib.PlaySound(sound);
     }

     public void Unload()
     {
         Raylib.UnloadSound(sound);
     }

}
public class Musics
{
   public static Music ambianceSeaMusic = Raylib.LoadMusicStream("ressources/Sounds/ambianceMer.wav"); 
   Music music;

   public Musics(Music music)
   {
        this.music = music;
        Raylib.PlayMusicStream(this.music);
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