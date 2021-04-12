float SqrDst(float4 A, float3 Pos)
{
    return (A.x - Pos.x) * (A.x - Pos.x) + (A.y - Pos.y) * (A.y - Pos.y) + (A.z - Pos.z) * (A.z - Pos.z);
}
float SquareClamp(float A)
{
    return max(0.001f, A * A);
}

SamplerState my_point_clamp_sampler;
void GetNearestPosition_float(float interactors, float channel, float channelCount, Texture2D positionTexture, float3 position, out float4 Out)
{
    float channelIndex = (.5 + channel) / channelCount;
    float channelInteractionCount = positionTexture.SampleLevel(my_point_clamp_sampler, float2(.5f, channelIndex),0);
    float4 cur = positionTexture.SampleLevel(my_point_clamp_sampler, float2(1.5f / interactors, channelIndex),0);
    float4 closest = cur;
    float closestSqrDst = SqrDst(closest, position) / SquareClamp(cur.w);
    for(int i = 1; i < channelInteractionCount; i++)
    {
        cur = positionTexture.SampleLevel(my_point_clamp_sampler, float2((i+1.5f) / interactors, channelIndex),0);
        float sqrDst = SqrDst(cur, position) / SquareClamp(cur.w);
        if(closestSqrDst >= sqrDst) {
            closest = cur;
            closestSqrDst = sqrDst;
        }
    }
    Out = closest;
}

