#ifndef DANBAIDONG_DECLARE_GLOBAL_ILLUMINATION_TEXTURE_INCLUDED
#define DANBAIDONG_DECLARE_GLOBAL_ILLUMINATION_TEXTURE_INCLUDED

TEXTURE2D_X(_GlobalIlluminationTexture);

float3 LoadSceneGlobalIllumination(uint2 pixelCoord)
{
    return LOAD_TEXTURE2D_X(_GlobalIlluminationTexture, pixelCoord).rgb;
}

#endif
