sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float Length(float2 vec) {
    return sqrt(vec.x * vec.x + vec.y * vec.y);
}

float4 Blindness(float2 texCoord : TEXCOORD0) : COLOR0
{
    float2 screenCenter = 0.5f * uScreenResolution;
    float2 currentPixel = texCoord * uScreenResolution;

    float4 color = tex2D(uImage0, texCoord);
    float dist = Length(currentPixel - screenCenter);
    if(dist > 96.0f) {
        color *= clamp((128.0f - dist) / 32.0f, max(0.15f, uProgress * 0.85f + 0.15f), 1.0f);
    }
    return color;
}

technique Technique1
{
    pass Blindness
    {
        PixelShader = compile ps_2_0 Blindness();
    }
}
