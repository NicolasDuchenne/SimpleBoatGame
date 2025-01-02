#version 330 core

uniform float time; // Time uniform for animation
uniform float windowWidth;
uniform float windowHeight;

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
    // Normalize screen coordinates to range 0 to 1
    vec2 uv = gl_FragCoord.xy / vec2(800.0, 600.0);

    // Generate Perlin noise
    float noiseUp = perlinNoise(uv * 5.0 + vec2(time * 0.2, 0)); // Larger patches with smaller frequency
    float noiseLeft = perlinNoise(uv * 3.0 + vec2(0, time * 0.1)); // Larger patches with smaller frequency

    // Generate sinusoidal waves for stripes
    float wave = sin(uv.x * 20.0 + time * 3.0) * 0.3; // Higher frequency for thinner stripes

    // Combine Perlin noise and waves
    float combined = noiseLeft *noiseUp*100;
    // Enhance visibility
    combined = pow(abs(combined), 2.0) * 2.5;


    // Apply thresholds to create large blue patches with small white stripes
    float threshold = smoothstep(0.1, 0.2, combined); // Adjust to control stripe width

    // Base gradient for large blue areas
    vec3 bluePatch = vec3(0.0, 0.3, 0.8);

    // White stripes
    vec3 whiteStripe = vec3(1.0, 1.0, 1.0);

    // Interpolate between blue patches and white stripes
    vec3 finalColor = mix(whiteStripe, bluePatch, threshold);

    // Output final color
    fragColor = vec4(finalColor, 1.0);
}
