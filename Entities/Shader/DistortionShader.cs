using Raylib_cs;

public static class DistortionShader
{
        private static string shaderLoc = "ressources/Shaders/distorsion.fs";
        public static Shader shader = Raylib.LoadShader(null, shaderLoc);
}