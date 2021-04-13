float SqrDst(float4 A, float3 Pos)
{
    return (A.x - Pos.x) * (A.x - Pos.x) + (A.y - Pos.y) * (A.y - Pos.y) + (A.z - Pos.z) * (A.z - Pos.z);
}

float2 IndexToUV(float interactor, float interactorCount, float channel, float channelCount)
{
    return float2((interactor + 1.5) / (interactorCount + 1), (channel + .5) / channelCount);
}

SamplerState my_point_clamp_sampler;
void GetNearestPosition_float(float interactorCount, float channel, float channelCount, Texture2D positionTexture, float3 position, out float4 Out)
{    
    float channelInteractorCount = positionTexture.SampleLevel(my_point_clamp_sampler, float2(.5 / interactorCount, (channel + .5) / channelCount), 0).x;    
    float4 closest = positionTexture.SampleLevel(my_point_clamp_sampler, IndexToUV(0, interactorCount, channel, channelCount), 0);
    float closestSqrDst = SqrDst(closest, position) / closest.w / closest.w;

    for(int i = 1; i < channelInteractorCount; i++)
    {
        float4 cur = positionTexture.SampleLevel(my_point_clamp_sampler, IndexToUV(i, interactorCount, channel, channelCount), 0);
        float sqrDst = SqrDst(cur, position) / cur.w / cur.w;
        if(closestSqrDst >= sqrDst) {
            closest = cur;
            closestSqrDst = sqrDst;
        }
    }
    Out = closest;
}

