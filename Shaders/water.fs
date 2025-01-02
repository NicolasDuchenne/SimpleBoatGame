#version 330 core

uniform float time; // Time uniform for animation

out vec4 fragColor;

// Random gradient generator
vec2 random(vec2 p)
{
    return fract(sin(vec2(dot(p, vec2(127.1, 311.7)),
                          dot(p, vec2(269.5, 183.3)))) *
                 43758.5453);
}

// Interpolated Perlin noise
float perlinNoise(vec2 p)
{
    vec2 i = floor(p);
    vec2 f = fract(p);

    // Smoothstep fade
    vec2 u = f * f * (3.0 - 2.0 * f);

    // Random gradient values
    float a = dot(random(i + vec2(0.0, 0.0)), f - vec2(0.0, 0.0));
    float b = dot(random(i + vec2(1.0, 0.0)), f - vec2(1.0, 0.0));
    float c = dot(random(i + vec2(0.0, 1.0)), f - vec2(0.0, 1.0));
    float d = dot(random(i + vec2(1.0, 1.0)), f - vec2(1.0, 1.0));

    // Interpolate results
    return mix(mix(a, b, u.x), mix(c, d, u.x), u.y);
}

void main()
{
    // Normalize screen coordinates to range -1 to 1
    vec2 uv = gl_FragCoord.xy / vec2(800.0, 600.0);

    // Generate Perlin noise for water effect
    float noise = perlinNoise(uv * 5.0 + vec2(time * 0.5, time * 0.2));

    // Background gradient
    vec3 gradientColor = mix(vec3(0.0, 0.3, 0.8), vec3(0.2, 0.5, 1.0), uv.y);

    // Final water effect
    vec3 waterColor = gradientColor + vec3(noise * 0.2);

    fragColor = vec4(waterColor, 1.0);
}
