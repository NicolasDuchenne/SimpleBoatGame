uniform sampler2D texture0;   // La texture source
uniform float time;           // Le temps pour des effets optionnels
uniform float alpha;
uniform float scale;

in vec2 fragTexCoord;         // Coordonnées de texture (UV) interpolées
out vec4 fragColor;           // Couleur de sortie du pixel
// Fonction pseudo-aléatoire basée sur Perlin-like noise
// Fonction pseudo-aléatoire basée sur Perlin-like noise
float random(vec2 st) {
    return fract(sin(dot(st.xy, vec2(12.9898, 78.233))) * 43758.5453123);
}

// Fonction de bruit Perlin 2D
float perlinNoise(vec2 st) {
    vec2 i = floor(st);
    vec2 f = fract(st);

    vec2 u = f * f * (3.0 - 2.0 * f);

    float a = random(i);
    float b = random(i + vec2(1.0, 0.0));
    float c = random(i + vec2(0.0, 1.0));
    float d = random(i + vec2(1.0, 1.0));

    return mix(mix(a, b, u.x), mix(c, d, u.x), u.y);
}

// Fonction pour interpoler une direction aléatoire fluide
vec2 randomDirection(float t) {
    return vec2(sin(t * 0.1), cos(t * 0.13));
}

// Rotate UV coordinates around the center
vec2 rotateUV(vec2 uv, vec2 center, float angle) {
    // Translation of UV to center
    vec2 translated = uv - center;
    
    // Rotation matrix
    float cosAngle = cos(angle);
    float sinAngle = sin(angle);
    
    vec2 rotated = vec2(
        translated.x * cosAngle - translated.y * sinAngle,
        translated.x * sinAngle + translated.y * cosAngle
    );
    
    // Return rotated UVs translated back to original space
    return rotated + center;
}

// Diamond distance function with adjustable corner roundness
float diamondDistance(vec2 uv, vec2 center, float roundness) {
    // Calculate distance to diamond edges
    vec2 d = abs(uv - center);  // Absolute distance from center
    float linearDist = d.x + d.y;  // Manhattan distance for diamond shape

    // Blend roundness into corners
    float cornerDist = length(uv - center);  // Euclidean distance for rounding
    return mix(linearDist, cornerDist, roundness);  // Blend sharpness with roundness
}


void main() {
    vec2 uv = fragTexCoord;
    // Store static UVs for fade calculations
    vec2 staticUV = uv;

    // Charger la couleur de la texture
    vec4 baseColor = texture(texture0, uv);

    // Centre de la texture
    vec2 center = vec2(0.5, 0.5);

    // Rotate UV coordinates to create a spiraling effect
    float rotationSpeed = 0.1;  // Speed of the rotation
    float angle = time * rotationSpeed;  // Angle over time
    uv = rotateUV(uv, center, angle);

    float distanceToCenter = length(uv - center);

    // Générer une direction aléatoire qui change avec le temps
    vec2 direction = randomDirection(time);

    // Ajouter un léger mouvement sinusoïdal
    float waveX = sin(uv.x * 10.0 + time * 0.7 + sin(time * 0.2) * 3.0);
    float waveY = cos(uv.y * 10.0 + time * 0.8 + cos(time * 0.1) * 2.5);
    float waveNoise = waveX * waveY;

    // Générer le bruit de Perlin avec déplacement temporel et direction
    float scale = scale;
    //float perlinValue = perlinNoise((uv + direction * 0.5) * scale + time * 0.5);
    float perlinValue = perlinNoise((uv + 0 * 0.5) * scale + 0 * 0.5);

    // Diamond fade with adjustable roundness
    float roundness = 1;  // Roundness factor (0 = sharp diamond, 1 = fully round)
    float distanceToEdge = diamondDistance(staticUV, center, roundness);

    

    // Calculer l'intensité du brouillard
    //float alphaFade = smoothstep(0.50, 0.51, 1 - distanceToCenter);  // Transition douce
    float alphaFade = smoothstep(0.43, 0.44, 1.0 - distanceToEdge);  // Adjust fade start and end
    float alphaFade2 = smoothstep(0.12, 0.13, distanceToEdge);  // Adjust fade start and end
     
    // fogFactor = (perlinValue  + 0 * waveNoise) * alphaFade  ;  // Mélanger le bruit Perlin et le bruit sinus/cos
    float fogFactor = alphaFade;

    // Couleur du brouillard
    vec3 fogColor = vec3(0.5, 0.5, 0.55);  // Gris clair

    fragColor = vec4(baseColor.rgb, baseColor.a * alpha * fogFactor);
}