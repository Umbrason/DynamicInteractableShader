

float SqrDst(float4 A, float3 Pos)
{
    return (A.x - Pos.x) * (A.x - Pos.x) + (A.y - Pos.y) * (A.y - Pos.y) + (A.z - Pos.z) * (A.z - Pos.z);
}

SamplerState my_point_clamp_sampler;
void GetNearestPosition_float(float interactors, Texture2D positionTexture, float3 position, out float3 Out)
{
    float4 sampled = positionTexture.SampleLevel(my_point_clamp_sampler,float2(.5 / interactors, 0),0);
    float4 closest = sampled;
    float closestSqrDst = SqrDst(closest, position);
    for(int i = 1; i < interactors; i++)
    {
        float4 sampled = positionTexture.SampleLevel(my_point_clamp_sampler,float2((i+.5) / interactors, 0),0);
        float4 cur = sampled;
        float sqrDst = SqrDst(cur, position) / sampled.w;
        if(closestSqrDst >= sqrDst) {
            closest = cur;
            closestSqrDst = sqrDst;
        }
    }
    Out = closest;
}

