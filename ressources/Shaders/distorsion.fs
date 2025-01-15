#version 330

uniform float time;           // Time for animating the distortion
uniform sampler2D texture0;   // Input texture (the object or scene)
uniform vec2 resolution;      // Screen resolution for scaling
uniform float alpha;

in vec2 fragTexCoord;         // Texture coordinates passed from the vertex shader
out vec4 fragColor;           // Output fragment color

void main() {
    // UV coordinates (normalized texture coordinates)
    vec2 uv = fragTexCoord;

    // Mirage distortion effect: Horizontal wave motion from right to left
    float waveIntensity = 0.08;  // How strong the wave effect is
    float frequency = 10.0;      // Frequency of the waves
    float speed = 1.5;           // Speed of the wave motion (controls how fast the mirage moves)
    
    // Calculate horizontal offset
    float offset = sin(uv.y * frequency + time * speed) * waveIntensity;

    // Shift the UV coordinates
    uv.x += offset;
    
    // Clamp the UV coordinates to prevent overflow
    uv = clamp(uv, 0.0, 1.0);

    // Sample the texture at the new distorted UV coordinates
    vec4 baseColor = texture(texture0, uv);

    // Preserve the original alpha
    fragColor = vec4(baseColor.rgb, baseColor.a*alpha);
}
