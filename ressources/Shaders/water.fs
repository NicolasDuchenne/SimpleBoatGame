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

    // First Perlin noise for large-scale patches
    float noise1 = perlinNoise(uv * 3.0 + vec2(time * 0.03, time * 0.05));

    // Second Perlin noise for smaller details
    float noise2 = perlinNoise(uv * 2.0 + vec2(time * 0.02, time * 0.01));

    // Combine the two Perlin noise patterns by multiplying them
    float combinedNoise = noise1 * noise2*100;

    // Add sine and cosine waves to enhance stripes
    float wave1 = sin(uv.x * 20.0 + time * 1.0) * 0.1;
    float wave2 = cos(uv.y * 30.0 - time * 0.5) * 0.05;

    // Final combination of noise and waves
    float finalValue = combinedNoise + wave1 + wave2;
    //finalValue = finalValue + wave1 + wave2;
    // Enhance visibility
    float combined = finalValue;
    combined = pow(abs(combined), 2) * 2.5; 


    // Apply thresholds to create large blue patches with small white stripes
    float threshold = smoothstep(0, 0.02, combined); // Adjust to control stripe width

    vec3 lightBlue = vec3(179, 218, 255)/255;
    vec3 darkBlue = vec3(26, 144, 255)/255;

    // Interpolate between blue patches and white stripes
    vec3 finalColor = mix(lightBlue, darkBlue, threshold);

    // Output final color
    fragColor = vec4(finalColor, 1.0);
}
