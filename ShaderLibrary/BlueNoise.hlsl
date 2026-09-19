#ifndef UNITY_BLUENOISE_INCLUDED
#define UNITY_BLUENOISE_INCLUDED

Texture2DArray<float>  _STBNVec1Texture;
Texture2DArray<float2> _STBNVec2Texture;
Texture2DArray<float4> _STBNUnitVec3Texture;
Texture2DArray<float4> _STBNUnitVec3CosineTexture;

int _STBNIndex;

float GetSpatiotemporalBlueNoiseVec1(uint2 pixelCoord, uint indexOffset = 0)
{
    return _STBNVec1Texture[uint3(pixelCoord.x % 128u, pixelCoord.y % 128u, ((uint)_STBNIndex + indexOffset) % 64u)].x;
}

float2 GetSpatiotemporalBlueNoiseVec2(uint2 pixelCoord, uint indexOffset = 0)
{
    return _STBNVec2Texture[uint3(pixelCoord.x % 128u, pixelCoord.y % 128u, ((uint)_STBNIndex + indexOffset) % 64u)].xy;
}

float3 GetSpatiotemporalBlueNoiseUnitVec3(uint2 pixelCoord, uint indexOffset = 0)
{
    float3 dir = _STBNUnitVec3Texture[uint3(pixelCoord.x & 127u, pixelCoord.y & 127u, ((uint)_STBNIndex + indexOffset) & 63u)].xyz;

    dir = dir * 2.0 - 1.0;
    dir = normalize(dir);

    // Uniform sphere -> uniform +Z hemisphere
    dir.z = abs(dir.z);

    return dir;
}

float3 GetSpatiotemporalBlueNoiseUnitVec3Cosine(uint2 pixelCoord, uint indexOffset = 0)
{
    float3 rayDir = _STBNUnitVec3CosineTexture[uint3(pixelCoord.x % 128u, pixelCoord.y % 128u, ((uint)_STBNIndex + indexOffset) % 64u)].xyz;
    rayDir = rayDir * 2.0 - 1.0;
    return normalize(rayDir);
}

#endif //UNITY_BLUENOISE_INCLUDED
